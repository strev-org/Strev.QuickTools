#!/usr/bin/env bash

this_script="$0"
scripts_dir=$(dirname $(readlink -f "${this_script}"))

. "${scripts_dir}/projectvars.sh"

VERSION=$(cat "${VERSION_FILE}")

mkdir -p "$(dirname ${VERSION_PROPS_FILE})"

sed -e "s/${VERSION_PATTERN}/${VERSION}/g;" "${VERSION_TEMPLATE_FILE}" > "${VERSION_PROPS_FILE}" 
