#!/usr/bin/env bash

. "${scripts_dir}/vswars.sh"

VERSION_PATTERN="{version}"
VERSION_TEMPLATE_FILE="${scripts_dir}/../templates/template-version.props"
VERSION_PROPS_FILE="${scripts_dir}/../../obj/Version.props"
VERSION_FILE="${scripts_dir}/../../VERSION"
PACKAGES_DIR="${scripts_dir}/../../bin/packages"
TEMPLATES_DIR="${scripts_dir}/../templates"
TEMP_BUILD_DIR="${scripts_dir}/../../obj/build"
