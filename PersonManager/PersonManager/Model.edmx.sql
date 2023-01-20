
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/07/2022 18:57:16
-- Generated from EDMX file: C:\Users\Next Design\Desktop\PPPK - Projekt\4_Delivery\PersonManager\PersonManager\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [PersonShoeManager];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'People'
CREATE TABLE [dbo].[People] (
    [IDPerson] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(20)  NOT NULL,
    [LastName] nvarchar(20)  NOT NULL,
    [Age] int  NOT NULL,
    [Contact] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Shoes'
CREATE TABLE [dbo].[Shoes] (
    [IShoe] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Size] int  NOT NULL,
    [IDPerson] int  NOT NULL
);
GO

-- Creating table 'UploadedFiles'
CREATE TABLE [dbo].[UploadedFiles] (
    [IDUploadedFile] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(100)  NOT NULL,
    [ContentType] nvarchar(20)  NOT NULL,
    [Content] varbinary(max)  NOT NULL,
    [ShoeIShoe] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IDPerson] in table 'People'
ALTER TABLE [dbo].[People]
ADD CONSTRAINT [PK_People]
    PRIMARY KEY CLUSTERED ([IDPerson] ASC);
GO

-- Creating primary key on [IShoe] in table 'Shoes'
ALTER TABLE [dbo].[Shoes]
ADD CONSTRAINT [PK_Shoes]
    PRIMARY KEY CLUSTERED ([IShoe] ASC);
GO

-- Creating primary key on [IDUploadedFile] in table 'UploadedFiles'
ALTER TABLE [dbo].[UploadedFiles]
ADD CONSTRAINT [PK_UploadedFiles]
    PRIMARY KEY CLUSTERED ([IDUploadedFile] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [IDPerson] in table 'Shoes'
ALTER TABLE [dbo].[Shoes]
ADD CONSTRAINT [FK_PersonShoe]
    FOREIGN KEY ([IDPerson])
    REFERENCES [dbo].[People]
        ([IDPerson])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonShoe'
CREATE INDEX [IX_FK_PersonShoe]
ON [dbo].[Shoes]
    ([IDPerson]);
GO

-- Creating foreign key on [ShoeIShoe] in table 'UploadedFiles'
ALTER TABLE [dbo].[UploadedFiles]
ADD CONSTRAINT [FK_ShoeUploadedFile]
    FOREIGN KEY ([ShoeIShoe])
    REFERENCES [dbo].[Shoes]
        ([IShoe])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ShoeUploadedFile'
CREATE INDEX [IX_FK_ShoeUploadedFile]
ON [dbo].[UploadedFiles]
    ([ShoeIShoe]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------