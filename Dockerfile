FROM mcr.microsoft.com/dotnet/core/aspnet
WORKDIR /app
COPY HiHellApi/bin/Release/netcoreapp2.2/publish/. /app
EXPOSE 80
CMD ["dotnet","HiHelloApi.dll"]
