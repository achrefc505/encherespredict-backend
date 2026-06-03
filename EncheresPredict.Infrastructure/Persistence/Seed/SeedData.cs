using EncheresPredict.Domain.Entities;
using EncheresPredict.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace EncheresPredict.Infrastructure.Persistence.Seed;

public static class SeedData
{
    public static async Task InitializeAsync(AppDbContext db)
    {
        if (await db.Auctions.AnyAsync()) return;

        var now = DateTime.UtcNow;

        var auctions = new List<(Auction auction, AiAnalysis ai, List<Document> docs, (AlertType type, string title, string message) alertInfo)>
        {
            CreateAuction("Appartement T3 — Paris 11e", "TJ Paris", "Paris", "Île-de-France",
                "14 rue de la Roquette, 75011 Paris", 68, 3, "Appartement",
                185_000, 310_000, 92, AuctionStatus.Active, now.AddDays(12),
                "Appartement lumineux au 3e étage avec balcon. Travaux de rénovation légers nécessaires.",
                4558m, "hausse", 35_000, 6.8m, 7.5m, 315_000,
                ["Locataire en place", "Pas d'ascenseur"], ["Emplacement premium", "Balcon sud", "DPE C"],
                [("Cahier des charges", "cahier_charges", "1.8 MB"), ("Rapport d'expertise", "expertise", "3.2 MB"), ("Jugement", "jugement", "0.9 MB")],
                (AlertType.Opportunity, "Opportunité exceptionnelle Paris 11e", "ROI de +67% détecté sur cette enchère — à ne pas manquer !")),

            CreateAuction("Appartement T4 — Lyon 6e", "TJ Lyon", "Lyon", "Auvergne-Rhône-Alpes",
                "8 avenue Foch, 69006 Lyon", 92, 4, "Appartement",
                220_000, 350_000, 88, AuctionStatus.Active, now.AddDays(18),
                "Grand appartement haussmannien avec parquet d'origine. Travaux de mise aux normes.",
                3804m, "hausse", 45_000, 5.9m, 6.8m, 358_000,
                ["Copropriété dégradée", "3e étage sans ascenseur"], ["Quartier prisé", "Parquet d'origine", "Hauts plafonds"],
                [("Cahier des charges", "cahier_charges", "2.1 MB"), ("Expertise immobilière", "expertise", "4.5 MB")],
                (AlertType.Opportunity, "Belle affaire Lyon 6e", "Appartement haussmannien à -37% du marché.")),

            CreateAuction("Maison T5 — Bordeaux Chartrons", "TJ Bordeaux", "Bordeaux", "Nouvelle-Aquitaine",
                "22 rue Notre-Dame, 33000 Bordeaux", 145, 5, "Maison",
                280_000, 420_000, 85, AuctionStatus.Active, now.AddDays(25),
                "Maison de ville dans le quartier des Chartrons. Rénovation complète à prévoir.",
                2897m, "hausse", 80_000, 6.2m, 7.1m, 435_000,
                ["Rénovation lourde", "Mitoyenneté des deux côtés"], ["Quartier branché", "Grande surface", "Potentiel locatif élevé"],
                [("Cahier des charges", "cahier_charges", "2.4 MB"), ("Rapport d'expertise", "expertise", "5.1 MB"), ("Plans cadastraux", "autre", "1.2 MB")],
                (AlertType.Opportunity, "Maison Chartrons -33%", "Maison de ville dans l'un des quartiers les plus recherchés de Bordeaux.")),

            CreateAuction("Immeuble de rapport — Marseille 6e", "TJ Marseille", "Marseille", "PACA",
                "15 rue Paradis, 13006 Marseille", 320, 0, "Immeuble de rapport",
                450_000, 680_000, 79, AuctionStatus.Upcoming, now.AddDays(35),
                "Immeuble de 6 appartements. 4 loués, 2 vacants. Rendement locatif immédiat.",
                2125m, "stable", 120_000, 7.2m, 8.4m, 695_000,
                ["2 lots vacants", "Travaux toiture", "Gestion locative complexe"], ["Rendement immédiat", "6 lots", "Quartier central"],
                [("Cahier des charges", "cahier_charges", "3.8 MB"), ("État locatif", "expertise", "2.1 MB"), ("Baux en cours", "autre", "1.5 MB"), ("Diagnostic technique", "expertise", "4.2 MB")],
                (AlertType.Opportunity, "Immeuble Marseille centre", "Rendement brut estimé à 8.4% dès l'acquisition.")),

            CreateAuction("Appartement T2 — Lille Vieux-Lille", "TJ Lille", "Lille", "Hauts-de-France",
                "3 rue de la Monnaie, 59000 Lille", 54, 2, "Appartement",
                95_000, 145_000, 90, AuctionStatus.Active, now.AddDays(8),
                "Appartement rénové dans le Vieux-Lille. Cuisine équipée, parquet. Idéal investisseur.",
                2685m, "hausse", 8_000, 7.8m, 8.6m, 148_000,
                ["Syndic peu réactif"], ["Rénové", "Vieux-Lille", "Forte demande locative", "DPE B"],
                [("Cahier des charges", "cahier_charges", "1.4 MB"), ("Diagnostic DPE", "expertise", "0.8 MB")],
                (AlertType.Opportunity, "Pépite Vieux-Lille", "Appartement rénové à saisir rapidement — audience dans 8 jours.")),

            CreateAuction("Maison T4 — Nantes Île de Nantes", "TJ Nantes", "Nantes", "Pays de la Loire",
                "47 boulevard de la Prairie au Duc, 44200 Nantes", 110, 4, "Maison",
                195_000, 275_000, 83, AuctionStatus.Active, now.AddDays(22),
                "Maison contemporaine avec jardin de 200m². Quartier en pleine rénovation urbaine.",
                2500m, "hausse", 25_000, 5.8m, 6.9m, 285_000,
                ["Zone de rénovation urbaine", "Travaux de finition"], ["Jardin 200m²", "Quartier en devenir", "Maison contemporaine"],
                [("Cahier des charges", "cahier_charges", "2.2 MB"), ("Plans architecturaux", "autre", "3.1 MB"), ("Expertise", "expertise", "2.8 MB")],
                (AlertType.Opportunity, "Maison Île de Nantes", "Quartier en forte valorisation — opportunité à +41%.")),

            CreateAuction("Appartement T3 — Toulouse Capitole", "TJ Toulouse", "Toulouse", "Occitanie",
                "12 rue Saint-Rome, 31000 Toulouse", 78, 3, "Appartement",
                155_000, 210_000, 86, AuctionStatus.Active, now.AddDays(15),
                "Appartement en plein cœur de Toulouse, à 200m du Capitole. Commerces en pied d'immeuble.",
                2692m, "stable", 18_000, 6.1m, 7.0m, 218_000,
                ["Bruit urbain", "Pas de parking"], ["Centre historique", "Forte demande locative", "Commerces"],
                [("Cahier des charges", "cahier_charges", "1.9 MB"), ("Rapport d'expertise", "expertise", "2.6 MB")],
                (AlertType.Opportunity, "Centre Toulouse -26%", "A 200m du Capitole, décote significative par rapport au marché.")),

            CreateAuction("Studio — Nice Promenade", "TJ Nice", "Nice", "PACA",
                "88 Promenade des Anglais, 06000 Nice", 28, 1, "Studio",
                85_000, 115_000, 87, AuctionStatus.Active, now.AddDays(10),
                "Studio vue mer partielle. Immeuble récent. Fort potentiel saisonnier.",
                4107m, "hausse", 5_000, 8.2m, 9.1m, 118_000,
                ["Charges de copropriété élevées", "Petit surface"], ["Vue mer", "Location saisonnière", "Immeuble récent", "DPE A"],
                [("Cahier des charges", "cahier_charges", "1.2 MB"), ("Diagnostic complet", "expertise", "1.8 MB")],
                (AlertType.Opportunity, "Studio Nice vue mer", "Rendement saisonnier estimé à 9.1% brut.")),

            CreateAuction("Local commercial — Strasbourg Centre", "TJ Strasbourg", "Strasbourg", "Grand Est",
                "5 rue des Grandes Arcades, 67000 Strasbourg", 180, 0, "Local commercial",
                320_000, 415_000, 75, AuctionStatus.Upcoming, now.AddDays(40),
                "Local commercial en hypercentre piétonnier. Loyer potentiel 3 500€/mois.",
                2306m, "stable", 40_000, 8.5m, 9.8m, 425_000,
                ["Vacance commerciale 18 mois", "Travaux d'aménagement"], ["Hypercentre piétonnier", "Fort trafic piéton", "Bail commercial flexible"],
                [("Cahier des charges", "cahier_charges", "2.5 MB"), ("État des lieux", "expertise", "1.4 MB"), ("Plans", "autre", "0.9 MB")],
                (AlertType.Opportunity, "Local Strasbourg hypercentre", "Rendement commercial potentiel de 9.8% brut.")),

            CreateAuction("Appartement T2 — Rennes Centre", "TJ Rennes", "Rennes", "Bretagne",
                "18 rue Saint-Georges, 35000 Rennes", 65, 2, "Appartement",
                130_000, 165_000, 82, AuctionStatus.Active, now.AddDays(20),
                "Appartement traversant avec cave. Proche métro et commerces.",
                2538m, "stable", 12_000, 5.4m, 6.2m, 170_000,
                ["Cave humide", "Travaux électricité"], ["Traversant", "Proche métro", "Cave"],
                [("Cahier des charges", "cahier_charges", "1.6 MB"), ("Expertise", "expertise", "2.0 MB")],
                (AlertType.Info, "Enchère Rennes Centre", "ROI estimé à +26% — analyse IA de niveau confiance 82%.")),

            CreateAuction("Studio — Paris 18e Montmartre", "TJ Paris", "Paris", "Île-de-France",
                "3 rue Lepic, 75018 Paris", 22, 1, "Studio",
                75_000, 95_000, 84, AuctionStatus.Active, now.AddDays(6),
                "Studio Montmartre au 4e étage. Idéal investissement locatif meublé.",
                4318m, "hausse", 6_000, 7.1m, 8.0m, 98_000,
                ["Très petite surface", "4e sans ascenseur"], ["Montmartre", "Location meublée", "Forte demande"],
                [("Cahier des charges", "cahier_charges", "1.1 MB"), ("Diagnostic énergétique", "expertise", "0.7 MB")],
                (AlertType.Info, "Studio Montmartre", "Surface idéale pour une location meublée LMNP.")),

            CreateAuction("Appartement T4 — Lyon 3e", "TJ Lyon", "Lyon", "Auvergne-Rhône-Alpes",
                "52 cours Lafayette, 69003 Lyon", 105, 4, "Appartement",
                280_000, 345_000, 78, AuctionStatus.Upcoming, now.AddDays(30),
                "Grand appartement familial avec balcon et cave. Copropriété bien entretenue.",
                3286m, "stable", 30_000, 4.8m, 5.6m, 352_000,
                ["Charges copropriété élevées", "Marché tendu"], ["Grande surface", "Balcon", "Cave", "Bien entretenu"],
                [("Cahier des charges", "cahier_charges", "2.0 MB"), ("PV assemblée générale", "autre", "1.3 MB")],
                (AlertType.Info, "T4 Lyon 3e", "Bonne affaire dans un secteur résidentiel prisé de Lyon.")),

            CreateAuction("Maison T4 — Montpellier Port Marianne", "TJ Montpellier", "Montpellier", "Occitanie",
                "8 allée de la Citadelle, 34000 Montpellier", 130, 4, "Maison",
                215_000, 258_000, 80, AuctionStatus.Active, now.AddDays(17),
                "Maison moderne dans le nouveau quartier Port Marianne. Garage double.",
                1985m, "hausse", 20_000, 4.5m, 5.3m, 265_000,
                ["Quartier récent, liquidité limitée", "Travaux jardin"], ["Garage double", "Quartier moderne", "Maison récente"],
                [("Cahier des charges", "cahier_charges", "2.3 MB"), ("Expertise", "expertise", "3.0 MB")],
                (AlertType.Info, "Maison Montpellier +20%", "Quartier Port Marianne en forte expansion démographique.")),

            CreateAuction("Appartement T2 — Bordeaux Bacalan", "TJ Bordeaux", "Bordeaux", "Nouvelle-Aquitaine",
                "31 rue Achard, 33300 Bordeaux", 48, 2, "Appartement",
                125_000, 148_000, 77, AuctionStatus.Upcoming, now.AddDays(28),
                "Appartement dans le quartier Bacalan en pleine transformation. Proche Darwin.",
                3083m, "hausse", 10_000, 4.2m, 5.0m, 152_000,
                ["Quartier en transition", "Nuisances sonores chantier"], ["Proche Darwin", "Quartier en mutation", "Bon DPE"],
                [("Cahier des charges", "cahier_charges", "1.7 MB"), ("Diagnostic complet", "expertise", "2.4 MB")],
                (AlertType.Info, "Bordeaux Bacalan", "Quartier en forte valorisation grâce aux projets urbains.")),

            CreateAuction("Terrain constructible — Nantes Sud", "TJ Nantes", "Nantes", "Pays de la Loire",
                "Route de Clisson, 44200 Nantes", 600, 0, "Terrain",
                180_000, 210_000, 72, AuctionStatus.Upcoming, now.AddDays(45),
                "Terrain constructible de 600m² en zone UA. CU positif. Idéal promoteur.",
                350m, "stable", 0, 4.1m, 4.1m, 215_000,
                ["Délai d'obtention du permis", "Réseaux à raccorder"], ["Zone UA constructible", "CU positif", "Bon accès"],
                [("Cahier des charges", "cahier_charges", "1.5 MB"), ("CU opérationnel", "autre", "0.6 MB")],
                (AlertType.Info, "Terrain Nantes constructible", "Terrain avec CU positif — projet de construction immédiat possible.")),

            CreateAuction("Appartement T3 — Marseille 8e", "TJ Marseille", "Marseille", "PACA",
                "24 avenue du Prado, 13008 Marseille", 72, 3, "Appartement",
                145_000, 162_000, 71, AuctionStatus.Active, now.AddDays(14),
                "Appartement dans résidence sécurisée avec gardien. Parking sous-sol.",
                2250m, "stable", 15_000, 3.8m, 4.6m, 166_000,
                ["Charges élevées", "Marché marseillais volatile"], ["Résidence sécurisée", "Parking", "Gardien"],
                [("Cahier des charges", "cahier_charges", "1.8 MB"), ("Règlement copropriété", "autre", "1.1 MB")],
                (AlertType.Info, "Marseille 8e neutre", "Analyse IA : légère décote par rapport au marché, à surveiller.")),

            CreateAuction("Local commercial — Toulouse Esquirol", "TJ Toulouse", "Toulouse", "Occitanie",
                "9 place Esquirol, 31000 Toulouse", 95, 0, "Local commercial",
                190_000, 205_000, 68, AuctionStatus.Upcoming, now.AddDays(50),
                "Local en rez-de-chaussée place animée. Actuellement loué 1 400€/mois (bail précaire).",
                2158m, "stable", 10_000, 3.5m, 4.2m, 210_000,
                ["Bail précaire non renouvelable", "Concurrence commerciale forte"], ["Place animée", "Loué", "Bonne visibilité"],
                [("Cahier des charges", "cahier_charges", "2.0 MB"), ("Bail en cours", "autre", "0.8 MB")],
                (AlertType.Info, "Local Toulouse neutre", "Rendement modéré — bail précaire à surveiller.")),

            CreateAuction("Maison T3 — Lille Lambersart", "TJ Lille", "Lille", "Hauts-de-France",
                "15 rue de la Barre, 59130 Lambersart", 88, 3, "Maison",
                160_000, 168_000, 74, AuctionStatus.Active, now.AddDays(19),
                "Maison mitoyenne avec jardin. Travaux de toiture récents. Quartier résidentiel calme.",
                1909m, "stable", 20_000, 3.1m, 4.0m, 172_000,
                ["Marché peu liquide", "Mitoyenneté"], ["Jardin", "Toiture neuve", "Quartier calme"],
                [("Cahier des charges", "cahier_charges", "1.9 MB"), ("Devis travaux", "autre", "1.4 MB")],
                (AlertType.Info, "Maison Lambersart", "Marché stable — faible potentiel de plus-value à court terme.")),

            CreateAuction("Immeuble de rapport — Nice Est", "TJ Nice", "Nice", "PACA",
                "15 avenue de la Californie, 06000 Nice", 410, 0, "Immeuble de rapport",
                780_000, 760_000, 62, AuctionStatus.Upcoming, now.AddDays(60),
                "Immeuble de 8 appartements dont 3 occupés par des locataires protégés.",
                1854m, "baisse", 200_000, 2.1m, 3.2m, 748_000,
                ["3 locataires protégés", "Travaux ravalement façade urgent", "Copropriété en difficulté", "Marché en baisse secteur"],
                ["8 lots", "Potentiel à terme"],
                [("Cahier des charges", "cahier_charges", "4.2 MB"), ("État locatif complet", "expertise", "3.1 MB"), ("Rapport ravalement", "autre", "1.8 MB"), ("Jugement", "jugement", "2.2 MB")],
                (AlertType.Warning, "Risque élevé — Immeuble Nice", "3 locataires protégés + travaux urgents — ROI négatif estimé à -2%.")),

            CreateAuction("Appartement T2 — Strasbourg Neudorf", "TJ Strasbourg", "Strasbourg", "Grand Est",
                "44 route de Colmar, 67100 Strasbourg", 58, 2, "Appartement",
                135_000, 124_000, 65, AuctionStatus.Upcoming, now.AddDays(55),
                "Appartement au rez-de-chaussée côté rue. Humidité constatée. DPE G.",
                2138m, "baisse", 55_000, 1.2m, 2.8m, 122_000,
                ["DPE G — rénovation énergétique obligatoire", "RDC côté rue", "Humidité", "Marché en baisse secteur"],
                ["Prix d'entrée bas"],
                [("Cahier des charges", "cahier_charges", "1.6 MB"), ("Diagnostic DPE G", "expertise", "1.2 MB"), ("Devis rénovation", "autre", "2.1 MB")],
                (AlertType.Warning, "Attention — DPE G Strasbourg", "Rénovation énergétique obligatoire estimée à 55 000€ — ROI négatif à -8%."))
        };

        foreach (var (auction, ai, docs, alertInfo) in auctions)
        {
            await db.Auctions.AddAsync(auction);
            await db.SaveChangesAsync();

            ai.GetType().GetProperty("AuctionId")!.SetValue(ai, auction.Id);
            await db.AiAnalyses.AddAsync(ai);

            foreach (var doc in docs)
                await db.Documents.AddAsync(doc);

            var alert = Alert.Create(alertInfo.type, alertInfo.title, alertInfo.message, auction.Id);
            await db.Alerts.AddAsync(alert);

            await db.SaveChangesAsync();
        }
    }

    private static (Auction, AiAnalysis, List<Document>, (AlertType type, string title, string message)) CreateAuction(
        string title, string tribunal, string city, string region, string address,
        int surface, int rooms, string type,
        decimal startPrice, decimal aiEstimate, int confidence,
        AuctionStatus status, DateTime auctionDate, string description,
        decimal pricePerSqm, string marketTrend, decimal renovationCost,
        decimal netYield, decimal grossYield, decimal potentialResalePrice,
        List<string> riskFactors, List<string> strengths,
        List<(string name, string docType, string size)> docInfos,
        (AlertType type, string title, string message) alertInfo)
    {
        var auction = Auction.Create(title, tribunal, city, region, address, surface, rooms, type,
            startPrice, aiEstimate, confidence, status, auctionDate, description);

        var ai = AiAnalysis.Create(auction.Id, pricePerSqm, marketTrend, renovationCost,
            netYield, grossYield, potentialResalePrice, riskFactors, strengths);

        var docs = docInfos.Select(d => Document.Create(auction.Id, d.name, d.docType, d.size)).ToList();

        return (auction, ai, docs, alertInfo);
    }
}
