:: delete old builds
del *.zip

:: rebuild QRSS uploader
rmdir /s /q ..\..\..\QRSS-Uploader\src\QrssUploader\bin\Release
dotnet build --configuration Release ..\..\..\QRSS-Uploader\src\QrssUploader

:: rebuild FSKview
rmdir /s /q ..\..\src\FSKview\bin

dotnet build --configuration Release ..\..\src\FSKview

:: clera debug files
del ..\..\src\FSKview\bin\Release\*.xml
del ..\..\src\FSKview\bin\Release\*.pdb
del ..\..\src\FSKview\bin\Release\*.config

:: copy tools into build folder
copy tools\* ..\..\src\FSKview\bin\Release

:: zip
cd ..\..\src\FSKview\bin\
move Release FSKview
powershell "Compress-Archive FSKview FSKview-VERSION.zip"
explorer .\
::cd ..\..\src\FSKview\bin\Release
::tar.exe -a -c -f FSKview-VERSION.zip *
::move FSKview-VERSION.zip ..\..\..\..\dev\devops

pause