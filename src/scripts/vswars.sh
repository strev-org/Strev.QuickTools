#!/usr/bin/env bash

VSWHERE=""
for vswhere_test in "C:\Program Files\Microsoft Visual Studio\Installer\vswhere.exe" "C:\Program Files (x86)\Microsoft Visual Studio\Installer\vswhere.exe"
do
    [ -f "${vswhere_test}" ] && VSWHERE="${vswhere_test}"
done

MSVSPATH="$("${VSWHERE}" -latest -products '*' requires Microsoft.Component.MSBuild | grep installationPath | sed -e 's/.*: //')"
if [ -n "${MSVSPATH}" ]
then
    MSBUILD="${MSVSPATH}\MSBuild\15.0\Bin\MSBuild.exe"
    if [ ! -f "${MSBUILD}" ]
    then
        MSBUILD="${MSVSPATH}\MSBuild\Current\Bin\MSBuild.exe"
    fi
fi


# echo "[${VSWHERE}]"
# echo "[${MSVSPATH}]"
# echo "[${MSBUILD}]"
