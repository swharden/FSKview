if exist ..\..\src\FSKview\bin rmdir /s /q ..\..\src\FSKview\bin

dotnet build --configuration Release ..\..\src\FSKview

move ..\..\src\FSKview\bin\Release\net6.0-windows ..\..\src\FSKview\bin\Release\FSKview-win10
move ..\..\src\FSKview\bin\Release\net48 ..\..\src\FSKview\bin\Release\FSKview-win7

powershell "Compress-Archive -Force ..\..\src\FSKview\bin\Release\FSKview-win10 FSKview-VERSION-win10.zip"
powershell "Compress-Archive -Force ..\..\src\FSKview\bin\Release\FSKview-win7 FSKview-VERSION-win7.zip"

pause