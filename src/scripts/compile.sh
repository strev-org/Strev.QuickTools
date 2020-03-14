#!/usr/bin/env bash

this_script="$0"
scripts_dir=$(dirname $(readlink -f "${this_script}"))

. "${scripts_dir}/projectvars.sh"

VERSION=$(cat "${VERSION_FILE}")

mkdir -p "$(dirname ${VERSION_PROPS_FILE})"

sed -e "s/${VERSION_PATTERN}/${VERSION}/g;" "${VERSION_TEMPLATE_FILE}" > "${VERSION_PROPS_FILE}"

pushd . 2>&1 >/dev/null

cd "${scripts_dir}/.."

"${scripts_dir}/nuget" restore

RESULT=KO

"${MSBUILD}" 

"${MSBUILD}" "-t:Build" "-p:Configuration=Debug"   "Strev.QuickTools.sln" && \
"${MSBUILD}" "-t:Build" "-p:Configuration=Release" "Strev.QuickTools.sln" && RESULT=OK

popd 2>&1 >/dev/null

[ "${RESULT}" == "OK" ]
