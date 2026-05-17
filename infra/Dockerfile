FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build

RUN apk add clang build-base zlib-dev

WORKDIR /app
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release --property PublishDir=out

FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["./rinha-backend-csharp-2026"]

# JIT
# ENTRYPOINT ["dotnet", "payment-processor.dll"]