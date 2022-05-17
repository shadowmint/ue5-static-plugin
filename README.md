# Unreal 5: Static library example

Basic example of using self-compiling static native code plugins.

Why? ...because the approach in https://github.com/shadowmint/ue4-static-plugin turned out to be
unsustainable; you can't sit there compiling all your dependencies by hand every time you install
a plugin.

These plugins are distributed as *source only*, but will recompile themselves on the fly.

So really, as long as the tools are installed, you can just drop the plugin in the `Plugins`
folder and run the project. <3

## Libbar

The plugin is a single self contained unit that:

- Builds a static C library using cmake (sqlite)
- Exposes the C api as a C++ header
- Exposes a blueprint calling the C library
- Compiles itself (assuming you have cmake installed)

## Libfoo

The plugin is a single self contained unit that:

- Contains a rust library with a C api
- Exposes the C api as a C++ header
- Exposes the C api as a blueprint
- Compiles itself (assuming you have cargo installed)

## Usage

Just copy it into the plugins folder and reference it in your project.

### Libbar

You need to have cmake installed and available on the global path.

### Libfoo

You need to have rust installed for it to work.

If you're on a mac, you should ensure that have have a matching architecture installed by running:

    rustup target add x86_64-apple-darwin

There's a map in the content folder with an example.

## Notes

Unreal doesn't support apple silicon; for rust we use:

`--target x86_64-apple-darwin`, but you have to install the correct toolchain.

For cmake its a builtin feature:

```
if(APPLE)
    set(CMAKE_OSX_DEPLOYMENT_TARGET "10.15")
    set(CMAKE_OSX_ARCHITECTURES "x86_64")
endif()
```

Invoking cmake using `cmake --build` is *the only* portable way of building code using cmake.

You can swap this for using some other platform specific tool if you don't like it, and don't care about other platforms.

For details on auto-generating hpp headers in rust, see https://michael-f-bryan.github.io/rust-ffi-guide/cbindgen.html and `build.rs`

Note that irritatingly UBT will automatically scan your files and try to compile them if you have your library in the 'Source`
folder; therefore the actual library has to sit at a higher level in a folder I've arbitrarily called 'library'. 
