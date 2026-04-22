-- ============================================================================
-- EncheresPredict — Schema (DDL)
-- SQL Server / LocalDB
-- Généré à partir des entités EF Core (DDD)
-- ============================================================================

-- Création de la base si elle n'existe pas
IF DB_ID('EncheresPredict') IS NULL
BEGIN
    CREATE DATABASE EncheresPredict;
END
GO

USE EncheresPredict;
GO

-- ============================================================================
-- DROP en cascade si on rejoue le script (ordre inverse des FK)
-- ============================================================================
IF OBJECT_ID('dbo.Documents',    'U') IS NOT NULL DROP TABLE dbo.Documents;
IF OBJECT_ID('dbo.AiAnalyses',   'U') IS NOT NULL DROP TABLE dbo.AiAnalyses;
IF OBJECT_ID('dbo.Alerts',       'U') IS NOT NULL DROP TABLE dbo.Alerts;
IF OBJECT_ID('dbo.Auctions',     'U') IS NOT NULL DROP TABLE dbo.Auctions;
IF OBJECT_ID('dbo.UserProfiles', 'U') IS NOT NULL DROP TABLE dbo.UserProfiles;
GO

-- ============================================================================
-- Table : Auctions  (Aggregate Root)
-- ============================================================================
CREATE TABLE dbo.Auctions
(
    Id                  UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_Auctions PRIMARY KEY,
    Title               NVARCHAR(200)    NOT NULL,
    Tribunal            NVARCHAR(100)    NOT NULL,
    City                NVARCHAR(100)    NOT NULL,
    Region              NVARCHAR(100)    NOT NULL,
    [Address]           NVARCHAR(300)    NULL,
    Surface             INT              NOT NULL,
    Rooms               INT              NOT NULL,
    [Type]              NVARCHAR(50)     NOT NULL,
    StartPriceAmount    DECIMAL(18,2)    NOT NULL,
    AiEstimateAmount    DECIMAL(18,2)    NOT NULL,
    Confidence          INT              NOT NULL,
    RoiValue            DECIMAL(10,2)    NOT NULL,
    Badge               NVARCHAR(50)     NOT NULL,    -- enum BadgeType (string)
    [Status]            NVARCHAR(50)     NOT NULL,    -- enum AuctionStatus (string)
    AuctionDate         DATETIME2        NOT NULL,
    [Description]       NVARCHAR(2000)   NULL,
    CreatedAt           DATETIME2        NOT NULL CONSTRAINT DF_Auctions_CreatedAt DEFAULT (SYSUTCDATETIME())
);
GO

CREATE INDEX IX_Auctions_City      ON dbo.Auctions(City);
CREATE INDEX IX_Auctions_Region    ON dbo.Auctions(Region);
CREATE INDEX IX_Auctions_Badge     ON dbo.Auctions(Badge);
CREATE INDEX IX_Auctions_Status    ON dbo.Auctions([Status]);
CREATE INDEX IX_Auctions_RoiValue  ON dbo.Auctions(RoiValue DESC);
GO

-- ============================================================================
-- Table : AiAnalyses  (1-to-1 avec Auctions, cascade)
-- ============================================================================
CREATE TABLE dbo.AiAnalyses
(
    Id                      UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_AiAnalyses PRIMARY KEY,
    AuctionId               UNIQUEIDENTIFIER NOT NULL,
    PricePerSqm             DECIMAL(18,2)    NOT NULL,
    MarketTrend             NVARCHAR(50)     NOT NULL,
    RenovationCost          DECIMAL(18,2)    NOT NULL,
    NetYield                DECIMAL(18,2)    NOT NULL,
    GrossYield              DECIMAL(18,2)    NOT NULL,
    PotentialResalePrice    DECIMAL(18,2)    NOT NULL,
    RiskFactorsJson         NVARCHAR(MAX)    NOT NULL CONSTRAINT DF_AiAnalyses_Risks    DEFAULT (N'[]'),
    StrengthsJson           NVARCHAR(MAX)    NOT NULL CONSTRAINT DF_AiAnalyses_Strs     DEFAULT (N'[]'),
    ModelVersion            NVARCHAR(20)     NOT NULL CONSTRAINT DF_AiAnalyses_ModelVer DEFAULT (N'v2.1'),
    AnalyzedAt              DATETIME2        NOT NULL CONSTRAINT DF_AiAnalyses_AnAt     DEFAULT (SYSUTCDATETIME()),
    CreatedAt               DATETIME2        NOT NULL CONSTRAINT DF_AiAnalyses_CreatedAt DEFAULT (SYSUTCDATETIME()),

    CONSTRAINT UQ_AiAnalyses_AuctionId UNIQUE (AuctionId),
    CONSTRAINT FK_AiAnalyses_Auctions  FOREIGN KEY (AuctionId)
        REFERENCES dbo.Auctions(Id) ON DELETE CASCADE
);
GO

-- ============================================================================
-- Table : Documents  (1-to-many avec Auctions, cascade)
-- ============================================================================
CREATE TABLE dbo.Documents
(
    Id          UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_Documents PRIMARY KEY,
    AuctionId   UNIQUEIDENTIFIER NOT NULL,
    [Name]      NVARCHAR(200)    NOT NULL,
    [Type]      NVARCHAR(50)     NOT NULL,
    [Size]      NVARCHAR(20)     NOT NULL,
    Available   BIT              NOT NULL CONSTRAINT DF_Documents_Available DEFAULT (1),
    CreatedAt   DATETIME2        NOT NULL CONSTRAINT DF_Documents_CreatedAt DEFAULT (SYSUTCDATETIME()),

    CONSTRAINT FK_Documents_Auctions FOREIGN KEY (AuctionId)
        REFERENCES dbo.Auctions(Id) ON DELETE CASCADE
);
GO

CREATE INDEX IX_Documents_AuctionId ON dbo.Documents(AuctionId);
GO

-- ============================================================================
-- Table : Alerts  (FK optionnelle vers Auctions, NO cascade)
-- ============================================================================
CREATE TABLE dbo.Alerts
(
    Id          UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_Alerts PRIMARY KEY,
    AuctionId   UNIQUEIDENTIFIER NULL,
    [Type]      NVARCHAR(50)     NOT NULL,    -- enum AlertType (string)
    Title       NVARCHAR(200)    NOT NULL,
    [Message]   NVARCHAR(1000)   NOT NULL,
    IsRead      BIT              NOT NULL CONSTRAINT DF_Alerts_IsRead DEFAULT (0),
    CreatedAt   DATETIME2        NOT NULL CONSTRAINT DF_Alerts_CreatedAt DEFAULT (SYSUTCDATETIME())
);
GO

CREATE INDEX IX_Alerts_AuctionId ON dbo.Alerts(AuctionId);
CREATE INDEX IX_Alerts_IsRead    ON dbo.Alerts(IsRead);
GO

-- ============================================================================
-- Table : UserProfiles
-- ============================================================================
CREATE TABLE dbo.UserProfiles
(
    Id          UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_UserProfiles PRIMARY KEY,
    Profile     NVARCHAR(50)     NOT NULL,
    Regions     NVARCHAR(MAX)    NOT NULL CONSTRAINT DF_UserProfiles_Regions DEFAULT (N'[]'),
    BudgetMin   DECIMAL(18,2)    NOT NULL,
    BudgetMax   DECIMAL(18,2)    NOT NULL,
    Types       NVARCHAR(MAX)    NOT NULL CONSTRAINT DF_UserProfiles_Types DEFAULT (N'[]'),
    CreatedAt   DATETIME2        NOT NULL CONSTRAINT DF_UserProfiles_CreatedAt DEFAULT (SYSUTCDATETIME())
);
GO

PRINT '✓ Schéma EncheresPredict créé avec succès';
GO
