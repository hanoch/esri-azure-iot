# Azure IoT Connector

ArcGIS 10.4 GeoEvent Extension for Server Azure IoT Connector.

## Features
* Azure IoT Connector

## Instructions

### Prerequistes

Build Apache Qpid (required by Azure IoT Service SDK)

1. Download the source from GitHub: (https://github.com/apache/qpid-proton)
2. run 'mvn install' on the **root** directory
3. Done

Build Azure IoT Service SDK

1. Download the source from GitHub: (https://github.com/Azure/azure-iot-sdks)
2. Switch to the **develop** branch
3. run 'mvn install' on the **java\service\iothub-service-sdk** directory
4. Done

Building the source code:

1. Make sure Maven and ArcGIS GeoEvent Extension SDK are installed on your machine.
2. Run 'mvn install -Dcontact.address=[YourContactEmailAddress]'

## Requirements

* ArcGIS GeoEvent Extension for Server (Certified with version 10.4.x).
* ArcGIS GeoEvent Extension SDK.
* Java JDK 1.8 or greater.
* Maven.

## Resources

* [ArcGIS GeoEvent Extension Gallery](http://links.esri.com/geovent-gallery) 
* [ArcGIS GeoEvent Extension for Server Resources](http://links.esri.com/geoevent)
* [ArcGIS Blog](http://blogs.esri.com/esri/arcgis/)
* [twitter@esri](http://twitter.com/esri)

## Issues

Find a bug or want to request a new feature?  Please let us know by submitting an issue.

## Contributing

Esri welcomes contributions from anyone and everyone. Please see our [guidelines for contributing](https://github.com/esri/contributing).

## Licensing
Copyright 2013 Esri

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

   http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

A copy of the license is available in the repository's [license.txt](license.txt?raw=true) file.

[](ArcGIS, GeoEvent, Processor)
[](Esri Tags: ArcGIS GeoEvent Extension for Server)
[](Esri Language: Java)
