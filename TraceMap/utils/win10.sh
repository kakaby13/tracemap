#!/bin/bash

cd ../Cli/TraceMap.Cli.Win10
dotnet publish -r win10-x64 --configuration Release -p:PublishSingleFile=true -o ../../


