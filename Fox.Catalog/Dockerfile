FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Fox.Catalog/Fox.Catalog.csproj", "Fox.Catalog/"]
RUN dotnet restore "Fox.Catalog/Fox.Catalog.csproj"
COPY . .
WORKDIR "/src/Fox.Catalog"
RUN dotnet build "Fox.Catalog.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Fox.Catalog.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Fox.Catalog.dll"]