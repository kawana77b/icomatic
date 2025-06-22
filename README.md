# icomatic

Generator for batch generation of icon sets for websites, etc., from image file or URL.

It is a so-called common tool. Basically, it was created for me personally.

![demo](/docs/images/demo.png)

## install

It is built for Windows x64 and Linux x64.
Binaries are available on the [release page](https://github.com/kawana77b/icomatic/releases).

## usage

Displays a set of icons in the current folder. Prompts you to generate an icon set with your selections.

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

Currently, the format is validated only with a simple extension check.

- png
- jpg
- bmp
- ico
- svg (Read)

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
