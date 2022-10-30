FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 54321
EXPOSE 43210

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
RUN dotnet dev-certs https
WORKDIR /src
COPY ["TwelveFactorApp.Api/TwelveFactorApp.Api.csproj", "TwelveFactorApp.Api/"]
RUN dotnet restore "TwelveFactorApp.Api/TwelveFactorApp.Api.csproj"
COPY . .
WORKDIR "/src/TwelveFactorApp.Api"
RUN dotnet build "TwelveFactorApp.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TwelveFactorApp.Api.csproj" -c Release -o /app/publish

ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV TZ=Europe/Berlin
ENV LANG de_AT.UTF-8
ENV LANGUAGE ${LANG}
ENV LC_ALL ${LANG}

FROM base AS final
WORKDIR /app
COPY --from=publish /root/.dotnet/corefx/cryptography/x509stores/my/* /root/.dotnet/corefx/cryptography/x509stores/my/
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TwelveFactorApp.Api.dll"]
