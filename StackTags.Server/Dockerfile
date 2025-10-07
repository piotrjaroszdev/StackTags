FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Install Node.js
RUN apt-get update && apt-get install -y curl \
    && curl -fsSL https://deb.nodesource.com/setup_20.x | bash - \
    && apt-get install -y nodejs

# Copy solution and all referenced projects
COPY *.sln .
COPY StackTags.Server/*.csproj ./StackTags.Server/
COPY StackTags.Server/Models/*.csproj ./StackTags.Server/Models/
COPY StackTags.Server/Repositories/*.csproj ./StackTags.Server/Repositories/
COPY StackTags.Server/Services/*.csproj ./StackTags.Server/Services/
COPY StackTags.Server/. ./StackTags.Server/
COPY StackTags.Server.Data/*.csproj ./StackTags.Server.Data/
COPY StackTags.Server.Data/. ./StackTags.Server.Data/
COPY stacktags.client/*.esproj ./stacktags.client/
COPY stacktags.client/. ./stacktags.client/

WORKDIR /app/StackTags.Server
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/StackTags.Server/out ./
EXPOSE 80
ENTRYPOINT ["dotnet", "StackTags.Server.dll"]