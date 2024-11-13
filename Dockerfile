FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base
USER app
WORKDIR /App
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS  build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src 
COPY . ./
RUN dotnet restore
RUN dotnet build "ApiManejoRRHH/ApiManejoRRHH.csproj" -c $BUILD_CONFIGURATION -o /App/build


FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ApiManejoRRHH.csproj"  -c $BUILD_CONFIGURATION Release -o /App/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /App
COPY --from=publish /App/publish . 
ENTRYPOINT ["dotnet", "ApiManejoRRHH.dll"]