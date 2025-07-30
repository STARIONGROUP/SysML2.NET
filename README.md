# SysML2.NET

SysML2.NET is a .NET implementation of the [OMG SysML2 specification](https://github.com/Systems-Modeling/SysML-v2-Release). SysML2.NET provides a number of libraries in the form on an SDK as well as reference implementation such as a web-application and REST API.

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=STARIONGROUP_SysML2.NET&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=STARIONGROUP_SysML2.NET)
[![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=STARIONGROUP_SysML2.NET&metric=code_smells)](https://sonarcloud.io/summary/new_code?id=STARIONGROUP_SysML2.NET)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=STARIONGROUP_SysML2.NET&metric=coverage)](https://sonarcloud.io/summary/new_code?id=STARIONGROUP_SysML2.NET)
[![Duplicated Lines (%)](https://sonarcloud.io/api/project_badges/measure?project=STARIONGROUP_SysML2.NET&metric=duplicated_lines_density)](https://sonarcloud.io/summary/new_code?id=STARIONGROUP_SysML2.NET)
[![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=STARIONGROUP_SysML2.NET&metric=ncloc)](https://sonarcloud.io/summary/new_code?id=STARIONGROUP_SysML2.NET)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=STARIONGROUP_SysML2.NET&metric=sqale_rating)](https://sonarcloud.io/summary/new_code?id=STARIONGROUP_SysML2.NET)
[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=STARIONGROUP_SysML2.NET&metric=reliability_rating)](https://sonarcloud.io/summary/new_code?id=STARIONGROUP_SysML2.NET)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=STARIONGROUP_SysML2.NET&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=STARIONGROUP_SysML2.NET)
[![Technical Debt](https://sonarcloud.io/api/project_badges/measure?project=STARIONGROUP_SysML2.NET&metric=sqale_index)](https://sonarcloud.io/summary/new_code?id=STARIONGROUP_SysML2.NET)
[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=STARIONGROUP_SysML2.NET&metric=vulnerabilities)](https://sonarcloud.io/summary/new_code?id=STARIONGROUP_SysML2.NET)

## Installation

The packages are available on Nuget at:

project                                                                                             | Nuget
--------------------------------------------------------------------------------------------------- | ------------
[SysML2.NET](https://www.nuget.org/packages/SysML2.NET)                                             | ![NuGet Version](https://img.shields.io/nuget/v/SysML2.NET)
[SysML2.NET.Serializer.Json](https://www.nuget.org/packages/SysML2.NET.Serializer.Json)             | ![NuGet Version](https://img.shields.io/nuget/v/SysML2.NET.Serializer.Json)
[SysML2.NET.Serializer.Dictionary](https://www.nuget.org/packages/SysML2.NET.Serializer.Dictionary) | ![NuGet Version](https://img.shields.io/nuget/v/SysML2.NET.Serializer.Dictionary)
[SysML2.NET.REST](https://www.nuget.org/packages/SysML2.NET.REST)                                   | ![NuGet Version](https://img.shields.io/nuget/v/SysML2.NET.REST)
[SysML2.NET.DAL](https://www.nuget.org/packages/SysML2.NET.DAL)                                     | ![NuGet Version](https://img.shields.io/nuget/v/SysML2.NET.DAL)
[SysML2.NET.OGM](https://www.nuget.org/packages/SysML2.NET.OGM)                                     | ![NuGet Version](https://img.shields.io/nuget/v/SysML2.NET.OGM)

The reference web-application is available on [docker-hub](https://hub.docker.com/r/stariongroup/sysml2.net.viewer). A demo-version is hosted at http://viewer.sysml2.net.

The generated HTML based meta-model documentation is availabe on [docker-hub](https://hub.docker.com/r/stariongroup/sysml2.net.docs). A live version is available at https://modeldocs.sysml2.net. 

## Build Status

GitHub actions are used to build and test the library

Branch | Build Status
------- | :------------
Master | ![Build Status](https://github.com/STARIONGROUP/SysML2.NET/actions/workflows/CodeQuality.yml/badge.svg?branch=master)
Development | ![Build Status](https://github.com/STARIONGROUP/SysML2.NET/actions/workflows/CodeQuality.yml/badge.svg?branch=development)

# License

The SysML2.NET libraries and reference web-application are provided to the community under the Apache License 2.0. The solution contains files that contain information about the SysML2 metamodel which are distributed with the [GNU Lesser General Public License (LGPL) v3.0](https://opensource.org/licenses/LGPL-3.0).

# Contributions

Contributions to the code-base are welcome. However, before we can accept your contributions we ask any contributor to sign the Contributor License Agreement (CLA) and send this digitaly signed to s.gerene@stariongroup.eu. You can find the CLA's in the CLA folder.
