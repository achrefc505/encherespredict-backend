-- ============================================================================
-- EncheresPredict — Seed (DML) — Step 4/5 : AI Analyses (20 rows, 1 par enchère)
-- ============================================================================
USE EncheresPredict;
GO

SET NOCOUNT ON;

INSERT INTO dbo.AiAnalyses
(Id, AuctionId, PricePerSqm, MarketTrend, RenovationCost, NetYield, GrossYield, PotentialResalePrice, RiskFactorsJson, StrengthsJson, ModelVersion, AnalyzedAt)
VALUES
('b0000001-0000-0000-0000-000000000001','a0000001-0000-0000-0000-000000000001', 4558, N'hausse',  35000, 6.8, 7.5, 315000, N'["Locataire en place","Pas d''ascenseur"]', N'["Emplacement premium","Balcon sud","DPE C"]', N'v2.1', SYSUTCDATETIME()),
('b0000001-0000-0000-0000-000000000002','a0000001-0000-0000-0000-000000000002', 3804, N'hausse',  45000, 5.9, 6.8, 358000, N'["Copropriété dégradée","3e étage sans ascenseur"]', N'["Quartier prisé","Parquet d''origine","Hauts plafonds"]', N'v2.1', SYSUTCDATETIME()),
('b0000001-0000-0000-0000-000000000003','a0000001-0000-0000-0000-000000000003', 2897, N'hausse',  80000, 6.2, 7.1, 435000, N'["Rénovation lourde","Mitoyenneté des deux côtés"]', N'["Quartier branché","Grande surface","Potentiel locatif élevé"]', N'v2.1', SYSUTCDATETIME()),
('b0000001-0000-0000-0000-000000000004','a0000001-0000-0000-0000-000000000004', 2125, N'stable',  120000,7.2, 8.4, 695000, N'["2 lots vacants","Travaux toiture","Gestion locative complexe"]', N'["Rendement immédiat","6 lots","Quartier central"]', N'v2.1', SYSUTCDATETIME()),
('b0000001-0000-0000-0000-000000000005','a0000001-0000-0000-0000-000000000005', 2685, N'hausse',  8000,  7.8, 8.6, 148000, N'["Syndic peu réactif"]', N'["Rénové","Vieux-Lille","Forte demande locative","DPE B"]', N'v2.1', SYSUTCDATETIME()),
('b0000001-0000-0000-0000-000000000006','a0000001-0000-0000-0000-000000000006', 2500, N'hausse',  25000, 5.8, 6.9, 285000, N'["Zone de rénovation urbaine","Travaux de finition"]', N'["Jardin 200m²","Quartier en devenir","Maison contemporaine"]', N'v2.1', SYSUTCDATETIME()),
('b0000001-0000-0000-0000-000000000007','a0000001-0000-0000-0000-000000000007', 2692, N'stable',  18000, 6.1, 7.0, 218000, N'["Bruit urbain","Pas de parking"]', N'["Centre historique","Forte demande locative","Commerces"]', N'v2.1', SYSUTCDATETIME()),
('b0000001-0000-0000-0000-000000000008','a0000001-0000-0000-0000-000000000008', 4107, N'hausse',  5000,  8.2, 9.1, 118000, N'["Charges de copropriété élevées","Petit surface"]', N'["Vue mer","Location saisonnière","Immeuble récent","DPE A"]', N'v2.1', SYSUTCDATETIME()),
('b0000001-0000-0000-0000-000000000009','a0000001-0000-0000-0000-000000000009', 2306, N'stable',  40000, 8.5, 9.8, 425000, N'["Vacance commerciale 18 mois","Travaux d''aménagement"]', N'["Hypercentre piétonnier","Fort trafic piéton","Bail commercial flexible"]', N'v2.1', SYSUTCDATETIME()),
('b0000001-0000-0000-0000-000000000010','a0000001-0000-0000-0000-000000000010', 2538, N'stable',  12000, 5.4, 6.2, 170000, N'["Cave humide","Travaux électricité"]', N'["Traversant","Proche métro","Cave"]', N'v2.1', SYSUTCDATETIME()),
('b0000001-0000-0000-0000-000000000011','a0000001-0000-0000-0000-000000000011', 4318, N'hausse',  6000,  7.1, 8.0, 98000,  N'["Très petite surface","4e sans ascenseur"]', N'["Montmartre","Location meublée","Forte demande"]', N'v2.1', SYSUTCDATETIME()),
('b0000001-0000-0000-0000-000000000012','a0000001-0000-0000-0000-000000000012', 3286, N'stable',  30000, 4.8, 5.6, 352000, N'["Charges copropriété élevées","Marché tendu"]', N'["Grande surface","Balcon","Cave","Bien entretenu"]', N'v2.1', SYSUTCDATETIME()),
('b0000001-0000-0000-0000-000000000013','a0000001-0000-0000-0000-000000000013', 1985, N'hausse',  20000, 4.5, 5.3, 265000, N'["Quartier récent, liquidité limitée","Travaux jardin"]', N'["Garage double","Quartier moderne","Maison récente"]', N'v2.1', SYSUTCDATETIME()),
('b0000001-0000-0000-0000-000000000014','a0000001-0000-0000-0000-000000000014', 3083, N'hausse',  10000, 4.2, 5.0, 152000, N'["Quartier en transition","Nuisances sonores chantier"]', N'["Proche Darwin","Quartier en mutation","Bon DPE"]', N'v2.1', SYSUTCDATETIME()),
('b0000001-0000-0000-0000-000000000015','a0000001-0000-0000-0000-000000000015', 350,  N'stable',  0,     4.1, 4.1, 215000, N'["Délai d''obtention du permis","Réseaux à raccorder"]', N'["Zone UA constructible","CU positif","Bon accès"]', N'v2.1', SYSUTCDATETIME()),
('b0000001-0000-0000-0000-000000000016','a0000001-0000-0000-0000-000000000016', 2250, N'stable',  15000, 3.8, 4.6, 166000, N'["Charges élevées","Marché marseillais volatile"]', N'["Résidence sécurisée","Parking","Gardien"]', N'v2.1', SYSUTCDATETIME()),
('b0000001-0000-0000-0000-000000000017','a0000001-0000-0000-0000-000000000017', 2158, N'stable',  10000, 3.5, 4.2, 210000, N'["Bail précaire non renouvelable","Concurrence commerciale forte"]', N'["Place animée","Loué","Bonne visibilité"]', N'v2.1', SYSUTCDATETIME()),
('b0000001-0000-0000-0000-000000000018','a0000001-0000-0000-0000-000000000018', 1909, N'stable',  20000, 3.1, 4.0, 172000, N'["Marché peu liquide","Mitoyenneté"]', N'["Jardin","Toiture neuve","Quartier calme"]', N'v2.1', SYSUTCDATETIME()),
('b0000001-0000-0000-0000-000000000019','a0000001-0000-0000-0000-000000000019', 1854, N'baisse',  200000,2.1, 3.2, 748000, N'["3 locataires protégés","Travaux ravalement façade urgent","Copropriété en difficulté","Marché en baisse secteur"]', N'["8 lots","Potentiel à terme"]', N'v2.1', SYSUTCDATETIME()),
('b0000001-0000-0000-0000-000000000020','a0000001-0000-0000-0000-000000000020', 2138, N'baisse',  55000, 1.2, 2.8, 122000, N'["DPE G — rénovation énergétique obligatoire","RDC côté rue","Humidité","Marché en baisse secteur"]', N'["Prix d''entrée bas"]', N'v2.1', SYSUTCDATETIME());
GO

PRINT '✓ 20 AI Analyses insérées';
GO
