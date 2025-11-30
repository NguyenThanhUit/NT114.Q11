FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /app
EXPOSE 80

COPY NT208.P23.sln NT208.P23.sln

COPY src/SearchService/SearchService.csproj src/SearchService/SearchService.csproj

COPY src/Contracts/Contracts.csproj src/Contracts/Contracts.csproj


RUN dotnet restore src/SearchService/SearchService.csproj

COPY src/SearchService src/SearchService
COPY src/Contracts src/Contracts
WORKDIR /app/src/SearchService
RUN dotnet publish -c Release -o /app/src/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/src/out .
ENTRYPOINT [ "dotnet", "SearchService.dll" ]


