FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app


COPY src/Contracts/Contracts.csproj src/Contracts/
COPY src/WalletService/WalletService.csproj src/WalletService/


RUN dotnet restore src/WalletService/WalletService.csproj


COPY src/Contracts src/Contracts
COPY src/WalletService src/WalletService


RUN dotnet publish src/WalletService -c Release -o /app/out

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "WalletService.dll"]
