cmake_minimum_required(VERSION 3.20)
project(bar)

# Define paths
cmake_path(APPEND sqlite_src "${CMAKE_CURRENT_SOURCE_DIR}" "sqlite-amalgamation-3380500")
cmake_path(APPEND sqlite_c "${sqlite_src}" "sqlite3.c")
cmake_path(APPEND sqlite_h_from "${sqlite_src}" "sqlite3.h")
cmake_path(APPEND sqlite_h_to "${CMAKE_CURRENT_BINARY_DIR}" "include" "bar.h")

# Unreal only supports x86_64
if(APPLE)
    set(CMAKE_OSX_DEPLOYMENT_TARGET "10.15")
    set(CMAKE_OSX_ARCHITECTURES "x86_64")
endif()

# Create a static library
add_library(bar STATIC ${sqlite_c})
configure_file(${sqlite_h_from} ${sqlite_h_to} COPYONLY)
