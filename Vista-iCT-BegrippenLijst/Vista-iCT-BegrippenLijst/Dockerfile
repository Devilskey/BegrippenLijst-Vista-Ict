#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Vista-iCT-BegrippenLijst/Vista-iCT-BegrippenLijst.csproj", "Vista-iCT-BegrippenLijst/"]
RUN dotnet restore "Vista-iCT-BegrippenLijst/Vista-iCT-BegrippenLijst.csproj"
COPY . .
WORKDIR "/src/Vista-iCT-BegrippenLijst"
RUN dotnet build "Vista-iCT-BegrippenLijst.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Vista-iCT-BegrippenLijst.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Vista-iCT-BegrippenLijst.dll"]