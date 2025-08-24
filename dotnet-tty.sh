#!/bin/sh
podman run --rm --interactive --tty \
  --volume "$PWD":/usr/src/app:z \
  --workdir /usr/src/app \
  rust /usr/bin/env bash
