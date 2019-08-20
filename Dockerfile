
FROM mcr.microsoft.com/dotnet/core/aspnet
WORKDIR /app
COPY . /app
EXPOSE 80
CMD ["dotnet", "HiHelloApi.dll"]