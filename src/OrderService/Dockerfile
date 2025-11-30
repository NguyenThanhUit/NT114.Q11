FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /app
EXPOSE 80


COPY NT208.P23.sln NT208.P23.sln
COPY src/OrderService/OrderService.csproj src/OrderService/OrderService.csproj
COPY src/Contracts/Contracts.csproj src/Contracts/Contracts.csproj


RUN dotnet restore src/OrderService/OrderService.csproj

COPY src/OrderService src/OrderService
COPY src/Contracts src/Contracts
WORKDIR /app/src/OrderService
RUN dotnet publish -c Release -o /app/src/out
# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/src/out .
ENTRYPOINT [ "dotnet", "OrderService.dll" ]


