FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /app
EXPOSE 80

#Copy file .csproj
COPY NT208.P23.sln NT208.P23.sln

COPY src/Contracts/Contracts.csproj src/Contracts/Contracts.csproj

# Restore package deps
RUN dotnet restore src/Contracts/Contracts.csproj
# Copy the app folders 
COPY src/Contracts src/Contracts
WORKDIR /app/src/Contracts
RUN dotnet publish -c Release -o /app/src/out
# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/src/out .
ENTRYPOINT [ "dotnet", "Contracts.dll" ]


