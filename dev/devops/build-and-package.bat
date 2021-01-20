:: delete old builds
del *.zip

:: rebuild FSKview
rmdir /s /q ..\..\src\FSKview\bin

::dotnet build --configuration Release ..\..\src\FSKview
"C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\amd64\MSBuild.exe" "C:\Users\scott\Documents\GitHub\FSKview\src\FSKview.sln" /property:Configuration=Release

:: clear old builds
del ..\..\src\FSKview\bin\Release\net5.0-windows\*.xml
del ..\..\src\FSKview\bin\Release\net5.0-windows\*.pdb
del ..\..\src\FSKview\bin\Release\net5.0-windows\*.config

:: zip
cd ..\..\src\FSKview\bin\Release
move net5.0-windows FSKview
powershell "Compress-Archive FSKview FSKview-VERSION.zip"
explorer .\
::cd ..\..\src\FSKview\bin\Release
::tar.exe -a -c -f FSKview-VERSION.zip *
::move FSKview-VERSION.zip ..\..\..\..\dev\devops

pause