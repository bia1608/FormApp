FROM mcr.microsoft.com/dotnet/aspnet:8.0-windowsservercore-ltsc2022 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0-windowsservercore-ltsc2022 AS build
WORKDIR /src
COPY ["FormApp3.Server/FormApp3.Server.csproj", "FormApp3.Server/"]
RUN dotnet restore "FormApp3.Server/FormApp3.Server.csproj"
COPY . .
WORKDIR "/src/FormApp3.Server"
RUN dotnet build "FormApp3.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FormApp3.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Copiază DLL-ul pentru Windows 64-bit pentru DinkToPdf
COPY ["FormApp3.Server/bin/Debug/net8.0/libwkhtmltox.dll", "/app/"]
ENTRYPOINT ["dotnet", "FormApp3.Server.dll"]