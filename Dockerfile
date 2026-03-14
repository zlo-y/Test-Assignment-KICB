
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src


COPY ["ContactGate.WebAPI/ContactGate.WebAPI.csproj", "ContactGate.WebAPI/"]
COPY ["ContactGate.Application/ContactGate.Application.csproj", "ContactGate.Application/"]
COPY ["ContactGate.Domain/ContactGate.Domain.csproj", "ContactGate.Domain/"]
COPY ["ContactGate.Infrastructure/ContactGate.Infrastructure.csproj", "ContactGate.Infrastructure/"]
COPY ["ContactGate.Client/ContactGate.Client.csproj", "ContactGate.Client/"]

RUN dotnet restore "ContactGate.WebAPI/ContactGate.WebAPI.csproj"


COPY . .
RUN dotnet publish "ContactGate.WebAPI/ContactGate.WebAPI.csproj" -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ContactGate.WebAPI.dll"]