FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /app
EXPOSE 80


COPY NT208.P23.sln NT208.P23.sln

COPY src/Contracts/Contracts.csproj src/Contracts/Contracts.csproj

COPY src/AuctionService/AuctionService.csproj src/AuctionService/AuctionService.csproj


RUN dotnet restore src/AuctionService/AuctionService.csproj

COPY src/AuctionService src/AuctionService
COPY src/Contracts src/Contracts
WORKDIR /app/src/AuctionService
RUN dotnet publish -c Release -o /app/src/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/src/out .
ENTRYPOINT [ "dotnet", "AuctionService.dll" ]


