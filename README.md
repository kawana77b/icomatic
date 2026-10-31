# icomatic

Generator for batch generation of icon sets for websites, etc., from image file or URL.

It is a so-called common tool. Basically, it was created for me personally.

![demo](/docs/images/demo.png)

## install

It is built for Windows x64 and Linux x64.
Binaries are available on the [release page](https://github.com/kawana77b/icomatic/releases).

## development

See the [architecture overview](docs/architecture.md) for the code structure and processing flow.

Install the .NET 10.0.402 SDK or a later stable .NET 10.0 SDK. `global.json` selects
the latest installed compatible feature band and patch, excluding prereleases.
Self-contained releases use .NET runtime 10.0.14.

```bash
dotnet restore Icomatic.sln
dotnet build Icomatic.sln --no-restore -c Release
dotnet test Icomatic.Test/Icomatic.Test.csproj --no-build --no-restore -c Release
dotnet publish Icomatic/Icomatic.csproj --no-restore -r win-x64 -c Release
dotnet publish Icomatic/Icomatic.csproj --no-restore -r linux-x64 -c Release
```

The xUnit project, `Icomatic.Test`, covers resizing, transparent masks, output
definitions, and input validation. It generates images in memory and substitutes
interactive prompts, so no image assets or network access are required to run the
tests after restoring dependencies.

## usage

Generates an icon set in the current folder. Prompts for a style and template,
then asks for confirmation before generating files.

```bash
icomatic foo.png
```

Place the icon set in the public folder. If not, it will be created.

```bash
icomatic foo.png --dir public
```

Displays a list of template types.

```bash
icomatic list
```

Displays template details.

```bash
icomatic info pwa
```

## supported formats

Local input files must exist and have one of the following extensions
(case-insensitive). Image decoding happens after this initial validation.

- png
- jpg / jpeg
- webp
- bmp
- ico
- svg (input only)
- gif
- tif / tiff

HTTP/HTTPS inputs are checked as URLs without requiring a recognized image
extension; the downloaded content must still be decodable as an image.

Output dimensions and formats are defined by the selected template. The `plane`
style uses the template's format. The `round` and `circle` styles always write PNG
data to preserve transparency, but keep the template's filename extension.

## template list

```
 --------------------------------------------------------------------
 | Name          | Description                                      |
 --------------------------------------------------------------------
 | android       | Android app icons for different screen densities |
 --------------------------------------------------------------------
 | desktop       | Desktop wallpapers for various resolutions       |
 --------------------------------------------------------------------
 | ios           | iOS app icons for various devices and contexts   |
 --------------------------------------------------------------------
 | mobile        | Mobile wallpapers for different device sizes     |
 --------------------------------------------------------------------
 | pwa           | Progressive Web App icons                        |
 --------------------------------------------------------------------
 | social        | Social media platform images and profiles        |
 --------------------------------------------------------------------
 | web           | Web favicon and touch icons                      |
 --------------------------------------------------------------------
 | windows       | Windows desktop application icons                |
 --------------------------------------------------------------------
 | windows-tiles | Windows Start Menu tile icons                    |
 --------------------------------------------------------------------
```
