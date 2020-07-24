FROM mcr.microsoft.com/dotnet/core-nightly/sdk:3.1.101 as build
WORKDIR /app
COPY src/Connect31 src/Connect31
RUN dotnet build src/Connect31

FROM build as publish
WORKDIR /app
RUN dotnet publish src/Connect31 -c Release -o /app -r linux-x64

FROM mcr.microsoft.com/dotnet/core-nightly/runtime:3.1 AS runtime
WORKDIR /app
COPY --from=publish /app .
CMD dotnet Connect31.dll
EXPOSE 80