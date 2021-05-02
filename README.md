# App Tracking Transparency for Unity Plugin

## Overview
This repository contains a plugin to support the App Tracking Transparency framework for iOS in Unity 3D.

Introduced on iOS 14.0, Apple wants all developers that want to retrieve the IDFA for a user, to ask permission for it. If the user does not provide permission, an anonymous IDFA is provided instead (`00000000-0000-0000-0000-000000000000`)

Starting from iOS 14.0, the API was made available for developers, but the restriction was not applied yet.

Starting from iOS 14.5, Apple started enforcing this rule, so all users have to give their permission to obtain the IDFA.

IMAGE OF POPUP

This plugin supports the following platforms:
* **iOS**
* **tvOS** (Experimental)


## Features
- Support for iOS
- Support for tvOS (Experimental)
- Support to get the current App Tracking Transparency Status to get the IDFA
- Support to request authorization for the IDFA
- Editor implementation for the feature, imitating the native implementation.
- Configurable automatic postprocessing, add required frameworks and required Info.plist entries


## Installation

> Current version is v0.5.0

Here is a list of available options to install the plugin

### Unity Package Manager with Git URL

Just add this line to the `Packages/manifest.json` file of your Unity Project:

```json
"dependencies": {
    "com.lupidan.unity-apptrackingtransparency": "https://github.com/lupidan/unity-apptrackingtransparency.git?path=/com.lupidan.unity-apptrackingtransparency#v0.5.0"
}
```

### Unity Package File
1. Download the most recent Unity package release [here](https://github.com/lupidan/unity-apptrackingtransparency/releases)
2. Import the downloaded Unity package in your app.

