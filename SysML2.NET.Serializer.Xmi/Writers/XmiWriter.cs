// -------------------------------------------------------------------------------------------------
// <copyright file="XmiWriter.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2026 Starion Group S.A.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.Xmi.Writers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Xml;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

    using SysML2.NET.Common;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Decorators;

    /// <summary>
    /// The purpose of the <see cref="XmiWriter"/> is to write <see cref="IData"/> instances
    /// to an <see cref="XmlWriter"/> in XMI format using reflection on the POCO [Property] attributes
    /// </summary>
    public class XmiWriter : IXmiWriter
    {
        private readonly ILogger<XmiWriter> logger;

        private static readonly Dictionary<Type, List<PropertySerializationInfo>> PropertyCache = new Dictionary<Type, List<PropertySerializationInfo>>();
        private static readonly object PropertyCacheLock = new object();

        public XmiWriter(ILoggerFactory loggerFactory = null)
        {
            this.logger = loggerFactory == null ? NullLogger<XmiWriter>.Instance : loggerFactory.CreateLogger<XmiWriter>();
        }

        /// <summary>
        /// Writes the specified <see cref="IData"/> as an XMI element
        /// </summary>
        public void Write(XmlWriter xmlWriter, IData data, string elementName, bool includeDerivedProperties, IXmiElementOriginMap elementOriginMap = null, Uri currentFileUri = null)
        {
            if (xmlWriter == null)
            {
                throw new ArgumentNullException(nameof(xmlWriter));
            }

            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var typeName = data.GetType().Name;

            xmlWriter.WriteStartElement(elementName);
            xmlWriter.WriteAttributeString("xsi", "type", null, $"sysml:{typeName}");
            xmlWriter.WriteAttributeString("xmi", "id", null, data.Id.ToString());

            var properties = GetSerializationProperties(data.GetType());

            // Write scalar (non-reference) properties as XML attributes
            foreach (var propInfo in properties)
            {
                if (propInfo.IsDerived && !includeDerivedProperties)
                {
                    continue;
                }

                if (propInfo.IsReference)
                {
                    continue;
                }

                this.WriteScalarAttribute(xmlWriter, data, propInfo);
            }

            // Write reference and containment properties as child elements
            foreach (var propInfo in properties)
            {
                if (propInfo.IsDerived && !includeDerivedProperties)
                {
                    continue;
                }

                if (!propInfo.IsReference)
                {
                    continue;
                }

                this.WriteReferenceElements(xmlWriter, data, propInfo, includeDerivedProperties, elementOriginMap, currentFileUri);
            }

            xmlWriter.WriteEndElement();
        }

        private void WriteScalarAttribute(XmlWriter xmlWriter, IData data, PropertySerializationInfo propInfo)
        {
            object value;

            try
            {
                value = propInfo.PropertyInfo.GetValue(data);
            }
            catch (TargetInvocationException)
            {
                return;
            }

            if (value == null)
            {
                return;
            }

            if (propInfo.IsEnumerable)
            {
                var enumerable = (IEnumerable)value;
                var values = new List<string>();

                foreach (var item in enumerable)
                {
                    if (item != null)
                    {
                        values.Add(FormatScalarValue(item));
                    }
                }

                if (values.Count > 0)
                {
                    xmlWriter.WriteAttributeString(propInfo.XmiName, string.Join(" ", values));
                }
            }
            else
            {
                var formatted = FormatScalarValue(value);

                if (formatted != null && !IsDefaultValue(value, propInfo))
                {
                    xmlWriter.WriteAttributeString(propInfo.XmiName, formatted);
                }
            }
        }

        private void WriteReferenceElements(XmlWriter xmlWriter, IData data, PropertySerializationInfo propInfo, bool includeDerivedProperties, IXmiElementOriginMap elementOriginMap, Uri currentFileUri)
        {
            object value;

            try
            {
                value = propInfo.PropertyInfo.GetValue(data);
            }
            catch (TargetInvocationException)
            {
                return;
            }

            if (value == null)
            {
                return;
            }

            if (propInfo.IsEnumerable)
            {
                var enumerable = (IEnumerable)value;

                foreach (var item in enumerable)
                {
                    if (item is IData childData)
                    {
                        if (propInfo.IsComposite)
                        {
                            this.WriteContainedElement(xmlWriter, childData, propInfo.XmiName, includeDerivedProperties, elementOriginMap, currentFileUri);
                        }
                        else
                        {
                            this.WriteReferenceElement(xmlWriter, childData, propInfo.XmiName, elementOriginMap, currentFileUri);
                        }
                    }
                }
            }
            else
            {
                if (value is IData childData)
                {
                    if (propInfo.IsComposite)
                    {
                        this.WriteContainedElement(xmlWriter, childData, propInfo.XmiName, includeDerivedProperties, elementOriginMap, currentFileUri);
                    }
                    else
                    {
                        this.WriteReferenceElement(xmlWriter, childData, propInfo.XmiName, elementOriginMap, currentFileUri);
                    }
                }
            }
        }

        private void WriteContainedElement(XmlWriter xmlWriter, IData childData, string elementName, bool includeDerivedProperties, IXmiElementOriginMap elementOriginMap, Uri currentFileUri)
        {
            if (elementOriginMap != null && currentFileUri != null)
            {
                var childSourceFile = elementOriginMap.GetSourceFile(childData.Id);

                if (childSourceFile != null && childSourceFile != currentFileUri)
                {
                    WriteHrefElement(xmlWriter, childData, elementName, childSourceFile, currentFileUri);
                    return;
                }
            }

            this.Write(xmlWriter, childData, elementName, includeDerivedProperties, elementOriginMap, currentFileUri);
        }

        private void WriteReferenceElement(XmlWriter xmlWriter, IData childData, string elementName, IXmiElementOriginMap elementOriginMap, Uri currentFileUri)
        {
            if (elementOriginMap != null && currentFileUri != null)
            {
                var childSourceFile = elementOriginMap.GetSourceFile(childData.Id);

                if (childSourceFile != null && childSourceFile != currentFileUri)
                {
                    WriteHrefElement(xmlWriter, childData, elementName, childSourceFile, currentFileUri);
                    return;
                }
            }

            // Same file reference - write as id reference
            xmlWriter.WriteStartElement(elementName);
            xmlWriter.WriteAttributeString("xmi", "idref", null, childData.Id.ToString());
            xmlWriter.WriteEndElement();
        }

        private static void WriteHrefElement(XmlWriter xmlWriter, IData childData, string elementName, Uri targetFile, Uri currentFile)
        {
            var relativePath = currentFile.MakeRelativeUri(targetFile);
            var href = $"{Uri.UnescapeDataString(relativePath.ToString())}#{childData.Id}";

            xmlWriter.WriteStartElement(elementName);
            xmlWriter.WriteAttributeString("href", href);
            xmlWriter.WriteEndElement();
        }

        private static string FormatScalarValue(object value)
        {
            switch (value)
            {
                case bool b:
                    return b ? "true" : "false";
                case int i:
                    return i.ToString(CultureInfo.InvariantCulture);
                case double d:
                    return d.ToString(CultureInfo.InvariantCulture);
                case Enum e:
                    return e.ToString();
                case string s:
                    return s;
                default:
                    return value.ToString();
            }
        }

        private static bool IsDefaultValue(object value, PropertySerializationInfo propInfo)
        {
            if (propInfo.DefaultValue == null)
            {
                return false;
            }

            var formatted = FormatScalarValue(value);
            return string.Equals(formatted, propInfo.DefaultValue, StringComparison.OrdinalIgnoreCase);
        }

        private static List<PropertySerializationInfo> GetSerializationProperties(Type pocoType)
        {
            lock (PropertyCacheLock)
            {
                if (PropertyCache.TryGetValue(pocoType, out var cached))
                {
                    return cached;
                }

                var result = new List<PropertySerializationInfo>();
                var interfaces = pocoType.GetInterfaces();

                var processedNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                foreach (var iface in interfaces)
                {
                    foreach (var prop in iface.GetProperties())
                    {
                        var attr = prop.GetCustomAttribute<PropertyAttribute>();

                        if (attr == null)
                        {
                            continue;
                        }

                        var xmiName = char.ToLowerInvariant(prop.Name[0]) + prop.Name.Substring(1);

                        if (!processedNames.Add(xmiName))
                        {
                            continue;
                        }

                        var propType = prop.PropertyType;
                        var isEnumerable = propType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(propType);
                        var elementType = isEnumerable ? GetElementType(propType) : propType;
                        var isReference = elementType != null && typeof(IData).IsAssignableFrom(elementType);

                        // Try to get the property from the concrete type to ensure we can read it
                        var concreteProp = pocoType.GetProperty(prop.Name) ?? prop;

                        result.Add(new PropertySerializationInfo
                        {
                            PropertyInfo = concreteProp,
                            XmiName = xmiName,
                            IsDerived = attr.IsDerived,
                            IsComposite = attr.Aggregation == AggregationKind.Composite,
                            IsReference = isReference,
                            IsEnumerable = isEnumerable,
                            DefaultValue = attr.DefaultValue
                        });
                    }
                }

                PropertyCache[pocoType] = result;
                return result;
            }
        }

        private static Type GetElementType(Type enumerableType)
        {
            if (enumerableType.IsGenericType)
            {
                return enumerableType.GetGenericArguments().FirstOrDefault();
            }

            foreach (var iface in enumerableType.GetInterfaces())
            {
                if (iface.IsGenericType && iface.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    return iface.GetGenericArguments().FirstOrDefault();
                }
            }

            return null;
        }

        private class PropertySerializationInfo
        {
            public PropertyInfo PropertyInfo { get; set; }
            public string XmiName { get; set; }
            public bool IsDerived { get; set; }
            public bool IsComposite { get; set; }
            public bool IsReference { get; set; }
            public bool IsEnumerable { get; set; }
            public string DefaultValue { get; set; }
        }
    }
}
