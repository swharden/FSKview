if exist ..\..\src\FSKview\bin rmdir /s /q ..\..\src\FSKview\bin

dotnet build --configuration Release ..\..\src\FSKview

powershell "Compress-Archive ..\..\src\FSKview\bin\Release\net6.0-windows FSKview-VERSION.zip"

pause