#!/usr/bin/env bash

this_script="$0"
scripts_dir=$(dirname $(readlink -f "${this_script}"))
VERSION="$1"

. "${scripts_dir}/projectvars.sh"

echo -n "${VERSION}" > "${VERSION_FILE}"
