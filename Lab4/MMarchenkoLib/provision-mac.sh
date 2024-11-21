
# Встановлення .NET SDK через Homebrew
brew install --cask dotnet-sdk

# Перехід до проекту
cd /vagrant

# Запуск LAB1
dotnet run --project App -- --lab lab1 --input MMarchenkoLib/LAB1/INPUT.TXT --output MMarchenkoLib/LAB1/OUTPUT.TXT
