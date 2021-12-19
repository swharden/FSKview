if exist ..\..\src\FSKview\bin rmdir /s /q ..\..\src\FSKview\bin

dotnet build --configuration Release ..\..\src\FSKview

powershell "Compress-Archive ..\..\src\FSKview\bin\Release\net6.0-windows FSKview-win10-VERSION.zip"
powershell "Compress-Archive ..\..\src\FSKview\bin\Release\net48 FSKview-win7-VERSION.zip"

pause