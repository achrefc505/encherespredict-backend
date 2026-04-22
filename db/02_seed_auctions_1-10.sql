-- ============================================================================
-- EncheresPredict — Seed (DML) — Step 2/5 : Auctions 1-10
-- Prérequis : 01_schema.sql déjà exécuté
-- ============================================================================
USE EncheresPredict;
GO

SET NOCOUNT ON;

INSERT INTO dbo.Auctions
(Id, Title, Tribunal, City, Region, [Address], Surface, Rooms, [Type], StartPriceAmount, AiEstimateAmount, Confidence, RoiValue, Badge, [Status], AuctionDate, [Description])
VALUES
('a0000001-0000-0000-0000-000000000001', N'Appartement T3 — Paris 11e',           N'TJ Paris',      N'Paris',      N'Île-de-France',        N'14 rue de la Roquette, 75011 Paris',         68, 3, N'Appartement',         185000, 310000, 92, 67.57,  N'TresBonneAffaire', N'Active',   DATEADD(day,12, SYSUTCDATETIME()), N'Appartement lumineux au 3e étage avec balcon. Travaux de rénovation légers nécessaires.'),
('a0000001-0000-0000-0000-000000000002', N'Appartement T4 — Lyon 6e',             N'TJ Lyon',       N'Lyon',       N'Auvergne-Rhône-Alpes', N'8 avenue Foch, 69006 Lyon',                  92, 4, N'Appartement',         220000, 350000, 88, 59.09,  N'TresBonneAffaire', N'Active',   DATEADD(day,18, SYSUTCDATETIME()), N'Grand appartement haussmannien avec parquet d''origine. Travaux de mise aux normes.'),
('a0000001-0000-0000-0000-000000000003', N'Maison T5 — Bordeaux Chartrons',       N'TJ Bordeaux',   N'Bordeaux',   N'Nouvelle-Aquitaine',   N'22 rue Notre-Dame, 33000 Bordeaux',         145, 5, N'Maison',              280000, 420000, 85, 50.00,  N'TresBonneAffaire', N'Active',   DATEADD(day,25, SYSUTCDATETIME()), N'Maison de ville dans le quartier des Chartrons. Rénovation complète à prévoir.'),
('a0000001-0000-0000-0000-000000000004', N'Immeuble de rapport — Marseille 6e',   N'TJ Marseille',  N'Marseille',  N'PACA',                 N'15 rue Paradis, 13006 Marseille',           320, 0, N'Immeuble de rapport', 450000, 680000, 79, 51.11,  N'TresBonneAffaire', N'Upcoming', DATEADD(day,35, SYSUTCDATETIME()), N'Immeuble de 6 appartements. 4 loués, 2 vacants. Rendement locatif immédiat.'),
('a0000001-0000-0000-0000-000000000005', N'Appartement T2 — Lille Vieux-Lille',   N'TJ Lille',      N'Lille',      N'Hauts-de-France',      N'3 rue de la Monnaie, 59000 Lille',           54, 2, N'Appartement',          95000, 145000, 90, 52.63,  N'TresBonneAffaire', N'Active',   DATEADD(day,8,  SYSUTCDATETIME()), N'Appartement rénové dans le Vieux-Lille. Cuisine équipée, parquet. Idéal investisseur.'),
('a0000001-0000-0000-0000-000000000006', N'Maison T4 — Nantes Île de Nantes',     N'TJ Nantes',     N'Nantes',     N'Pays de la Loire',     N'47 bd de la Prairie au Duc, 44200 Nantes',  110, 4, N'Maison',              195000, 275000, 83, 41.03,  N'TresBonneAffaire', N'Active',   DATEADD(day,22, SYSUTCDATETIME()), N'Maison contemporaine avec jardin de 200m². Quartier en pleine rénovation urbaine.'),
('a0000001-0000-0000-0000-000000000007', N'Appartement T3 — Toulouse Capitole',   N'TJ Toulouse',   N'Toulouse',   N'Occitanie',            N'12 rue Saint-Rome, 31000 Toulouse',          78, 3, N'Appartement',         155000, 210000, 86, 35.48,  N'TresBonneAffaire', N'Active',   DATEADD(day,15, SYSUTCDATETIME()), N'Appartement en plein cœur de Toulouse, à 200m du Capitole. Commerces en pied d''immeuble.'),
('a0000001-0000-0000-0000-000000000008', N'Studio — Nice Promenade',              N'TJ Nice',       N'Nice',       N'PACA',                 N'88 Promenade des Anglais, 06000 Nice',       28, 1, N'Studio',               85000, 115000, 87, 35.29,  N'TresBonneAffaire', N'Active',   DATEADD(day,10, SYSUTCDATETIME()), N'Studio vue mer partielle. Immeuble récent. Fort potentiel saisonnier.'),
('a0000001-0000-0000-0000-000000000009', N'Local commercial — Strasbourg Centre', N'TJ Strasbourg', N'Strasbourg', N'Grand Est',            N'5 rue des Grandes Arcades, 67000 Strasbourg',180, 0, N'Local commercial',   320000, 415000, 75, 29.69,  N'BonneAffaire',     N'Upcoming', DATEADD(day,40, SYSUTCDATETIME()), N'Local commercial en hypercentre piétonnier. Loyer potentiel 3 500€/mois.'),
('a0000001-0000-0000-0000-000000000010', N'Appartement T2 — Rennes Centre',       N'TJ Rennes',     N'Rennes',     N'Bretagne',             N'18 rue Saint-Georges, 35000 Rennes',         65, 2, N'Appartement',         130000, 165000, 82, 26.92,  N'BonneAffaire',     N'Active',   DATEADD(day,20, SYSUTCDATETIME()), N'Appartement traversant avec cave. Proche métro et commerces.');
GO

PRINT '✓ Auctions 1-10 insérées';
GO
