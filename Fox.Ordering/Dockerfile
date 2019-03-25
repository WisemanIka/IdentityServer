FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Fox.Ordering/Fox.Ordering.csproj", "Fox.Ordering/"]
RUN dotnet restore "Fox.Ordering/Fox.Ordering.csproj"
COPY . .
WORKDIR "/src/Fox.Ordering"
RUN dotnet build "Fox.Ordering.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Fox.Ordering.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Fox.Ordering.dll"]