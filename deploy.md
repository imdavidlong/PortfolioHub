# PortfolioHub Deployment

## Automatic deployment

GitHub Actions deploys automatically when `main` receives a push.

Workflow file:

```text
.github/workflows/azure-app-service.yml
```

Required GitHub secret:

```text
AZURE_WEBAPP_PUBLISH_PROFILE
```

To get the value:

1. Open Azure Portal.
2. Go to App Service `portfoliohub-davidlong`.
3. Click `Get publish profile`.
4. Copy the whole downloaded publish profile XML.
5. In GitHub, go to `Settings > Secrets and variables > Actions`.
6. Create repository secret `AZURE_WEBAPP_PUBLISH_PROFILE`.
7. Paste the publish profile XML as the secret value.

After that, every push to `main` deploys the app. You can also run it manually from GitHub Actions with `workflow_dispatch`.

## Azure app settings

Make sure the App Service has these environment variables:

```text
ASPNETCORE_ENVIRONMENT=Production
WEBSITES_ENABLE_APP_SERVICE_STORAGE=true
ConnectionStrings__DefaultConnection=Data Source=/home/data/portfoliohub.db;Cache=Shared
```

## Manual deployment

```powershell
dotnet publish .\PortfolioHub\PortfolioHub.csproj -c Release -o .\artifacts\publish
tar -a -cf .\artifacts\portfoliohub-linux.zip -C .\artifacts\publish .
az webapp deploy --resource-group portfoliohub-rg --name portfoliohub-davidlong --src-path .\artifacts\portfoliohub-linux.zip --type zip
```
