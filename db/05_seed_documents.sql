-- ============================================================================
-- EncheresPredict — Seed (DML) — Step 5/5 : Documents + Alerts
-- ============================================================================
USE EncheresPredict;
GO

SET NOCOUNT ON;

-- Documents (2-4 par enchère)
INSERT INTO dbo.Documents (Id, AuctionId, [Name], [Type], [Size], Available) VALUES
('c0000001-0000-0000-0000-000000000001','a0000001-0000-0000-0000-000000000001', N'Cahier des charges',  N'cahier_charges', N'1.8 MB', 1),
('c0000001-0000-0000-0000-000000000002','a0000001-0000-0000-0000-000000000001', N'Rapport d''expertise',N'expertise',     N'3.2 MB', 1),
('c0000001-0000-0000-0000-000000000003','a0000001-0000-0000-0000-000000000001', N'Jugement',            N'jugement',       N'0.9 MB', 1),
('c0000001-0000-0000-0000-000000000004','a0000001-0000-0000-0000-000000000002', N'Cahier des charges',  N'cahier_charges', N'2.1 MB', 1),
('c0000001-0000-0000-0000-000000000005','a0000001-0000-0000-0000-000000000002', N'Expertise immobilière',N'expertise',    N'4.5 MB', 1),
('c0000001-0000-0000-0000-000000000006','a0000001-0000-0000-0000-000000000003', N'Cahier des charges',  N'cahier_charges', N'2.4 MB', 1),
('c0000001-0000-0000-0000-000000000007','a0000001-0000-0000-0000-000000000003', N'Rapport d''expertise',N'expertise',     N'5.1 MB', 1),
('c0000001-0000-0000-0000-000000000008','a0000001-0000-0000-0000-000000000003', N'Plans cadastraux',    N'autre',          N'1.2 MB', 1),
('c0000001-0000-0000-0000-000000000009','a0000001-0000-0000-0000-000000000004', N'Cahier des charges',  N'cahier_charges', N'3.8 MB', 1),
('c0000001-0000-0000-0000-000000000010','a0000001-0000-0000-0000-000000000004', N'État locatif',        N'expertise',      N'2.1 MB', 1),
('c0000001-0000-0000-0000-000000000011','a0000001-0000-0000-0000-000000000004', N'Baux en cours',       N'autre',          N'1.5 MB', 1),
('c0000001-0000-0000-0000-000000000012','a0000001-0000-0000-0000-000000000004', N'Diagnostic technique',N'expertise',      N'4.2 MB', 1),
('c0000001-0000-0000-0000-000000000013','a0000001-0000-0000-0000-000000000005', N'Cahier des charges',  N'cahier_charges', N'1.4 MB', 1),
('c0000001-0000-0000-0000-000000000014','a0000001-0000-0000-0000-000000000005', N'Diagnostic DPE',      N'expertise',      N'0.8 MB', 1),
('c0000001-0000-0000-0000-000000000015','a0000001-0000-0000-0000-000000000006', N'Cahier des charges',  N'cahier_charges', N'2.2 MB', 1),
('c0000001-0000-0000-0000-000000000016','a0000001-0000-0000-0000-000000000006', N'Plans architecturaux',N'autre',          N'3.1 MB', 1),
('c0000001-0000-0000-0000-000000000017','a0000001-0000-0000-0000-000000000006', N'Expertise',           N'expertise',      N'2.8 MB', 1),
('c0000001-0000-0000-0000-000000000018','a0000001-0000-0000-0000-000000000007', N'Cahier des charges',  N'cahier_charges', N'1.9 MB', 1),
('c0000001-0000-0000-0000-000000000019','a0000001-0000-0000-0000-000000000007', N'Rapport d''expertise',N'expertise',     N'2.6 MB', 1),
('c0000001-0000-0000-0000-000000000020','a0000001-0000-0000-0000-000000000008', N'Cahier des charges',  N'cahier_charges', N'1.2 MB', 1),
('c0000001-0000-0000-0000-000000000021','a0000001-0000-0000-0000-000000000008', N'Diagnostic complet',  N'expertise',      N'1.8 MB', 1),
('c0000001-0000-0000-0000-000000000022','a0000001-0000-0000-0000-000000000009', N'Cahier des charges',  N'cahier_charges', N'2.5 MB', 1),
('c0000001-0000-0000-0000-000000000023','a0000001-0000-0000-0000-000000000009', N'État des lieux',      N'expertise',      N'1.4 MB', 1),
('c0000001-0000-0000-0000-000000000024','a0000001-0000-0000-0000-000000000009', N'Plans',               N'autre',          N'0.9 MB', 1),
('c0000001-0000-0000-0000-000000000025','a0000001-0000-0000-0000-000000000010', N'Cahier des charges',  N'cahier_charges', N'1.6 MB', 1),
('c0000001-0000-0000-0000-000000000026','a0000001-0000-0000-0000-000000000010', N'Expertise',           N'expertise',      N'2.0 MB', 1),
('c0000001-0000-0000-0000-000000000027','a0000001-0000-0000-0000-000000000011', N'Cahier des charges',  N'cahier_charges', N'1.1 MB', 1),
('c0000001-0000-0000-0000-000000000028','a0000001-0000-0000-0000-000000000011', N'Diagnostic énergétique',N'expertise',    N'0.7 MB', 1),
('c0000001-0000-0000-0000-000000000029','a0000001-0000-0000-0000-000000000012', N'Cahier des charges',  N'cahier_charges', N'2.0 MB', 1),
('c0000001-0000-0000-0000-000000000030','a0000001-0000-0000-0000-000000000012', N'PV assemblée générale',N'autre',         N'1.3 MB', 1),
('c0000001-0000-0000-0000-000000000031','a0000001-0000-0000-0000-000000000013', N'Cahier des charges',  N'cahier_charges', N'2.3 MB', 1),
('c0000001-0000-0000-0000-000000000032','a0000001-0000-0000-0000-000000000013', N'Expertise',           N'expertise',      N'3.0 MB', 1),
('c0000001-0000-0000-0000-000000000033','a0000001-0000-0000-0000-000000000014', N'Cahier des charges',  N'cahier_charges', N'1.7 MB', 1),
('c0000001-0000-0000-0000-000000000034','a0000001-0000-0000-0000-000000000014', N'Diagnostic complet',  N'expertise',      N'2.4 MB', 1),
('c0000001-0000-0000-0000-000000000035','a0000001-0000-0000-0000-000000000015', N'Cahier des charges',  N'cahier_charges', N'1.5 MB', 1),
('c0000001-0000-0000-0000-000000000036','a0000001-0000-0000-0000-000000000015', N'CU opérationnel',     N'autre',          N'0.6 MB', 1),
('c0000001-0000-0000-0000-000000000037','a0000001-0000-0000-0000-000000000016', N'Cahier des charges',  N'cahier_charges', N'1.8 MB', 1),
('c0000001-0000-0000-0000-000000000038','a0000001-0000-0000-0000-000000000016', N'Règlement copropriété',N'autre',         N'1.1 MB', 1),
('c0000001-0000-0000-0000-000000000039','a0000001-0000-0000-0000-000000000017', N'Cahier des charges',  N'cahier_charges', N'2.0 MB', 1),
('c0000001-0000-0000-0000-000000000040','a0000001-0000-0000-0000-000000000017', N'Bail en cours',       N'autre',          N'0.8 MB', 1),
('c0000001-0000-0000-0000-000000000041','a0000001-0000-0000-0000-000000000018', N'Cahier des charges',  N'cahier_charges', N'1.9 MB', 1),
('c0000001-0000-0000-0000-000000000042','a0000001-0000-0000-0000-000000000018', N'Devis travaux',       N'autre',          N'1.4 MB', 1),
('c0000001-0000-0000-0000-000000000043','a0000001-0000-0000-0000-000000000019', N'Cahier des charges',  N'cahier_charges', N'4.2 MB', 1),
('c0000001-0000-0000-0000-000000000044','a0000001-0000-0000-0000-000000000019', N'État locatif complet',N'expertise',      N'3.1 MB', 1),
('c0000001-0000-0000-0000-000000000045','a0000001-0000-0000-0000-000000000019', N'Rapport ravalement',  N'autre',          N'1.8 MB', 1),
('c0000001-0000-0000-0000-000000000046','a0000001-0000-0000-0000-000000000019', N'Jugement',            N'jugement',       N'2.2 MB', 1),
('c0000001-0000-0000-0000-000000000047','a0000001-0000-0000-0000-000000000020', N'Cahier des charges',  N'cahier_charges', N'1.6 MB', 1),
('c0000001-0000-0000-0000-000000000048','a0000001-0000-0000-0000-000000000020', N'Diagnostic DPE G',    N'expertise',      N'1.2 MB', 1),
('c0000001-0000-0000-0000-000000000049','a0000001-0000-0000-0000-000000000020', N'Devis rénovation',    N'autre',          N'2.1 MB', 1);
GO

-- Alerts (1 par enchère)
INSERT INTO dbo.Alerts (Id, AuctionId, [Type], Title, [Message], IsRead) VALUES
('d0000001-0000-0000-0000-000000000001','a0000001-0000-0000-0000-000000000001', N'Opportunity', N'Opportunité exceptionnelle Paris 11e', N'ROI de +67% détecté sur cette enchère — à ne pas manquer !', 0),
('d0000001-0000-0000-0000-000000000002','a0000001-0000-0000-0000-000000000002', N'Opportunity', N'Belle affaire Lyon 6e',                N'Appartement haussmannien à -37% du marché.', 0),
('d0000001-0000-0000-0000-000000000003','a0000001-0000-0000-0000-000000000003', N'Opportunity', N'Maison Chartrons -33%',                N'Maison de ville dans l''un des quartiers les plus recherchés de Bordeaux.', 0),
('d0000001-0000-0000-0000-000000000004','a0000001-0000-0000-0000-000000000004', N'Opportunity', N'Immeuble Marseille centre',            N'Rendement brut estimé à 8.4% dès l''acquisition.', 0),
('d0000001-0000-0000-0000-000000000005','a0000001-0000-0000-0000-000000000005', N'Opportunity', N'Pépite Vieux-Lille',                   N'Appartement rénové à saisir rapidement — audience dans 8 jours.', 0),
('d0000001-0000-0000-0000-000000000006','a0000001-0000-0000-0000-000000000006', N'Opportunity', N'Maison Île de Nantes',                 N'Quartier en forte valorisation — opportunité à +41%.', 0),
('d0000001-0000-0000-0000-000000000007','a0000001-0000-0000-0000-000000000007', N'Opportunity', N'Centre Toulouse -26%',                 N'A 200m du Capitole, décote significative par rapport au marché.', 0),
('d0000001-0000-0000-0000-000000000008','a0000001-0000-0000-0000-000000000008', N'Opportunity', N'Studio Nice vue mer',                  N'Rendement saisonnier estimé à 9.1% brut.', 0),
('d0000001-0000-0000-0000-000000000009','a0000001-0000-0000-0000-000000000009', N'Opportunity', N'Local Strasbourg hypercentre',         N'Rendement commercial potentiel de 9.8% brut.', 0),
('d0000001-0000-0000-0000-000000000010','a0000001-0000-0000-0000-000000000010', N'Info',        N'Enchère Rennes Centre',                N'ROI estimé à +26% — analyse IA de niveau confiance 82%.', 0),
('d0000001-0000-0000-0000-000000000011','a0000001-0000-0000-0000-000000000011', N'Info',        N'Studio Montmartre',                    N'Surface idéale pour une location meublée LMNP.', 0),
('d0000001-0000-0000-0000-000000000012','a0000001-0000-0000-0000-000000000012', N'Info',        N'T4 Lyon 3e',                           N'Bonne affaire dans un secteur résidentiel prisé de Lyon.', 0),
('d0000001-0000-0000-0000-000000000013','a0000001-0000-0000-0000-000000000013', N'Info',        N'Maison Montpellier +20%',              N'Quartier Port Marianne en forte expansion démographique.', 0),
('d0000001-0000-0000-0000-000000000014','a0000001-0000-0000-0000-000000000014', N'Info',        N'Bordeaux Bacalan',                     N'Quartier en forte valorisation grâce aux projets urbains.', 0),
('d0000001-0000-0000-0000-000000000015','a0000001-0000-0000-0000-000000000015', N'Info',        N'Terrain Nantes constructible',         N'Terrain avec CU positif — projet de construction immédiat possible.', 0),
('d0000001-0000-0000-0000-000000000016','a0000001-0000-0000-0000-000000000016', N'Info',        N'Marseille 8e neutre',                  N'Analyse IA : légère décote par rapport au marché, à surveiller.', 0),
('d0000001-0000-0000-0000-000000000017','a0000001-0000-0000-0000-000000000017', N'Info',        N'Local Toulouse neutre',                N'Rendement modéré — bail précaire à surveiller.', 0),
('d0000001-0000-0000-0000-000000000018','a0000001-0000-0000-0000-000000000018', N'Info',        N'Maison Lambersart',                    N'Marché stable — faible potentiel de plus-value à court terme.', 0),
('d0000001-0000-0000-0000-000000000019','a0000001-0000-0000-0000-000000000019', N'Warning',     N'Risque élevé — Immeuble Nice',         N'3 locataires protégés + travaux urgents — ROI négatif estimé à -2%.', 0),
('d0000001-0000-0000-0000-000000000020','a0000001-0000-0000-0000-000000000020', N'Warning',     N'Attention — DPE G Strasbourg',         N'Rénovation énergétique obligatoire estimée à 55 000€ — ROI négatif à -8%.', 0);
GO

PRINT '✓ 49 Documents + 20 Alerts insérés';
PRINT '✓ Base de données prête — Dashboard devrait afficher 20 enchères';
GO
