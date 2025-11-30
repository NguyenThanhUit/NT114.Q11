FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /app
EXPOSE 80


COPY NT208.P23.sln NT208.P23.sln

COPY src/UserService/UserService.csproj src/UserService/UserService.csproj

RUN dotnet restore src/UserService/UserService.csproj

COPY src/UserService src/UserService
WORKDIR /app/src/UserService
RUN dotnet publish -c Release -o /app/src/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/src/out .
ENTRYPOINT [ "dotnet", "UserService.dll" ]


