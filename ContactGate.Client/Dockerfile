# Этап 1: Сборка
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["ContactGate.Client/ContactGate.Client.csproj", "ContactGate.Client/"]
COPY ["ContactGate.Domain/ContactGate.Domain.csproj", "ContactGate.Domain/"]

RUN dotnet restore "ContactGate.Client/ContactGate.Client.csproj"

COPY . .
RUN dotnet publish "ContactGate.Client/ContactGate.Client.csproj" -c Release -o /app/publish


FROM nginx:alpine
WORKDIR /usr/share/nginx/html

COPY --from=build /app/publish/wwwroot .

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]