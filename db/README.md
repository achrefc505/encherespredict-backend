# Base de données EncheresPredict

Scripts SQL Server pour créer la base **EncheresPredict** et insérer les 20 enchères de démo.

## Prérequis

- SQL Server LocalDB (inclus avec Visual Studio) ou SQL Server Express/Developer
- Outil client : `sqlcmd` (CLI) OU SSMS (Management Studio) OU Azure Data Studio

## Fichiers

| Ordre | Fichier | Description |
|-------|---------|-------------|
| 1 | `01_schema.sql` | DDL — crée la base + 5 tables + index + FK |
| 2 | `02_seed_auctions_1-10.sql` | Enchères 1 à 10 (Paris, Lyon, Bordeaux, Marseille, Lille, Nantes, Toulouse, Nice, Strasbourg, Rennes) |
| 3 | `03_seed_auctions_11-20.sql` | Enchères 11 à 20 (Paris 18e, Lyon 3e, Montpellier, Bordeaux Bacalan, Nantes terrain, Marseille 8e, Toulouse, Lille, Nice immeuble, Strasbourg Neudorf) |
| 4 | `04_seed_ai_analyses.sql` | 20 analyses IA (1 par enchère) avec prix/m², tendance marché, points forts/risques |
| 5 | `05_seed_documents.sql` | 49 documents PDF + 20 alertes (Opportunity / Info / Warning) |

## Exécution

### Option A — via `sqlcmd` (CLI)

```bash
cd db
sqlcmd -S "(localdb)\mssqllocaldb" -i 01_schema.sql
sqlcmd -S "(localdb)\mssqllocaldb" -i 02_seed_auctions_1-10.sql
sqlcmd -S "(localdb)\mssqllocaldb" -i 03_seed_auctions_11-20.sql
sqlcmd -S "(localdb)\mssqllocaldb" -i 04_seed_ai_analyses.sql
sqlcmd -S "(localdb)\mssqllocaldb" -i 05_seed_documents.sql
```

### Option B — script PowerShell (tout-en-un)

```powershell
cd db
Get-ChildItem *.sql | Sort-Object Name | ForEach-Object {
    Write-Host "▶ Exécution $($_.Name)..." -ForegroundColor Cyan
    sqlcmd -S "(localdb)\mssqllocaldb" -i $_.FullName
}
```

### Option C — via SSMS / Azure Data Studio

1. Ouvrir chaque fichier dans l'ordre
2. `F5` (Execute) pour chacun

## Vérification

```sql
USE EncheresPredict;
SELECT COUNT(*) AS Enchères  FROM dbo.Auctions;    -- Attendu : 20
SELECT COUNT(*) AS Analyses  FROM dbo.AiAnalyses;  -- Attendu : 20
SELECT COUNT(*) AS Documents FROM dbo.Documents;   -- Attendu : 49
SELECT COUNT(*) AS Alertes   FROM dbo.Alerts;      -- Attendu : 20

SELECT Title, City, RoiValue, Badge
FROM dbo.Auctions
ORDER BY RoiValue DESC;
```

## Alternative : seed via EF Core au démarrage

Si tu lances simplement `dotnet run --project EncheresPredict.Api`, la classe
`SeedData.InitializeAsync()` fait **exactement la même chose** : elle applique
les migrations EF + insère les 20 enchères si la table `Auctions` est vide.

Les scripts SQL de ce dossier sont utiles pour :
- Créer la base manuellement (sans lancer l'API)
- Exporter les données vers un autre SGBD
- Debugging / inspection directe de la DB

## Schéma relationnel

```
Auctions  (1)──(1)  AiAnalyses
Auctions  (1)──(N)  Documents
Auctions  (0..1)──(N)  Alerts  (FK nullable)
UserProfiles  (indépendant)
```

Toutes les colonnes enum (`Badge`, `Status`, `Type` d'alerte) sont stockées en
**NVARCHAR** (pas en INT), pour faciliter les requêtes ad-hoc.
