FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
#EXPOSE 80
 
FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR .
COPY . .
RUN dotnet build "HiHelloApi.sln" -c Release -o /app
RUN dotnet publish "HiHellApi.sln" -c Release -o /app/publish
 
WORKDIR /app/publish
ENTRYPOINT ["dotnet", "HiHelloApi.dll"]