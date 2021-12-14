:: delete old builds
del *.zip

:: delete old releases
if exist ..\..\src\FSKview\bin rmdir /s /q ..\..\src\FSKview\bin

:: rebuild release
dotnet build --configuration Release ..\..\src\FSKview

:: zip
cd ..\..\src\FSKview\bin\Release
move net6.0-windows FSKview
powershell "Compress-Archive FSKview FSKview-VERSION.zip"
explorer .\

pause