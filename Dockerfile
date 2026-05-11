FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /app

COPY . .

RUN dotnet restore
RUN dotnet publish WEBEditorAPI.Api/WEBEditorAPI.Api.csproj -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview
WORKDIR /app

COPY --from=build /publish .

EXPOSE 4000

ENTRYPOINT ["dotnet", "WEBEditorAPI.Api.dll"]