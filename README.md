# SysML2.NET - GSEF

SysML2.NET is a .NET implementation of the [OMG SysML2 specification](https://github.com/Systems-Modeling/SysML-v2-Release). SysML2.NET provides a number of libraries in the form on an SDK fixed
on release 2022-09 to support the GSEF implementation

> THIS BRANCH SHALL NOT BE MERGED INTO DEVELOPMENT OR MASTER. ITS PURPOSE IS TO SUPPORT GSEF

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=RHEAGROUP_SysML2.NET&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=RHEAGROUP_SysML2.NET)
[![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=RHEAGROUP_SysML2.NET&metric=code_smells)](https://sonarcloud.io/summary/new_code?id=RHEAGROUP_SysML2.NET)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=RHEAGROUP_SysML2.NET&metric=coverage)](https://sonarcloud.io/summary/new_code?id=RHEAGROUP_SysML2.NET)
[![Duplicated Lines (%)](https://sonarcloud.io/api/project_badges/measure?project=RHEAGROUP_SysML2.NET&metric=duplicated_lines_density)](https://sonarcloud.io/summary/new_code?id=RHEAGROUP_SysML2.NET)
[![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=RHEAGROUP_SysML2.NET&metric=ncloc)](https://sonarcloud.io/summary/new_code?id=RHEAGROUP_SysML2.NET)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=RHEAGROUP_SysML2.NET&metric=sqale_rating)](https://sonarcloud.io/summary/new_code?id=RHEAGROUP_SysML2.NET)
[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=RHEAGROUP_SysML2.NET&metric=reliability_rating)](https://sonarcloud.io/summary/new_code?id=RHEAGROUP_SysML2.NET)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=RHEAGROUP_SysML2.NET&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=RHEAGROUP_SysML2.NET)
[![Technical Debt](https://sonarcloud.io/api/project_badges/measure?project=RHEAGROUP_SysML2.NET&metric=sqale_index)](https://sonarcloud.io/summary/new_code?id=RHEAGROUP_SysML2.NET)
[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=RHEAGROUP_SysML2.NET&metric=vulnerabilities)](https://sonarcloud.io/summary/new_code?id=RHEAGROUP_SysML2.NET)
[![Total alerts](https://img.shields.io/lgtm/alerts/g/RHEAGROUP/SysML2.NET.svg?logo=lgtm&logoWidth=18)](https://lgtm.com/projects/g/RHEAGROUP/SysML2.NET/alerts/)

## Installation

The packages are available on Nuget at:

project                                                                                             | Nuget
--------------------------------------------------------------------------------------------------- | ------------
[SysML2.NET](https://www.nuget.org/packages/SysML2.NET)                                             | [![NuGet Badge](https://buildstats.info/nuget/SysML2.NET)](https://buildstats.info/nuget/SysML2.NET)
[SysML2.NET.Serializer.Json](https://www.nuget.org/packages/SysML2.NET.Serializer.Json)             | [![NuGet Badge](https://buildstats.info/nuget/SysML2.NET.Serializer.Json)](https://buildstats.info/nuget/SysML2.NET.Serializer.Json)
[SysML2.NET.REST](https://www.nuget.org/packages/SysML2.NET.REST)                                   | [![NuGet Badge](https://buildstats.info/nuget/SysML2.NET.REST)](https://buildstats.info/nuget/SysML2.NET.REST)
[SysML2.NET.DAL](https://www.nuget.org/packages/SysML2.NET.DAL)                                   | [![NuGet Badge](https://buildstats.info/nuget/SysML2.NET.DAL)](https://buildstats.info/nuget/SysML2.NET.DAL)

## Build Status

GitHub actions are used to build and test the library

Branch | Build Status
------- | :------------
Master | ![Build Status](https://github.com/RHEAGROUP/SysML2.NET/actions/workflows/CodeQuality.yml/badge.svg?branch=master)
Development | ![Build Status](https://github.com/RHEAGROUP/SysML2.NET/actions/workflows/CodeQuality.yml/badge.svg?branch=development)

## RELEASE

>NOTE: SPECIFIC DETAILS FOR THIS 2022-09 BRANCH TO SUPPORT GSEF DEVELOPMENT

dotnet pack -c Release -o .

dotnet nuget push SysML2.NET.0.10.0-GSEF.nupkg --source https://nuget.pkg.github.com/RHEAGROUP/index.json --api-key "your-api-key"
dotnet nuget push SysML2.NET.Dal.0.10.0-GSEF.nupkg --source https://nuget.pkg.github.com/RHEAGROUP/index.json --api-key "your-api-key"
dotnet nuget push SysML2.NET.REST.0.10.0-GSEF.nupkg --source https://nuget.pkg.github.com/RHEAGROUP/index.json --api-key "your-api-key"
dotnet nuget push SysML2.NET.Serializer.Json.0.10.0-GSEF.nupkg --source https://nuget.pkg.github.com/RHEAGROUP/index.json --api-key "your-api-key"

# License

The SysML2.NET libraries and reference web-application are provided to the community under the Apache License 2.0. The solution contains files that contain information about the SysML2 metamodel which are distributed with the [GNU Lesser General Public License (LGPL) v3.0](https://opensource.org/licenses/LGPL-3.0).
