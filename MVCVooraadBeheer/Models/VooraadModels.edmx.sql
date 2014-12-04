
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/01/2014 22:13:24
-- Generated from EDMX file: E:\data barld\Documents\Visual Studio 14\Projects\MVCVooraadBeheer\MVCVooraadBeheer\Models\VooraadModels.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [aspnet-MVCVooraadBeheer-20141119045826];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_LanguageMagazineSerie]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MagazineTitleSet] DROP CONSTRAINT [FK_LanguageMagazineSerie];
GO
IF OBJECT_ID(N'[dbo].[FK_LeverancierMagazineTransaction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MagazineTransactionSet] DROP CONSTRAINT [FK_LeverancierMagazineTransaction];
GO
IF OBJECT_ID(N'[dbo].[FK_LeverancierProductLeverancier]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MagazineTitleLeverancierSet] DROP CONSTRAINT [FK_LeverancierProductLeverancier];
GO
IF OBJECT_ID(N'[dbo].[FK_LocationMagazineTitleWarningMagazineTitle]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LocationMagazineTitleWarningSet] DROP CONSTRAINT [FK_LocationMagazineTitleWarningMagazineTitle];
GO
IF OBJECT_ID(N'[dbo].[FK_LocationMagazineWarningLocation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LocationMagazineTitleWarningSet] DROP CONSTRAINT [FK_LocationMagazineWarningLocation];
GO
IF OBJECT_ID(N'[dbo].[FK_MagazineMagazineTransaction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MagazineTransactionSet] DROP CONSTRAINT [FK_MagazineMagazineTransaction];
GO
IF OBJECT_ID(N'[dbo].[FK_MagazineSerieMagazine]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MagazineSet] DROP CONSTRAINT [FK_MagazineSerieMagazine];
GO
IF OBJECT_ID(N'[dbo].[FK_MagazineSerieMagazineSerieLeverancier]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MagazineTitleLeverancierSet] DROP CONSTRAINT [FK_MagazineSerieMagazineSerieLeverancier];
GO
IF OBJECT_ID(N'[dbo].[FK_MagazineTransactionLocation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MagazineTransactionSet] DROP CONSTRAINT [FK_MagazineTransactionLocation];
GO
IF OBJECT_ID(N'[dbo].[FK_MagazineTransactionLocation1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MagazineTransactionSet] DROP CONSTRAINT [FK_MagazineTransactionLocation1];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[LanguageSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LanguageSet];
GO
IF OBJECT_ID(N'[dbo].[LeverancierSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LeverancierSet];
GO
IF OBJECT_ID(N'[dbo].[LocationMagazineTitleWarningSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LocationMagazineTitleWarningSet];
GO
IF OBJECT_ID(N'[dbo].[LocationSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LocationSet];
GO
IF OBJECT_ID(N'[dbo].[MagazineSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MagazineSet];
GO
IF OBJECT_ID(N'[dbo].[MagazineTitleLeverancierSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MagazineTitleLeverancierSet];
GO
IF OBJECT_ID(N'[dbo].[MagazineTitleSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MagazineTitleSet];
GO
IF OBJECT_ID(N'[dbo].[MagazineTransactionSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MagazineTransactionSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'LocationSet'
CREATE TABLE [dbo].[LocationSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Adresse] nvarchar(max)  NOT NULL,
    [PostCode] nvarchar(max)  NOT NULL,
    [City] nvarchar(max)  NOT NULL,
    [Active] bit  NOT NULL
);
GO

-- Creating table 'MagazineSet'
CREATE TABLE [dbo].[MagazineSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Active] bit  NOT NULL,
    [Jaargang] smallint  NULL,
    [nummer] smallint  NULL,
    [Verschijning] datetime  NOT NULL,
    [UitSchappen] datetime  NULL,
    [BarCode] int  NOT NULL,
    [Price] int  NOT NULL,
    [MagazineTitleId] int  NOT NULL
);
GO

-- Creating table 'MagazineTransactionSet'
CREATE TABLE [dbo].[MagazineTransactionSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DateTime] datetime  NOT NULL,
    [Value] int  NOT NULL,
    [LocationFromId] int  NULL,
    [LocationToId] int  NULL,
    [TransactionType] int  NOT NULL,
    [MagazineId] int  NOT NULL,
    [LeverancierId] int  NOT NULL
);
GO

-- Creating table 'LeverancierSet'
CREATE TABLE [dbo].[LeverancierSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Adresse] nvarchar(max)  NOT NULL,
    [PostCode] nvarchar(max)  NOT NULL,
    [City] nvarchar(max)  NOT NULL,
    [Active] bit  NOT NULL
);
GO

-- Creating table 'MagazineTitleLeverancierSet'
CREATE TABLE [dbo].[MagazineTitleLeverancierSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Leverancier_Id] int  NOT NULL,
    [MagazineSerie_Id] int  NOT NULL
);
GO

-- Creating table 'MagazineTitleSet'
CREATE TABLE [dbo].[MagazineTitleSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Language_Id] int  NOT NULL
);
GO

-- Creating table 'LanguageSet'
CREATE TABLE [dbo].[LanguageSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [ShortName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'LocationMagazineTitleWarningSet'
CREATE TABLE [dbo].[LocationMagazineTitleWarningSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Active] bit  NOT NULL,
    [ActiveTo] datetime  NULL,
    [value] nvarchar(max)  NOT NULL,
    [Location_Id] int  NOT NULL,
    [MagazineTitle_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'LocationSet'
ALTER TABLE [dbo].[LocationSet]
ADD CONSTRAINT [PK_LocationSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MagazineSet'
ALTER TABLE [dbo].[MagazineSet]
ADD CONSTRAINT [PK_MagazineSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MagazineTransactionSet'
ALTER TABLE [dbo].[MagazineTransactionSet]
ADD CONSTRAINT [PK_MagazineTransactionSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LeverancierSet'
ALTER TABLE [dbo].[LeverancierSet]
ADD CONSTRAINT [PK_LeverancierSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MagazineTitleLeverancierSet'
ALTER TABLE [dbo].[MagazineTitleLeverancierSet]
ADD CONSTRAINT [PK_MagazineTitleLeverancierSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MagazineTitleSet'
ALTER TABLE [dbo].[MagazineTitleSet]
ADD CONSTRAINT [PK_MagazineTitleSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LanguageSet'
ALTER TABLE [dbo].[LanguageSet]
ADD CONSTRAINT [PK_LanguageSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LocationMagazineTitleWarningSet'
ALTER TABLE [dbo].[LocationMagazineTitleWarningSet]
ADD CONSTRAINT [PK_LocationMagazineTitleWarningSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [LocationFromId] in table 'MagazineTransactionSet'
ALTER TABLE [dbo].[MagazineTransactionSet]
ADD CONSTRAINT [FK_MagazineTransactionLocation]
    FOREIGN KEY ([LocationFromId])
    REFERENCES [dbo].[LocationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MagazineTransactionLocation'
CREATE INDEX [IX_FK_MagazineTransactionLocation]
ON [dbo].[MagazineTransactionSet]
    ([LocationFromId]);
GO

-- Creating foreign key on [LocationToId] in table 'MagazineTransactionSet'
ALTER TABLE [dbo].[MagazineTransactionSet]
ADD CONSTRAINT [FK_MagazineTransactionLocation1]
    FOREIGN KEY ([LocationToId])
    REFERENCES [dbo].[LocationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MagazineTransactionLocation1'
CREATE INDEX [IX_FK_MagazineTransactionLocation1]
ON [dbo].[MagazineTransactionSet]
    ([LocationToId]);
GO

-- Creating foreign key on [Location_Id] in table 'LocationMagazineTitleWarningSet'
ALTER TABLE [dbo].[LocationMagazineTitleWarningSet]
ADD CONSTRAINT [FK_LocationMagazineWarningLocation]
    FOREIGN KEY ([Location_Id])
    REFERENCES [dbo].[LocationSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LocationMagazineWarningLocation'
CREATE INDEX [IX_FK_LocationMagazineWarningLocation]
ON [dbo].[LocationMagazineTitleWarningSet]
    ([Location_Id]);
GO

-- Creating foreign key on [Leverancier_Id] in table 'MagazineTitleLeverancierSet'
ALTER TABLE [dbo].[MagazineTitleLeverancierSet]
ADD CONSTRAINT [FK_LeverancierProductLeverancier]
    FOREIGN KEY ([Leverancier_Id])
    REFERENCES [dbo].[LeverancierSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LeverancierProductLeverancier'
CREATE INDEX [IX_FK_LeverancierProductLeverancier]
ON [dbo].[MagazineTitleLeverancierSet]
    ([Leverancier_Id]);
GO

-- Creating foreign key on [MagazineSerie_Id] in table 'MagazineTitleLeverancierSet'
ALTER TABLE [dbo].[MagazineTitleLeverancierSet]
ADD CONSTRAINT [FK_MagazineSerieMagazineSerieLeverancier]
    FOREIGN KEY ([MagazineSerie_Id])
    REFERENCES [dbo].[MagazineTitleSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MagazineSerieMagazineSerieLeverancier'
CREATE INDEX [IX_FK_MagazineSerieMagazineSerieLeverancier]
ON [dbo].[MagazineTitleLeverancierSet]
    ([MagazineSerie_Id]);
GO

-- Creating foreign key on [MagazineTitle_Id] in table 'LocationMagazineTitleWarningSet'
ALTER TABLE [dbo].[LocationMagazineTitleWarningSet]
ADD CONSTRAINT [FK_LocationMagazineTitleWarningMagazineTitle]
    FOREIGN KEY ([MagazineTitle_Id])
    REFERENCES [dbo].[MagazineTitleSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LocationMagazineTitleWarningMagazineTitle'
CREATE INDEX [IX_FK_LocationMagazineTitleWarningMagazineTitle]
ON [dbo].[LocationMagazineTitleWarningSet]
    ([MagazineTitle_Id]);
GO

-- Creating foreign key on [Language_Id] in table 'MagazineTitleSet'
ALTER TABLE [dbo].[MagazineTitleSet]
ADD CONSTRAINT [FK_LanguageMagazineSerie]
    FOREIGN KEY ([Language_Id])
    REFERENCES [dbo].[LanguageSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LanguageMagazineSerie'
CREATE INDEX [IX_FK_LanguageMagazineSerie]
ON [dbo].[MagazineTitleSet]
    ([Language_Id]);
GO

-- Creating foreign key on [MagazineTitleId] in table 'MagazineSet'
ALTER TABLE [dbo].[MagazineSet]
ADD CONSTRAINT [FK_MagazineMagazineTitle]
    FOREIGN KEY ([MagazineTitleId])
    REFERENCES [dbo].[MagazineTitleSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MagazineMagazineTitle'
CREATE INDEX [IX_FK_MagazineMagazineTitle]
ON [dbo].[MagazineSet]
    ([MagazineTitleId]);
GO

-- Creating foreign key on [MagazineId] in table 'MagazineTransactionSet'
ALTER TABLE [dbo].[MagazineTransactionSet]
ADD CONSTRAINT [FK_MagazineTransactionMagazine]
    FOREIGN KEY ([MagazineId])
    REFERENCES [dbo].[MagazineSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MagazineTransactionMagazine'
CREATE INDEX [IX_FK_MagazineTransactionMagazine]
ON [dbo].[MagazineTransactionSet]
    ([MagazineId]);
GO

-- Creating foreign key on [LeverancierId] in table 'MagazineTransactionSet'
ALTER TABLE [dbo].[MagazineTransactionSet]
ADD CONSTRAINT [FK_MagazineTransactionLeverancier]
    FOREIGN KEY ([LeverancierId])
    REFERENCES [dbo].[LeverancierSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MagazineTransactionLeverancier'
CREATE INDEX [IX_FK_MagazineTransactionLeverancier]
ON [dbo].[MagazineTransactionSet]
    ([LeverancierId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------