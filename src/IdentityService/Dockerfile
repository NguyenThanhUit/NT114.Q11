FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /app
EXPOSE 80

COPY NT208.P23.sln NT208.P23.sln
COPY src/IdentityService/IdentityService.csproj src/IdentityService/IdentityService.csproj


RUN dotnet restore src/IdentityService/IdentityService.csproj

COPY src/IdentityService src/IdentityService
WORKDIR /app/src/IdentityService
RUN dotnet publish -c Release -o /app/src/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/src/out .
ENTRYPOINT [ "dotnet", "IdentityService.dll" ]


