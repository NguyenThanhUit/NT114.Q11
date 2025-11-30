FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /app
EXPOSE 80


COPY NT208.P23.sln NT208.P23.sln

COPY src/Contracts/Contracts.csproj src/Contracts/Contracts.csproj
COPY src/NotificationService/NotificationService.csproj src/NotificationService/NotificationService.csproj


RUN dotnet restore src/NotificationService/NotificationService.csproj

COPY src/NotificationService src/NotificationService
COPY src/Contracts src/Contracts
WORKDIR /app/src/NotificationService
RUN dotnet publish -c Release -o /app/src/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/src/out .
ENTRYPOINT [ "dotnet", "NotificationService.dll" ]


