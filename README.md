# Enchères Predict — Backend API

Backend C# .NET 8 avec architecture **DDD + CQRS** pour la plateforme d'analyse d'enchères immobilières judiciaires.

## Stack technique
- **.NET 8** Web API
- **DDD** (Domain-Driven Design)
- **CQRS** via MediatR 12
- **Entity Framework Core 8** + SQL Server LocalDB
- **FluentValidation** (pipeline MediatR)
- **Swagger** UI

## Démarrage rapide

```bash
# 1. Migration EF Core
cd EncheresPredict.Api
dotnet ef migrations add InitialCreate --project ../EncheresPredict.Infrastructure
dotnet ef database update --project ../EncheresPredict.Infrastructure

# 2. Lancer l'API
dotnet run
```

- **Swagger** : https://localhost:7xxx/swagger
- **CORS** configuré pour http://localhost:4200 (Angular)
- **Seed automatique** : 20 enchères avec analyses IA, documents et alertes

## Endpoints

| Méthode | Route | Description |
|---------|-------|-------------|
| GET | /api/auctions | Liste filtrée (city, type, badge, region, budget, sort) |
| GET | /api/auctions/{id} | Détail + AiAnalysis + Documents |
| GET | /api/auctions/stats | Stats dashboard (KPIs, chart data) |
| POST | /api/auctions | Créer une enchère |
| GET | /api/alerts | Liste des alertes |
| PATCH | /api/alerts/{id}/read | Marquer comme lue |
| GET | /api/profile | Profil utilisateur |
| POST | /api/profile | Sauvegarder le profil |
