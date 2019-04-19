FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Fox.Provider/Fox.Provider.csproj", "Fox.Provider/"]
RUN dotnet restore "Fox.Provider/Fox.Provider.csproj"
COPY . .
WORKDIR "/src/Fox.Provider"
RUN dotnet build "Fox.Provider.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Fox.Provider.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Fox.Provider.dll"]