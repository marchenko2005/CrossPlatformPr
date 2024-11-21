# Встановлення Chocolatey
Set-ExecutionPolicy Bypass -Scope Process -Force
iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))

# Встановлення .NET SDK 8.0
choco install dotnet-8.0-sdk -y

# Оновлення змінних середовища
refreshenv

# Перевірка версії .NET
dotnet --version

# Перехід до проекту
cd C:/project

# Запуск LAB1
dotnet run --project App -- --lab lab1 --input MMarchenkoLib\LAB1\INPUT.TXT --output MMarchenkoLib\LAB1\OUTPUT.TXT
