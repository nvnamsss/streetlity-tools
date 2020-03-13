#!/usr/bin/env bash

mode=""
output=""

if [ ! "$1" == "" ]; then
    mode="-c ${1}"
fi

if [ ! "$2" == "" ]; then
    output="-o ${2}"
fi

# echo $build $output
dotnet build $mode $output

