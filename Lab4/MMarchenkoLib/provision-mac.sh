
brew install --cask dotnet-sdk

cd /vagrant

dotnet run --project App -- --lab lab1 --input MMarchenkoLib/LAB1/INPUT.TXT --output MMarchenkoLib/LAB1/OUTPUT.TXT
