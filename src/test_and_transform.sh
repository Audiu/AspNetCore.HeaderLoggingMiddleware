#/bin/sh

dotnet test --logger "trx" --results-directory /test-results
export PATH="$PATH:/root/.dotnet/tools"
dotnet tool install -g trx2junit
trx2junit /test-results/*.trx
