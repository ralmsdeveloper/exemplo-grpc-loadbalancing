FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app
COPY . ./

RUN dotnet --info
RUN dotnet restore src/DevIO.Frontend
RUN dotnet publish src/DevIO.Frontend -c Release -o out
 
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out . 

ENTRYPOINT ["dotnet", "DevIO.Frontend.dll"]