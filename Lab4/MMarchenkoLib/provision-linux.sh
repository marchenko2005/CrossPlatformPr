
# Оновлення системи та встановлення .NET SDK
sudo apt-get update
sudo apt-get install -y wget apt-transport-https
wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo apt-get update
sudo apt-get install -y dotnet-sdk-8.0

# Перехід до директорії проекту
cd /vagrant

# Запуск LAB1
dotnet run --project App -- --lab lab1 --input MMarchenkoLib/LAB1/INPUT.TXT --output MMarchenkoLib/LAB1/OUTPUT.TXT
