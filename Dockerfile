FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /unihacker-docker

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/runtime:6.0
WORKDIR /unihacker-docker
ENV UNITY_PATH=/opt/unity
COPY --from=build-env /unihacker-docker/out .
ENTRYPOINT ["dotnet", "unihacker.dll"]