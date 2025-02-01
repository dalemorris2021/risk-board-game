dotnet clean ./src/Risk
dotnet test ./src/Risk.Tests
success=$(echo $?)
if [ $success -eq 0 ]
then
  dotnet build ./src/Risk --self-contained true -o ./out
fi
  
