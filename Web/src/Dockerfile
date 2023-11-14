#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS sdk
RUN curl -fsSL https://deb.nodesource.com/setup_18.x | bash -
RUN apt install nodejs -y

FROM sdk AS build
WORKDIR /src
COPY ["Directory.Build.props", "."]
COPY ["Web/src/CodeRabbits.KaoList.Web.csproj", "Web/src/"]
COPY ["Data/src/CodeRabbits.KaoList.Data.csproj", "Data/src/"]
COPY ["Board/src/CodeRabbits.KaoList.Board.csproj", "Board/src/"]
COPY ["Core/src/CodeRabbits.KaoList.csproj", "Core/src/"]
COPY ["Identity/src/CodeRabbits.KaoList.Identity.csproj", "Identity/src/"]
COPY ["Playlist/src/CodeRabbits.KaoList.Playlist.csproj", "Playlist/src/"]
COPY ["Song/src/CodeRabbits.KaoList.Song.csproj", "Song/src/"]
RUN dotnet restore "Web/src/CodeRabbits.KaoList.Web.csproj"
COPY . .
WORKDIR "/src/Web/src"
RUN npm ci
RUN npm install --global gulp-cli
RUN gulp --gulpfile "./gulpfile.js" min
RUN dotnet build "CodeRabbits.KaoList.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CodeRabbits.KaoList.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false
COPY --from=build /src/Web/src/client-app/public/css ./css
COPY --from=build /src/Web/src/client-app/public/js ./js

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CodeRabbits.KaoList.Web.dll"]
