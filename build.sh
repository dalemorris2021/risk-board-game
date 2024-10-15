dotnet clean ./Risk
dotnet test ./Risk.Tests
success=$(echo $?)
if [ $success -eq 0 ]
then
  dotnet build ./Risk --self-contained true -o ./out
fi
  
