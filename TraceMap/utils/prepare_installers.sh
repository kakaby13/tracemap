#!/bin/bash

sh linux.sh
mv ../*.deb ./output/tracemap.deb

sh win10.sh
mv ../*.exe ./output/tracemap.exe
cp ./install.ps1 ./output
cd ./output
zip tracemap.zip tracemap.exe install.ps1

rm install.ps1
rm tracemap.exe