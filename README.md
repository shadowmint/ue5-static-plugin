# Unreal 5: Static library example

This is a basic plugin showing how to have a binary module that builds itself as part of the UBT build.

The plugin is a single self contained unit that:

- Contains a rust library with a C api
- Exposes the C api as a C++ header
- Exposes the C api as a blueprint
- Compiles itself (assuming you have cargo installed)

## Usage

Just copy it into the plugins folder and reference it in your project.

You need to have rust installed for it to work.

If you're on a mac, you should ensure that have have a matching architecture installed by running:

    rustup target add x86_64-apple-darwin

There's a map in the content folder with an example.

## How does it work?

1) We build the rust target as part of `Libfoo.Build.cs`
 
2) The build runs and `build.rs` generates an include file in the build folder.

That's it.

### Notes

`--target x86_64-apple-darwin` is required because UE doesn't support apple silicon yet.

For details on auto-generating headers, see https://michael-f-bryan.github.io/rust-ffi-guide/cbindgen.html and `build.rs`