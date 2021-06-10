#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Examples/UseCase/Wings.Examples.UseCase.Server/Wings.Examples.UseCase.Server.csproj", "Examples/UseCase/Wings.Examples.UseCase.Server/"]
COPY ["Examples/UseCase/Wings.Examples.UseCase.Client/Wings.Examples.UseCase.Client.csproj", "Examples/UseCase/Wings.Examples.UseCase.Client/"]
COPY ["Frameworks/Wings.Framework.Shared/Wings.Framework.Shared.csproj", "Frameworks/Wings.Framework.Shared/"]
COPY ["Frameworks/Wings.Framework.Ui.Ant/Wings.Framework.Ui.Ant.csproj", "Frameworks/Wings.Framework.Ui.Ant/"]
COPY ["Frameworks/Wings.Framework.Ui.Core/Wings.Framework.Ui.Core.csproj", "Frameworks/Wings.Framework.Ui.Core/"]
COPY ["Examples/UseCase/Wings.Examples.UseCase.Shared/Wings.Examples.UseCase.Shared.csproj", "Examples/UseCase/Wings.Examples.UseCase.Shared/"]
RUN dotnet restore "Examples/UseCase/Wings.Examples.UseCase.Server/Wings.Examples.UseCase.Server.csproj"
COPY . .
WORKDIR "/src/Examples/UseCase/Wings.Examples.UseCase.Server"
RUN dotnet build "Wings.Examples.UseCase.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Wings.Examples.UseCase.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Wings.Examples.UseCase.Server.dll"]