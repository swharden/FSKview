:: delete old builds
del *.zip

:: rebuild FSKview
rmdir /s /q ..\..\src\FSKview\bin

::dotnet build --configuration Release ..\..\src\FSKview
"C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\amd64\MSBuild.exe" "C:\Users\scott\Documents\GitHub\FSKview\src\FSKview.sln" /property:Configuration=Release

:: clear old builds
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