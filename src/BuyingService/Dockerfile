FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /app
EXPOSE 80


COPY NT208.P23.sln NT208.P23.sln

COPY src/Contracts/Contracts.csproj src/Contracts/Contracts.csproj
COPY src/BuyingService/BuyingService.csproj src/BuyingService/BuyingService.csproj

RUN dotnet restore src/BuyingService/BuyingService.csproj

COPY src/BuyingService src/BuyingService
COPY src/Contracts src/Contracts
WORKDIR /app/src/BuyingService
RUN dotnet publish -c Release -o /app/src/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 
WORKDIR /app
COPY --from=build /app/src/out .
ENTRYPOINT [ "dotnet", "BuyingService.dll" ]


