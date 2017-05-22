
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/29/2017 16:07:07
-- Generated from EDMX file: C:\Users\Michał\Desktop\Xamarin\Bet\DatabaseProject\BetWebService\DatabaseModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [STS2];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__Druzyna__id_liga__145C0A3F]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Team] DROP CONSTRAINT [FK__Druzyna__id_liga__145C0A3F];
GO
IF OBJECT_ID(N'[dbo].[FK__Mecz__id_gosc__1A14E395]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Match] DROP CONSTRAINT [FK__Mecz__id_gosc__1A14E395];
GO
IF OBJECT_ID(N'[dbo].[FK__Mecz__id_gosp__1B0907CE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Match] DROP CONSTRAINT [FK__Mecz__id_gosp__1B0907CE];
GO
IF OBJECT_ID(N'[dbo].[FK__Przypisan__id_me__21B6055D]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Attrib] DROP CONSTRAINT [FK__Przypisan__id_me__21B6055D];
GO
IF OBJECT_ID(N'[dbo].[FK__Przypisan__id_za__22AA2996]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Attrib] DROP CONSTRAINT [FK__Przypisan__id_za__22AA2996];
GO
IF OBJECT_ID(N'[dbo].[FK__Udzial__id_gosc__25869641]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Participation] DROP CONSTRAINT [FK__Udzial__id_gosc__25869641];
GO
IF OBJECT_ID(N'[dbo].[FK__Udzial__id_gosp__267ABA7A]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Participation] DROP CONSTRAINT [FK__Udzial__id_gosp__267ABA7A];
GO
IF OBJECT_ID(N'[dbo].[FK__Udzial__id_mecz__276EDEB3]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Participation] DROP CONSTRAINT [FK__Udzial__id_mecz__276EDEB3];
GO
IF OBJECT_ID(N'[dbo].[FK__Zaklad__id_mecz__1DE57479]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Bet] DROP CONSTRAINT [FK__Zaklad__id_mecz__1DE57479];
GO
IF OBJECT_ID(N'[dbo].[FK__Zaklad__id_user__1ED998B2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Bet] DROP CONSTRAINT [FK__Zaklad__id_user__1ED998B2];
GO
IF OBJECT_ID(N'[dbo].[FK__Zawodnik__id_dru__398D8EEE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Player] DROP CONSTRAINT [FK__Zawodnik__id_dru__398D8EEE];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Attrib]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Attrib];
GO
IF OBJECT_ID(N'[dbo].[Bet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Bet];
GO
IF OBJECT_ID(N'[dbo].[League]', 'U') IS NOT NULL
    DROP TABLE [dbo].[League];
GO
IF OBJECT_ID(N'[dbo].[Match]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Match];
GO
IF OBJECT_ID(N'[dbo].[Participation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Participation];
GO
IF OBJECT_ID(N'[dbo].[Player]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Player];
GO
IF OBJECT_ID(N'[dbo].[Team]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Team];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Attribs'
CREATE TABLE [dbo].[Attribs] (
    [id_przyp] int IDENTITY(1,1) NOT NULL,
    [id_mecz] int  NULL,
    [id_zaklad] int  NULL
);
GO

-- Creating table 'Bets'
CREATE TABLE [dbo].[Bets] (
    [id_zaklad] int IDENTITY(1,1) NOT NULL,
    [kwota] int  NOT NULL,
    [wygrana] int  NOT NULL,
    [id_mecz] int  NULL,
    [id_user] int  NULL,
    [status] char(255)  NULL,
    [typ] int  NULL
);
GO

-- Creating table 'Leagues'
CREATE TABLE [dbo].[Leagues] (
    [id_liga] int IDENTITY(1,1) NOT NULL,
    [nazwa] varchar(255)  NOT NULL
);
GO

-- Creating table 'Matches'
CREATE TABLE [dbo].[Matches] (
    [id_mecz] int IDENTITY(1,1) NOT NULL,
    [kurs] int  NOT NULL,
    [bramki_gosc] int  NOT NULL,
    [bramki_gosp] int  NOT NULL,
    [data] datetime  NOT NULL,
    [minuta] int  NULL,
    [id_gosc] int  NULL,
    [id_gosp] int  NULL
);
GO

-- Creating table 'Participations'
CREATE TABLE [dbo].[Participations] (
    [id_udzial] int IDENTITY(1,1) NOT NULL,
    [id_gosc] int  NULL,
    [id_gosp] int  NULL,
    [id_mecz] int  NULL
);
GO

-- Creating table 'Players'
CREATE TABLE [dbo].[Players] (
    [id_zaw] int IDENTITY(1,1) NOT NULL,
    [imie] varchar(255)  NOT NULL,
    [nazwisko] varchar(255)  NOT NULL,
    [pozycja] varchar(255)  NOT NULL,
    [id_druz] int  NULL,
    [gole] int  NULL,
    [czyste_konto] int  NULL,
    [narodowosc] varchar(255)  NULL
);
GO

-- Creating table 'Teams'
CREATE TABLE [dbo].[Teams] (
    [id_druz] int IDENTITY(1,1) NOT NULL,
    [nazwa] varchar(255)  NOT NULL,
    [stadion] varchar(255)  NOT NULL,
    [id_liga] int  NULL,
    [liga_mistrzow] varchar(3)  NULL,
    [liga_europy] varchar(3)  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [id_user] int IDENTITY(1,1) NOT NULL,
    [login] varchar(255)  NOT NULL,
    [haslo] varchar(255)  NOT NULL,
    [wiek] int  NOT NULL,
    [stan_konta] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id_przyp] in table 'Attribs'
ALTER TABLE [dbo].[Attribs]
ADD CONSTRAINT [PK_Attribs]
    PRIMARY KEY CLUSTERED ([id_przyp] ASC);
GO

-- Creating primary key on [id_zaklad] in table 'Bets'
ALTER TABLE [dbo].[Bets]
ADD CONSTRAINT [PK_Bets]
    PRIMARY KEY CLUSTERED ([id_zaklad] ASC);
GO

-- Creating primary key on [id_liga] in table 'Leagues'
ALTER TABLE [dbo].[Leagues]
ADD CONSTRAINT [PK_Leagues]
    PRIMARY KEY CLUSTERED ([id_liga] ASC);
GO

-- Creating primary key on [id_mecz] in table 'Matches'
ALTER TABLE [dbo].[Matches]
ADD CONSTRAINT [PK_Matches]
    PRIMARY KEY CLUSTERED ([id_mecz] ASC);
GO

-- Creating primary key on [id_udzial] in table 'Participations'
ALTER TABLE [dbo].[Participations]
ADD CONSTRAINT [PK_Participations]
    PRIMARY KEY CLUSTERED ([id_udzial] ASC);
GO

-- Creating primary key on [id_zaw] in table 'Players'
ALTER TABLE [dbo].[Players]
ADD CONSTRAINT [PK_Players]
    PRIMARY KEY CLUSTERED ([id_zaw] ASC);
GO

-- Creating primary key on [id_druz] in table 'Teams'
ALTER TABLE [dbo].[Teams]
ADD CONSTRAINT [PK_Teams]
    PRIMARY KEY CLUSTERED ([id_druz] ASC);
GO

-- Creating primary key on [id_user] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([id_user] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [id_mecz] in table 'Attribs'
ALTER TABLE [dbo].[Attribs]
ADD CONSTRAINT [FK__Przypisan__id_me__21B6055D]
    FOREIGN KEY ([id_mecz])
    REFERENCES [dbo].[Matches]
        ([id_mecz])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Przypisan__id_me__21B6055D'
CREATE INDEX [IX_FK__Przypisan__id_me__21B6055D]
ON [dbo].[Attribs]
    ([id_mecz]);
GO

-- Creating foreign key on [id_zaklad] in table 'Attribs'
ALTER TABLE [dbo].[Attribs]
ADD CONSTRAINT [FK__Przypisan__id_za__22AA2996]
    FOREIGN KEY ([id_zaklad])
    REFERENCES [dbo].[Bets]
        ([id_zaklad])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Przypisan__id_za__22AA2996'
CREATE INDEX [IX_FK__Przypisan__id_za__22AA2996]
ON [dbo].[Attribs]
    ([id_zaklad]);
GO

-- Creating foreign key on [id_mecz] in table 'Bets'
ALTER TABLE [dbo].[Bets]
ADD CONSTRAINT [FK__Zaklad__id_mecz__1DE57479]
    FOREIGN KEY ([id_mecz])
    REFERENCES [dbo].[Matches]
        ([id_mecz])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Zaklad__id_mecz__1DE57479'
CREATE INDEX [IX_FK__Zaklad__id_mecz__1DE57479]
ON [dbo].[Bets]
    ([id_mecz]);
GO

-- Creating foreign key on [id_user] in table 'Bets'
ALTER TABLE [dbo].[Bets]
ADD CONSTRAINT [FK__Zaklad__id_user__1ED998B2]
    FOREIGN KEY ([id_user])
    REFERENCES [dbo].[Users]
        ([id_user])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Zaklad__id_user__1ED998B2'
CREATE INDEX [IX_FK__Zaklad__id_user__1ED998B2]
ON [dbo].[Bets]
    ([id_user]);
GO

-- Creating foreign key on [id_liga] in table 'Teams'
ALTER TABLE [dbo].[Teams]
ADD CONSTRAINT [FK__Druzyna__id_liga__145C0A3F]
    FOREIGN KEY ([id_liga])
    REFERENCES [dbo].[Leagues]
        ([id_liga])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Druzyna__id_liga__145C0A3F'
CREATE INDEX [IX_FK__Druzyna__id_liga__145C0A3F]
ON [dbo].[Teams]
    ([id_liga]);
GO

-- Creating foreign key on [id_gosc] in table 'Matches'
ALTER TABLE [dbo].[Matches]
ADD CONSTRAINT [FK__Mecz__id_gosc__1A14E395]
    FOREIGN KEY ([id_gosc])
    REFERENCES [dbo].[Teams]
        ([id_druz])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Mecz__id_gosc__1A14E395'
CREATE INDEX [IX_FK__Mecz__id_gosc__1A14E395]
ON [dbo].[Matches]
    ([id_gosc]);
GO

-- Creating foreign key on [id_gosp] in table 'Matches'
ALTER TABLE [dbo].[Matches]
ADD CONSTRAINT [FK__Mecz__id_gosp__1B0907CE]
    FOREIGN KEY ([id_gosp])
    REFERENCES [dbo].[Teams]
        ([id_druz])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Mecz__id_gosp__1B0907CE'
CREATE INDEX [IX_FK__Mecz__id_gosp__1B0907CE]
ON [dbo].[Matches]
    ([id_gosp]);
GO

-- Creating foreign key on [id_mecz] in table 'Participations'
ALTER TABLE [dbo].[Participations]
ADD CONSTRAINT [FK__Udzial__id_mecz__276EDEB3]
    FOREIGN KEY ([id_mecz])
    REFERENCES [dbo].[Matches]
        ([id_mecz])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Udzial__id_mecz__276EDEB3'
CREATE INDEX [IX_FK__Udzial__id_mecz__276EDEB3]
ON [dbo].[Participations]
    ([id_mecz]);
GO

-- Creating foreign key on [id_gosc] in table 'Participations'
ALTER TABLE [dbo].[Participations]
ADD CONSTRAINT [FK__Udzial__id_gosc__25869641]
    FOREIGN KEY ([id_gosc])
    REFERENCES [dbo].[Teams]
        ([id_druz])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Udzial__id_gosc__25869641'
CREATE INDEX [IX_FK__Udzial__id_gosc__25869641]
ON [dbo].[Participations]
    ([id_gosc]);
GO

-- Creating foreign key on [id_gosp] in table 'Participations'
ALTER TABLE [dbo].[Participations]
ADD CONSTRAINT [FK__Udzial__id_gosp__267ABA7A]
    FOREIGN KEY ([id_gosp])
    REFERENCES [dbo].[Teams]
        ([id_druz])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Udzial__id_gosp__267ABA7A'
CREATE INDEX [IX_FK__Udzial__id_gosp__267ABA7A]
ON [dbo].[Participations]
    ([id_gosp]);
GO

-- Creating foreign key on [id_druz] in table 'Players'
ALTER TABLE [dbo].[Players]
ADD CONSTRAINT [FK__Zawodnik__id_dru__398D8EEE]
    FOREIGN KEY ([id_druz])
    REFERENCES [dbo].[Teams]
        ([id_druz])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Zawodnik__id_dru__398D8EEE'
CREATE INDEX [IX_FK__Zawodnik__id_dru__398D8EEE]
ON [dbo].[Players]
    ([id_druz]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------