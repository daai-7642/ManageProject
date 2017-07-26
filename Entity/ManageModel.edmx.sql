
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/26/2017 20:09:09
-- Generated from EDMX file: F:\项目\ManageProject\Entity\ManageModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SuperDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_dbo_UserClaim_dbo_MyUsers_IdentityUser_Id]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserClaim] DROP CONSTRAINT [FK_dbo_UserClaim_dbo_MyUsers_IdentityUser_Id];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_UserLogin_dbo_MyUsers_IdentityUser_Id]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserLogin] DROP CONSTRAINT [FK_dbo_UserLogin_dbo_MyUsers_IdentityUser_Id];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_UserRole_dbo_MyRoles_IdentityRole_Id]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [FK_dbo_UserRole_dbo_MyRoles_IdentityRole_Id];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_UserRole_dbo_MyUsers_IdentityUser_Id]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [FK_dbo_UserRole_dbo_MyUsers_IdentityUser_Id];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Address_City]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Address_City];
GO
IF OBJECT_ID(N'[dbo].[Address_County]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Address_County];
GO
IF OBJECT_ID(N'[dbo].[Address_Province]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Address_Province];
GO
IF OBJECT_ID(N'[dbo].[Address_Town]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Address_Town];
GO
IF OBJECT_ID(N'[dbo].[Address_Village]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Address_Village];
GO
IF OBJECT_ID(N'[dbo].[AddressBase]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AddressBase];
GO
IF OBJECT_ID(N'[dbo].[MyRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MyRoles];
GO
IF OBJECT_ID(N'[dbo].[MyUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MyUsers];
GO
IF OBJECT_ID(N'[dbo].[RoleFunction]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RoleFunction];
GO
IF OBJECT_ID(N'[dbo].[UserClaim]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserClaim];
GO
IF OBJECT_ID(N'[dbo].[UserLogin]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserLogin];
GO
IF OBJECT_ID(N'[dbo].[UserRole]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserRole];
GO
IF OBJECT_ID(N'[SuperDBModelStoreContainer].[C__MigrationHistory]', 'U') IS NOT NULL
    DROP TABLE [SuperDBModelStoreContainer].[C__MigrationHistory];
GO
IF OBJECT_ID(N'[SuperDBModelStoreContainer].[Function]', 'U') IS NOT NULL
    DROP TABLE [SuperDBModelStoreContainer].[Function];
GO
IF OBJECT_ID(N'[SuperDBModelStoreContainer].[FunctionGroup]', 'U') IS NOT NULL
    DROP TABLE [SuperDBModelStoreContainer].[FunctionGroup];
GO
IF OBJECT_ID(N'[SuperDBModelStoreContainer].[MyLogger]', 'U') IS NOT NULL
    DROP TABLE [SuperDBModelStoreContainer].[MyLogger];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'C__MigrationHistory'
CREATE TABLE [dbo].[C__MigrationHistory] (
    [MigrationId] nvarchar(150)  NOT NULL,
    [ContextKey] nvarchar(300)  NOT NULL,
    [Model] varbinary(max)  NOT NULL,
    [ProductVersion] nvarchar(32)  NOT NULL
);
GO

-- Creating table 'Function'
CREATE TABLE [dbo].[Function] (
    [FunctionID] int IDENTITY(1,1) NOT NULL,
    [GroupID] int  NOT NULL,
    [FunctionName] nvarchar(50)  NULL,
    [PageURL] nvarchar(200)  NULL,
    [OrderNo] int  NOT NULL,
    [Description] nvarchar(200)  NULL,
    [Status] int  NOT NULL
);
GO

-- Creating table 'FunctionGroup'
CREATE TABLE [dbo].[FunctionGroup] (
    [GroupID] int IDENTITY(1,1) NOT NULL,
    [GroupName] nvarchar(50)  NULL,
    [ParentID] int  NOT NULL,
    [GroupCode] nvarchar(50)  NULL,
    [OrderNo] int  NOT NULL,
    [State] smallint  NOT NULL,
    [Description] nvarchar(100)  NULL
);
GO

-- Creating table 'MyRoles'
CREATE TABLE [dbo].[MyRoles] (
    [Id] nvarchar(128)  NOT NULL,
    [Name] nvarchar(50)  NULL,
    [Description] nvarchar(200)  NULL,
    [Status] int  NOT NULL
);
GO

-- Creating table 'MyUsers'
CREATE TABLE [dbo].[MyUsers] (
    [Id] nvarchar(128)  NOT NULL,
    [Email] nvarchar(max)  NULL,
    [EmailConfirmed] bit  NOT NULL,
    [PasswordHash] nvarchar(max)  NULL,
    [SecurityStamp] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [PhoneNumberConfirmed] bit  NOT NULL,
    [TwoFactorEnabled] bit  NOT NULL,
    [LockoutEndDateUtc] datetime  NULL,
    [LockoutEnabled] bit  NOT NULL,
    [AccessFailedCount] int  NOT NULL,
    [UserName] nvarchar(max)  NULL,
    [Discriminator] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'UserClaim'
CREATE TABLE [dbo].[UserClaim] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(max)  NULL,
    [ClaimType] nvarchar(max)  NULL,
    [ClaimValue] nvarchar(max)  NULL,
    [IdentityUser_Id] nvarchar(128)  NULL
);
GO

-- Creating table 'UserLogin'
CREATE TABLE [dbo].[UserLogin] (
    [UserId] nvarchar(128)  NOT NULL,
    [LoginProvider] nvarchar(max)  NULL,
    [ProviderKey] nvarchar(max)  NULL,
    [IdentityUser_Id] nvarchar(128)  NULL
);
GO

-- Creating table 'UserRole'
CREATE TABLE [dbo].[UserRole] (
    [UserId] nvarchar(128)  NOT NULL,
    [RoleId] nvarchar(max)  NULL,
    [IdentityRole_Id] nvarchar(128)  NULL,
    [IdentityUser_Id] nvarchar(128)  NULL
);
GO

-- Creating table 'MyLogger'
CREATE TABLE [dbo].[MyLogger] (
    [LogId] int IDENTITY(1,1) NOT NULL,
    [Event_Type] int  NULL,
    [TIMESTAMP] nvarchar(50)  NULL,
    [EventCategory] nvarchar(50)  NULL,
    [Event_ID] int  NULL,
    [ComputerName] nvarchar(50)  NULL,
    [Mac_Address] nvarchar(50)  NULL,
    [UserName] nvarchar(50)  NULL,
    [SourceUrl] nvarchar(100)  NULL,
    [Source] nvarchar(50)  NULL,
    [Description] nvarchar(max)  NULL,
    [CollectDate] datetime  NULL
);
GO

-- Creating table 'RoleFunction'
CREATE TABLE [dbo].[RoleFunction] (
    [FunctionID] int  NOT NULL,
    [RoleID] varchar(36)  NOT NULL
);
GO

-- Creating table 'AddressBase'
CREATE TABLE [dbo].[AddressBase] (
    [Code] varchar(50)  NOT NULL,
    [Text] nvarchar(50)  NULL,
    [Type] varchar(50)  NULL
);
GO

-- Creating table 'Address_City'
CREATE TABLE [dbo].[Address_City] (
    [CityCode] char(12)  NOT NULL,
    [CityName] nvarchar(50)  NOT NULL,
    [ProvinceCode] char(12)  NOT NULL
);
GO

-- Creating table 'Address_County'
CREATE TABLE [dbo].[Address_County] (
    [CountyCode] char(12)  NOT NULL,
    [CountyNaem] nvarchar(50)  NOT NULL,
    [CityCode] char(12)  NOT NULL
);
GO

-- Creating table 'Address_Province'
CREATE TABLE [dbo].[Address_Province] (
    [ProvinceCode] char(12)  NOT NULL,
    [ProvinceName] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Address_Town'
CREATE TABLE [dbo].[Address_Town] (
    [TownCode] char(12)  NOT NULL,
    [TownName] nvarchar(50)  NOT NULL,
    [CountyCode] char(12)  NOT NULL
);
GO

-- Creating table 'Address_Village'
CREATE TABLE [dbo].[Address_Village] (
    [VillageCode] char(12)  NOT NULL,
    [VillageName] nvarchar(50)  NOT NULL,
    [Town] char(12)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [MigrationId], [ContextKey] in table 'C__MigrationHistory'
ALTER TABLE [dbo].[C__MigrationHistory]
ADD CONSTRAINT [PK_C__MigrationHistory]
    PRIMARY KEY CLUSTERED ([MigrationId], [ContextKey] ASC);
GO

-- Creating primary key on [FunctionID] in table 'Function'
ALTER TABLE [dbo].[Function]
ADD CONSTRAINT [PK_Function]
    PRIMARY KEY CLUSTERED ([FunctionID] ASC);
GO

-- Creating primary key on [GroupID] in table 'FunctionGroup'
ALTER TABLE [dbo].[FunctionGroup]
ADD CONSTRAINT [PK_FunctionGroup]
    PRIMARY KEY CLUSTERED ([GroupID] ASC);
GO

-- Creating primary key on [Id] in table 'MyRoles'
ALTER TABLE [dbo].[MyRoles]
ADD CONSTRAINT [PK_MyRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MyUsers'
ALTER TABLE [dbo].[MyUsers]
ADD CONSTRAINT [PK_MyUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserClaim'
ALTER TABLE [dbo].[UserClaim]
ADD CONSTRAINT [PK_UserClaim]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [UserId] in table 'UserLogin'
ALTER TABLE [dbo].[UserLogin]
ADD CONSTRAINT [PK_UserLogin]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [UserId] in table 'UserRole'
ALTER TABLE [dbo].[UserRole]
ADD CONSTRAINT [PK_UserRole]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [LogId] in table 'MyLogger'
ALTER TABLE [dbo].[MyLogger]
ADD CONSTRAINT [PK_MyLogger]
    PRIMARY KEY CLUSTERED ([LogId] ASC);
GO

-- Creating primary key on [FunctionID], [RoleID] in table 'RoleFunction'
ALTER TABLE [dbo].[RoleFunction]
ADD CONSTRAINT [PK_RoleFunction]
    PRIMARY KEY CLUSTERED ([FunctionID], [RoleID] ASC);
GO

-- Creating primary key on [Code] in table 'AddressBase'
ALTER TABLE [dbo].[AddressBase]
ADD CONSTRAINT [PK_AddressBase]
    PRIMARY KEY CLUSTERED ([Code] ASC);
GO

-- Creating primary key on [CityCode] in table 'Address_City'
ALTER TABLE [dbo].[Address_City]
ADD CONSTRAINT [PK_Address_City]
    PRIMARY KEY CLUSTERED ([CityCode] ASC);
GO

-- Creating primary key on [CountyCode] in table 'Address_County'
ALTER TABLE [dbo].[Address_County]
ADD CONSTRAINT [PK_Address_County]
    PRIMARY KEY CLUSTERED ([CountyCode] ASC);
GO

-- Creating primary key on [ProvinceCode] in table 'Address_Province'
ALTER TABLE [dbo].[Address_Province]
ADD CONSTRAINT [PK_Address_Province]
    PRIMARY KEY CLUSTERED ([ProvinceCode] ASC);
GO

-- Creating primary key on [TownCode] in table 'Address_Town'
ALTER TABLE [dbo].[Address_Town]
ADD CONSTRAINT [PK_Address_Town]
    PRIMARY KEY CLUSTERED ([TownCode] ASC);
GO

-- Creating primary key on [VillageCode] in table 'Address_Village'
ALTER TABLE [dbo].[Address_Village]
ADD CONSTRAINT [PK_Address_Village]
    PRIMARY KEY CLUSTERED ([VillageCode] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [IdentityRole_Id] in table 'UserRole'
ALTER TABLE [dbo].[UserRole]
ADD CONSTRAINT [FK_dbo_UserRole_dbo_MyRoles_IdentityRole_Id]
    FOREIGN KEY ([IdentityRole_Id])
    REFERENCES [dbo].[MyRoles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_UserRole_dbo_MyRoles_IdentityRole_Id'
CREATE INDEX [IX_FK_dbo_UserRole_dbo_MyRoles_IdentityRole_Id]
ON [dbo].[UserRole]
    ([IdentityRole_Id]);
GO

-- Creating foreign key on [IdentityUser_Id] in table 'UserClaim'
ALTER TABLE [dbo].[UserClaim]
ADD CONSTRAINT [FK_dbo_UserClaim_dbo_MyUsers_IdentityUser_Id]
    FOREIGN KEY ([IdentityUser_Id])
    REFERENCES [dbo].[MyUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_UserClaim_dbo_MyUsers_IdentityUser_Id'
CREATE INDEX [IX_FK_dbo_UserClaim_dbo_MyUsers_IdentityUser_Id]
ON [dbo].[UserClaim]
    ([IdentityUser_Id]);
GO

-- Creating foreign key on [IdentityUser_Id] in table 'UserLogin'
ALTER TABLE [dbo].[UserLogin]
ADD CONSTRAINT [FK_dbo_UserLogin_dbo_MyUsers_IdentityUser_Id]
    FOREIGN KEY ([IdentityUser_Id])
    REFERENCES [dbo].[MyUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_UserLogin_dbo_MyUsers_IdentityUser_Id'
CREATE INDEX [IX_FK_dbo_UserLogin_dbo_MyUsers_IdentityUser_Id]
ON [dbo].[UserLogin]
    ([IdentityUser_Id]);
GO

-- Creating foreign key on [IdentityUser_Id] in table 'UserRole'
ALTER TABLE [dbo].[UserRole]
ADD CONSTRAINT [FK_dbo_UserRole_dbo_MyUsers_IdentityUser_Id]
    FOREIGN KEY ([IdentityUser_Id])
    REFERENCES [dbo].[MyUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_UserRole_dbo_MyUsers_IdentityUser_Id'
CREATE INDEX [IX_FK_dbo_UserRole_dbo_MyUsers_IdentityUser_Id]
ON [dbo].[UserRole]
    ([IdentityUser_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------