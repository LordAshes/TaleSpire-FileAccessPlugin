# File Access Plugin

This unofficial TaleSpire plugin for providing a standardized method of accessing
both local and internet resources (files). Plugins that use this plugin for file
access will be able to access both local file resources and internet resources,
without having to have different code, just by specifying a local or internet
source. When using local files this plugin automatically searches plugin folders
and the common folder (if present) for content which means the parent plugin does
not need to worry about such details.

## Change Log

1.2.3: Catalog option to avoid extra info so that results can be used in code

1.2.0: Supports direct path option

1.1.1: Better location of the plugins folder in case manual install was done.

1.1.0: Fixed issue with forced R2ModMan profile. Plugin now auto detects profile.

1.0.0: Initial release

## Install

Install using R2ModMan or similar and reference the plugin DLL when building the
parent plugin. This gives access to the LordAshes.FileAccessPlugin namespace with
most of the file related functions under the sub-namespace File, some image loading
function under the sub-namepace Image and an assetBundle loader under the sub-namespace
of AssetBundle. 

## Usage

```C#
GetProtocol(string source)
```

Gets the protocol portion of the source. Empty string if the source is a local file or
the protocol poriton (e.g. http or https) if it is a URL.

The following sub-namespaces are available...

### File

```C#
AppendAllText(string source, string content)
string ReadAllText(string source)
WriteAllText(string source, string content)
```
Functions for appending, reading and writing the contents of the source as a single string.
Note: When using a URL for the source, append and write functions perform an upload.

```C#
AppendAllLines(string source, string[] content)
string[]  ReadAllLines(string source)
WriteAllLines(string source, string[] content)
```
Functions for appending, reading and writing the contents of the source as an string array
of lines. Each line is the source is an individual entry in the array.
Note: When using a URL for the source, append and write functions perform an upload.

```C#
byte[] ReadAllBytes(string source)
WriteAllBytes(string source, byte[] content)
```
Functions for reading and writing the contents of the source as an byte array.
Note: When using a URL for the source, append and write functions perform an upload.

```C#
bool Exists(source)
```
Determines if the source exists. For local files it must exists in the plugin directories
or the common folder. For URL the resource needs to exists at the give source.

```C#
string[] Find(source)
```
When the source is a local file, finds where the source is located. Returns a string array
of resources that match the given source using a "contains" concept. When using any of the
append, reaad or write functions from this plugin, it is not necessary to use Find because
the functions take care of that on their own. However, if using a different function to
access the file, the Find function can be used to obtain the complete file path and namepace
of the file.

```C#
void SetCacheType(CacheType setting)
```
Used to set the cache setting for the assets file list. Four options are possible:
NoCacheCustomData = Only files in the CustomData sub-folders are collected. Contents are
re-generated each time they are needed (allows adding contents on the fly).
NoCacheCustomData = Only files in the CustomData sub-folders are collected. Contents are
re-generated each time they are needed (allows adding contents on the fly).
NoCacheFullListing = All files in plugin folders are added. Contents are re-generated
each time they are needed (allows adding contents on the fly).
CacheCustomData = Only files in the CustomData sub-folders are collected. Contents are
generated once at startup (does not allow adding contents on the fly).
CacheFullListing = All files in plugin folders are added. Contents are generated once at
startup (does not allow adding contents on the fly).

```C#
string[] Catalog(Bool extraData)
```
Returns a sorted list of all available assets sorted by type and then plugin

### Image

```C#
Texture2D LoadTexture(string source)
```
Returns a texture loaded with the contents of the source

```C#
Sprite LoadSprite(string source)
```
Returns a sprite loaded with the contents of the source. Typically used for icons.

### AssetBundle

```C#
AssetBundle Load(string source)
```
Returns an assetBundle loaded from the specified source

## Local File Search

When using local files, the source can specify a resource name or a folder and
resource name and the plugin will automatically search the plugin folders for
the specified resource. This allows the parent plugins to specify resources by name
without having to concern themselves with figuring out which plugin contains the
resource. If the installaton has the TaleSpire_CustomData folder in the main
TaleSpire game folder then this folder is also used to locate assets.

This means that local access (append, read and write) is limited to writing in
the plugin folders and the common folder (if it exists).

This also means that append and write functions only work with existing files.
Thus the write functions cannot be used to make a new file but can be used to
rewrite an existing file. 

## Full Path

Full path sources are recognized by having a colon as a second characters since
such sources start with a drive letter and then are followed by a colon and the
path and file name. When such sources are provided, they are used as is without
looking them up in the files list or on the internet.
