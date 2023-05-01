FROM mcr.microsoft.com/dotnet/sdk:7.0 as build
WORKDIR /src
COPY . .
RUN dotnet publish "Proyecto_Carrito.csproj" -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT [ "dotnet", "Proyecto_Carrito.dll" ]

ENV ASPNETCORE_ENVIRONMENT=Docker
