#!/usr/bin/env bash

this_script="$0"
scripts_dir=$(dirname $(readlink -f "${this_script}"))

. "${scripts_dir}/projectvars.sh"

BUILD_DATE=$(date +"%Y%m%d-%H%MS")
VERSION="$(cat "${VERSION_FILE}")"
NUGET_PACKAGE_VERSION="${VERSION}"
if [ "${NUGET_PACKAGE_VERSION: -8}" == "SNAPSHOT" ]
then
    NUGET_PACKAGE_VERSION="${VERSION::-8}${BUILD_DATE}"
fi

create_nuget() {
    NUGET_PACKAGE_NAME="$1"
    NUGET_PACKAGE_PATH="..\\\\..\\\\bin\\\\${NUGET_PACKAGE_NAME}"
    NUGET_PACKAGE_DESCRIPTION_ADD="$2"
    NUGET_PACKAGE_TARGET1="$3"
    NUGET_PACKAGE_TARGET2="$4"

    NUGET_REPOURL="https://api.nuget.org/v3/index.json"
    PACKAGE_FILENAME="${NUGET_PACKAGE_NAME}.${NUGET_PACKAGE_VERSION}.nupkg"
    FULL_PACKAGE_FILENAME="${PACKAGES_DIR}/${PACKAGE_FILENAME}"
    NUGET_PACKAGE_NAME_PATTERN="{package_name}"
    NUGET_PACKAGE_VERSION_PATTERN="{version}"
    NUGET_PACKAGE_DESCRIPTION_ADD_PATTERN="{description_add}"
    NUGET_PACKAGE_PATH_PATTERN="{package_path}"
    NUGET_PACKAGE_TARGET1_PATTERN="{target1}"
    NUGET_PACKAGE_TARGET2_PATTERN="{target2}"
    
    NUSPEC_TEMPLATE="${TEMPLATES_DIR}/template.nuspec"
    NUSPEC_FILE="${TEMP_BUILD_DIR}/${NUGET_PACKAGE_NAME}.${NUGET_PACKAGE_VERSION}.nuspec"
    
    pushd . 2>&1 >/dev/null

    mkdir -p "${PACKAGES_DIR}"
    mkdir -p "${TEMP_BUILD_DIR}"
    cd "${TEMP_BUILD_DIR}"

    sed -e "s/${NUGET_PACKAGE_VERSION_PATTERN}/${NUGET_PACKAGE_VERSION}/g; s/${NUGET_PACKAGE_NAME_PATTERN}/${NUGET_PACKAGE_NAME}/g; s/${NUGET_PACKAGE_DESCRIPTION_ADD_PATTERN}/${NUGET_PACKAGE_DESCRIPTION_ADD}/g; s/${NUGET_PACKAGE_PATH_PATTERN}/${NUGET_PACKAGE_PATH}/g; s/${NUGET_PACKAGE_TARGET1_PATTERN}/${NUGET_PACKAGE_TARGET1}/g; s/${NUGET_PACKAGE_TARGET2_PATTERN}/${NUGET_PACKAGE_TARGET2}/g; s/^.*__remove_me__.*$//g;" "${NUSPEC_TEMPLATE}" > "${NUSPEC_FILE}" 
    "${scripts_dir}/nuget" pack "${NUSPEC_FILE}"

    mv "${TEMP_BUILD_DIR}/${PACKAGE_FILENAME}" "${FULL_PACKAGE_FILENAME}"

    popd 2>&1 >/dev/null
    
}

create_nuget "Strev.QuickTools" "" net35 netstandard2.0
create_nuget "Strev.QuickTools.Plugin" "" net35 netstandard2.0
create_nuget "Strev.QuickTools.WPF" "" net35 net472
