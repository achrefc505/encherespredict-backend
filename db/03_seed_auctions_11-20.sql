-- ============================================================================
-- EncheresPredict — Seed (DML) — Step 3/5 : Auctions 11-20
-- ============================================================================
USE EncheresPredict;
GO

SET NOCOUNT ON;

INSERT INTO dbo.Auctions
(Id, Title, Tribunal, City, Region, [Address], Surface, Rooms, [Type], StartPriceAmount, AiEstimateAmount, Confidence, RoiValue, Badge, [Status], AuctionDate, [Description])
VALUES
('a0000001-0000-0000-0000-000000000011', N'Studio — Paris 18e Montmartre',               N'TJ Paris',       N'Paris',        N'Île-de-France',         N'3 rue Lepic, 75018 Paris',                   22, 1, N'Studio',              75000,  95000,  84, 26.67,  N'BonneAffaire', N'Active',   DATEADD(day,6,  SYSUTCDATETIME()), N'Studio Montmartre au 4e étage. Idéal investissement locatif meublé.'),
('a0000001-0000-0000-0000-000000000012', N'Appartement T4 — Lyon 3e',                    N'TJ Lyon',        N'Lyon',         N'Auvergne-Rhône-Alpes',  N'52 cours Lafayette, 69003 Lyon',            105, 4, N'Appartement',         280000, 345000, 78, 23.21,  N'BonneAffaire', N'Upcoming', DATEADD(day,30, SYSUTCDATETIME()), N'Grand appartement familial avec balcon et cave. Copropriété bien entretenue.'),
('a0000001-0000-0000-0000-000000000013', N'Maison T4 — Montpellier Port Marianne',       N'TJ Montpellier', N'Montpellier',  N'Occitanie',             N'8 allée de la Citadelle, 34000 Montpellier',130, 4, N'Maison',              215000, 258000, 80, 20.00,  N'BonneAffaire', N'Active',   DATEADD(day,17, SYSUTCDATETIME()), N'Maison moderne dans le nouveau quartier Port Marianne. Garage double.'),
('a0000001-0000-0000-0000-000000000014', N'Appartement T2 — Bordeaux Bacalan',           N'TJ Bordeaux',    N'Bordeaux',     N'Nouvelle-Aquitaine',    N'31 rue Achard, 33300 Bordeaux',              48, 2, N'Appartement',         125000, 148000, 77, 18.40,  N'BonneAffaire', N'Upcoming', DATEADD(day,28, SYSUTCDATETIME()), N'Appartement dans le quartier Bacalan en pleine transformation. Proche Darwin.'),
('a0000001-0000-0000-0000-000000000015', N'Terrain constructible — Nantes Sud',          N'TJ Nantes',      N'Nantes',       N'Pays de la Loire',      N'Route de Clisson, 44200 Nantes',             600,0, N'Terrain',             180000, 210000, 72, 16.67,  N'BonneAffaire', N'Upcoming', DATEADD(day,45, SYSUTCDATETIME()), N'Terrain constructible de 600m² en zone UA. CU positif. Idéal promoteur.'),
('a0000001-0000-0000-0000-000000000016', N'Appartement T3 — Marseille 8e',               N'TJ Marseille',   N'Marseille',    N'PACA',                  N'24 avenue du Prado, 13008 Marseille',        72, 3, N'Appartement',         145000, 162000, 71, 11.72,  N'Neutre',       N'Active',   DATEADD(day,14, SYSUTCDATETIME()), N'Appartement dans résidence sécurisée avec gardien. Parking sous-sol.'),
('a0000001-0000-0000-0000-000000000017', N'Local commercial — Toulouse Esquirol',        N'TJ Toulouse',    N'Toulouse',     N'Occitanie',             N'9 place Esquirol, 31000 Toulouse',           95, 0, N'Local commercial',    190000, 205000, 68, 7.89,   N'Neutre',       N'Upcoming', DATEADD(day,50, SYSUTCDATETIME()), N'Local en rez-de-chaussée place animée. Actuellement loué 1 400€/mois (bail précaire).'),
('a0000001-0000-0000-0000-000000000018', N'Maison T3 — Lille Lambersart',                N'TJ Lille',       N'Lille',        N'Hauts-de-France',       N'15 rue de la Barre, 59130 Lambersart',       88, 3, N'Maison',              160000, 168000, 74, 5.00,   N'Neutre',       N'Active',   DATEADD(day,19, SYSUTCDATETIME()), N'Maison mitoyenne avec jardin. Travaux de toiture récents. Quartier résidentiel calme.'),
('a0000001-0000-0000-0000-000000000019', N'Immeuble de rapport — Nice Est',              N'TJ Nice',        N'Nice',         N'PACA',                  N'15 avenue de la Californie, 06000 Nice',    410, 0, N'Immeuble de rapport', 780000, 760000, 62, -2.56,  N'Risque',       N'Upcoming', DATEADD(day,60, SYSUTCDATETIME()), N'Immeuble de 8 appartements dont 3 occupés par des locataires protégés.'),
('a0000001-0000-0000-0000-000000000020', N'Appartement T2 — Strasbourg Neudorf',         N'TJ Strasbourg',  N'Strasbourg',   N'Grand Est',             N'44 route de Colmar, 67100 Strasbourg',       58, 2, N'Appartement',         135000, 124000, 65, -8.15,  N'Risque',       N'Upcoming', DATEADD(day,55, SYSUTCDATETIME()), N'Appartement au rez-de-chaussée côté rue. Humidité constatée. DPE G.');
GO

PRINT '✓ Auctions 11-20 insérées';
GO
