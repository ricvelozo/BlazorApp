FROM mcr.microsoft.com/dotnet/sdk:9.0@sha256:3fcf6f1e809c0553f9feb222369f58749af314af6f063f389cbd2f913b4ad556 AS builder

WORKDIR /usr/src/app

COPY . .
RUN --mount=type=cache,target=/root/.nuget/packages dotnet restore
RUN --mount=type=cache,target=/root/.nuget/packages dotnet publish Api -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0@sha256:b4bea3a52a0a77317fa93c5bbdb076623f81e3e2f201078d89914da71318b5d8

WORKDIR /app

RUN rm -rf /var/lib/apt/lists/*

COPY --from=builder /usr/src/app/out .

ENTRYPOINT ["dotnet", "Api.dll"]
