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

echo "pulling latest version of nginx:alpine"
docker pull nginx:alpine

echo "Building local Docker image for version: $VERSION"

docker build \
  -f HtmlDocs/Dockerfile \
  -t stariongroup/sysml2.net.docs:latest \
  -t stariongroup/sysml2.net.docs:$VERSION \
  .

echo "Build complete."
echo "Tags: latest, $VERSION"