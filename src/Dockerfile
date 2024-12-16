# NOTE: Needs buildkit for proper targetting
# export DOCKER_BUILDKIT=1

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# For running tests, target the build container and run dotnet test within this container
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG VERSION
WORKDIR /src
COPY . .
RUN dotnet build /p:Version=$VERSION -c Release -o /app

# Our testrunner target
FROM build AS testrunner
WORKDIR /src
ENTRYPOINT ["sh", "test_and_transform.sh"]

# Our package target
FROM build AS package
ARG NUGET_KEY
ARG NUGET_URL
WORKDIR /app
RUN find . -name "*.nupkg" | xargs -i dotnet nuget push {} --source $NUGET_URL --api-key $NUGET_KEY
