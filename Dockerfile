# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source
COPY ./ /source

# installs NodeJS and NPM
RUN apt-get update -yq && apt-get upgrade -yq && apt-get install -yq curl git nano
RUN curl -sL https://deb.nodesource.com/setup_12.x | bash - && apt-get install -yq nodejs build-essential

RUN npm install -g npm
RUN npm --version


# copy csproj and restore as distinct layers
COPY *.sln .
COPY control/*.csproj ./control/
RUN dotnet restore -r linux-arm

# copy everything else and build app
COPY control/. ./control/
WORKDIR /source/control
RUN dotnet publish -c release -o /app -r linux-arm --self-contained false --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim-arm32v7
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["./control"]
