# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /app

# Copy project files first
COPY DataAccess/DataAccess.csproj ./DataAccess/
COPY BusinessLogic/BusinessLogic.csproj ./BusinessLogic/
COPY WebApi/WebApi.csproj ./WebApi/
# Add solution file copy if you have one, e.g.: COPY YourSolution.sln .

# Restore dependencies for each project
RUN dotnet restore DataAccess/DataAccess.csproj
RUN dotnet restore BusinessLogic/BusinessLogic.csproj
RUN dotnet restore WebApi/WebApi.csproj

# Copy the rest of the source code
COPY DataAccess/ ./DataAccess/
COPY BusinessLogic/ ./BusinessLogic/
COPY WebApi/ ./WebApi/

# Publish the WebApi project
RUN dotnet publish WebApi/WebApi.csproj -c Release -o /app/publish --no-restore

# Stage 2: Serve the application
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build-env /app/publish .

# Expose the port the app runs on (default is 8080 for ASP.NET Core in containers)
EXPOSE 8080
# For HTTPS, uncomment the following line (make sure your app is configured for HTTPS)
# EXPOSE 8081

# Set the entry point for the container
ENTRYPOINT ["dotnet", "WebApi.dll"] 