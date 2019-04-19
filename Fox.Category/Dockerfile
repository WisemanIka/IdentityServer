FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Fox.Category/Fox.Category.csproj", "Fox.Category/"]
RUN dotnet restore "Fox.Category/Fox.Category.csproj"
COPY . .
WORKDIR "/src/Fox.Category"
RUN dotnet build "Fox.Category.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Fox.Category.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Fox.Category.dll"]