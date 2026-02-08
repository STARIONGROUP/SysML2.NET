#!/bin/bash

# Exit on error
set -e

# Ensure version is passed
if [[ -z "$1" ]]; then
  echo "Usage: $0 <version>"
  echo "Example: $0 x.y.z"
  exit 1
fi

VERSION="$1"

echo "Building local Docker image for version: $VERSION"

docker build \
  -f SysML2.NET.Viewer/Dockerfile \
  -t stariongroup/sysml2.net.viewer:latest \
  -t stariongroup/sysml2.net.viewer:$VERSION \
  .

echo "Build complete."
echo "Tags: latest, $VERSION"