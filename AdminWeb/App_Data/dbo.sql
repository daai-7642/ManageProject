/*
Navicat SQL Server Data Transfer

Source Server         : local
Source Server Version : 120000
Source Host           : (LocalDb)\MSSQLLocalDB:1433
Source Database       : SuperDB
Source Schema         : dbo

Target Server Type    : SQL Server
Target Server Version : 120000
File Encoding         : 65001

Date: 2017-07-24 18:15:31
*/


-- ----------------------------
-- Table structure for __MigrationHistory
-- ----------------------------
DROP TABLE [dbo].[__MigrationHistory]
GO
CREATE TABLE [dbo].[__MigrationHistory] (
[MigrationId] nvarchar(150) NOT NULL ,
[ContextKey] nvarchar(300) NOT NULL ,
[Model] varbinary(MAX) NOT NULL ,
[ProductVersion] nvarchar(32) NOT NULL 
)


GO

-- ----------------------------
-- Records of __MigrationHistory
-- ----------------------------
INSERT INTO [dbo].[__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'201707240335587_InitialCreate', N'AdminWeb.Models.ApplicationDbContext', 0x1F8B0800000000000400DD5C5B6FE4B6157E2FD0FF20E829299C912F4DB0356612787D698DAE2FD8F1A67D5B7024CE5858895224CAB111E497F5A13FA97FA1A4444924454AD46566678200C18E487EE7F0F0230F79C8E3FFFDE7BFF39F5EC3C07A8149EA4768619FCC8E6D0B2237F27CB459D8195E7FF7CEFEE9C73FFF697EED85AFD6CF65BD335A8FB444E9C27EC6383E779CD47D86214867A1EF26511AADF1CC8D42077891737A7CFC37E7E4C48104C226589635FF9821EC8730FF417E5E46C88531CE407017793048D97752B2CC51AD7B10C234062E5CD8175EE8A37FC1D5ACA86A5B17810F881A4B18AC6D0B201461808992E79F52B8C4498436CB987C00C1D35B0C49BD350852C8943FAFAB9BF6E3F894F6C3A91B96506E96E228EC097872C60CE3C8CD0799D7AE0C474C774D4C8CDF68AF73F32DEC5B0FE69F3E460131802CF0FC324868E5857D5789B848E37B886765C35901799310B85FA3E4CB8C473CB28CDB1D55443A9D1DD3FF8EACCB2CC059021708663801C191F598AD02DFFD277C7B8ABE40B4383B59ADCFDE7DFF03F0CE7EF82B3CFB9EEF29E92BA9277C209F1E93288609D10DAEABFEDB9623B673E4865533AE4D6115C22532276CEB0EBC7E8068839FC96C397D675B37FE2BF4CA2F8C5C9F904FA6106984938CFCBCCF8200AC0258953BAD32E9FF5BA4927F1A499584DC83177F938FB4248ECC93844CA38F30C84BD3673F2E669330BC9F59B59B240AE96F914E45E9E76594252ED53DD2567902C9066251BBB95373D588C1146A7A1697A8FBCF64AAE9103697ED76CD686AD556B943386DCC9A8B38260390D3831AA08D34927399494D8FACB2423DF027A6038F88CD6CEB3D482133033FC50AC51C63F65F06C00FA7A77F0EBBFFFC1FB792DF227C76AAE03067C6258E12F87788600230F41E01C63041F50874F1BD739E0D5CC32531F9685119BB91F43308B2A9450D5AFA3F441B1F4D4FFE1C76FFC97F688B7F6E56F2EDC5F7E81AB765A696828835F681AAD3B374FF097A28FBECEB10F8C1D609994B2107DCB59F84B0EAE5FB886C8900EAADF323485332B4DE3F40FABC75D597D0CD1242A8250661BCFD99FB1C21789F85AB5DAC12B5ACC986E6E9D7E806B864E3708D68ABD1781F22F74B94E16BE45D914DC827EC9680F4E7931F9A034CA2CE85EBC234BD216486DE659421DCB59DEADE22EDF8A09B6F62DA4FBA54ABCF65BDE651972BD69E75F93AAAC36E9B86B9AB34D0B0ACA7D1B0286ED790D5E9AB214532509055D3E89797B6AB5754E9132CB848D3C8F573AD14F12E16BE107B49A696D51DCB681ED68AD0C31D71A53E3D1E922F0BFB78363B6998B15540D939CD695025E42FB229B84E9BD982277897AE4AB6EB8EAE83ACA19A2B1A09ECC0BB157BB0E964A4AD3CB7A6B78734333512D819682BF62866AF91B2D2549EDE1AE242B085B952AC26641780814FCEFBCD98D1D58A16C257ACD8CB13E16C3B9F326F24778A822F211697CF7A0553AE2C0DCB882072CFDB006BEBF4002D1D602B2A9B903D604BAFD50ACB78DD03B60BB101C611A189C8C7BAB98AFA88B87C1E32722A557F2A4A38BD70CAF9C0E12858211FC2C48EF7308AB02DD25B45EB5C8CDD8BA63F66F65179130D60D997692D5412BCC3422A7763EC70C65B48F22F1AC0B22FD35A8811B3C3400AFF63EA81C69B477438134DB072D35AB999FA86DB29AEB8CBAB704773173EBF03714C4E48DCDD38FB622D8B8BF1CBEF96FD2F8DC302C37153C5DD71A56D25899C6EC1064AA54434D1F4C64F524C8EA66005E819EDD20B1BD5944E55B3D0972279BFD91CC172D92F6BD37F33072EDDE4081E5602AA6D7943BA17928A794FA162F0D5CDF3870A20008922AA75190559887491B1B6D6C5F1986F5F7C6922CC1D497FD9524EC354127365BB1B8D4A73464C3142D59665F828E92174B62E83E3BCB5D581F63694F29E934729BFEDE5A88D1B31F96275DC806D615A9135C6F3F3534E7E77A5B80AE661AEFCD44D7CD24540B4DCC6807598F736A5FF7E587FD33633BEDD3F2B2B5BB3E03A0FC03EF5C4E0E2B30D30AECC1C550CA1F398628939A21427E721A5A21E5AF2D1704149BE60109EC6A2EA1AE6129AF16F1EBD596A8EAC8884F3D08AE201D80A9DE532735445B09C0756149B63D79173D9596D6B7B30C291EFDF6AD5F00962AC60A70E61020FAE3D6E0F1BAF22AA336ED03418DBF133D36CDCB897343C10F7B927167B2BD30063DFF7924ADAB8C4302A1591BC7154D2606C970CD28315D12308453DDC2FFF2C45F0BA7CC1EE6921462CD4DCE043A26607ECB276DFF3198DC93456075D10B5692BC3E585035411839AACD26290822C7864A8601FD2B6EA567BB6DB94DEC657F7E53DBA2FC7B006934608199B2D1855F5DECEA565581451E691C4C91127248E226C3D58C36D3246D3EF691953869D0D195356EFED43BAC6438CBAEF2363C430FE5EAF353B610E0BC91B1287D59ED041352F23F69135C2EDC61F93348DBB16B94AB5A7AAEE5CA4BB9539BBE7E84E466C5C7C14556CABDCD62DECE55B8A6138A31566CB5F82CBC087F4BC5F56B803C85FC31417CF6FEDD3E3935329A5717FD20B9D34F5821E3986E2C0EDE0ED307A0189FB0C92E6EBE111297825E8372178FD96479AE889F850534D93073099C9C41CAF1E46530DA8B44795556DBCC3B9451E7C5DD8BFE500E7D6EDBF3F4B1847D6434266DBB9756CFDDED6D53EEA552BD408F518C670F58665251CEECC141EED8F6299FA61FECAC7933CCA1FA59AF2E1FD2844C5E3FAA9F02631A1EEF1FC102CEDC3798FFCC4F9C3F97E9D553FA41FA29AF611BD8FFA83C94FE8470DA814EB1E356B07AD498A83FD2E16A6DCF09DE99E034666225FD8C8F11C8F26E4714EE2A80FD7132A820307B9FB5226598E5BE19B89947F7CAE4C96443275DE48FD8E7BF81378024CF80B13DA08D09D4F8A1372406E5C293C263E72FD1804AA7E356306262B32B578052B975CC118222A43D76733999D61FD4A8E3465BB8CB283349B6DA44EB007FA23F269C61246F30060AB84D15D60EB19D31267DB35637A24226D83312CF76244C6D1213246774F7D108C314FD5DA0661BEB647FA1A74E9ED9176C91629F25DEEFA1BCFF3E5C1651720AA206E57B25A11F12687EB5544A8506CB8EEDED409123A41355DB4C2EA2A2A8175690F899D7963EAAE29F349BAF3E9CCD2E9749D1B986D67966CA713AACEC5FB1AE973CA441CD580772C556D577D07902EA7ED80B119042A6A1E571C4056DC784308D343F36660EF93DFC69B61CA69D123C9AD79C14A9C1EF7376189074EFD4D0D41AF8E1174057757D5B945EBA874BF924665152916710731F0882FBC48B0BF062E26C5342E9BFF0112F62AF83A5C41EF163D6438CE30E9320C5781F0428F7AEF36F979269FA8F3FC21CEFF68C6145D206AFA349EFD80DE677EE0557ADF28C2B11A08BA2D60414F3A9698063F376F15D27D840C8198F9AADDCC130CE38080A50F68095EE010DD08FD3EC00D70DFEA98960EA47B2044B3CFAF7CB0494098328CBA3DF94938EC85AF3FFE1FFE79872D1A590000, N'6.1.3-40302')
GO
GO

-- ----------------------------
-- Table structure for C__MigrationHistory
-- ----------------------------
DROP TABLE [dbo].[C__MigrationHistory]
GO
CREATE TABLE [dbo].[C__MigrationHistory] (
[MigrationId] nvarchar(150) NOT NULL ,
[ContextKey] nvarchar(300) NOT NULL ,
[Model] varbinary(MAX) NOT NULL ,
[ProductVersion] nvarchar(32) NOT NULL 
)


GO

-- ----------------------------
-- Records of C__MigrationHistory
-- ----------------------------

-- ----------------------------
-- Table structure for Function
-- ----------------------------
DROP TABLE [dbo].[Function]
GO
CREATE TABLE [dbo].[Function] (
[FunctionID] int NOT NULL IDENTITY(1,1) ,
[GroupID] int NOT NULL ,
[FunctionName] nvarchar(50) NULL ,
[PageURL] nvarchar(200) NULL ,
[OrderNo] int NOT NULL ,
[Description] nvarchar(200) NULL ,
[Status] int NOT NULL 
)


GO

-- ----------------------------
-- Records of Function
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Function] ON
GO
INSERT INTO [dbo].[Function] ([FunctionID], [GroupID], [FunctionName], [PageURL], [OrderNo], [Description], [Status]) VALUES (N'1', N'1', N'角色管理', N'', N'1', null, N'1')
GO
GO
SET IDENTITY_INSERT [dbo].[Function] OFF
GO

-- ----------------------------
-- Table structure for FunctionGroup
-- ----------------------------
DROP TABLE [dbo].[FunctionGroup]
GO
CREATE TABLE [dbo].[FunctionGroup] (
[GroupID] int NOT NULL IDENTITY(1,1) ,
[GroupName] nvarchar(50) NULL ,
[ParentID] int NOT NULL ,
[GroupCode] nvarchar(50) NULL ,
[OrderNo] int NOT NULL ,
[State] smallint NOT NULL ,
[Description] nvarchar(100) NULL 
)


GO

-- ----------------------------
-- Records of FunctionGroup
-- ----------------------------
SET IDENTITY_INSERT [dbo].[FunctionGroup] ON
GO
INSERT INTO [dbo].[FunctionGroup] ([GroupID], [GroupName], [ParentID], [GroupCode], [OrderNo], [State], [Description]) VALUES (N'1', N'系统管理', N'0', N'0', N'1', N'1', N'系统管理')
GO
GO
SET IDENTITY_INSERT [dbo].[FunctionGroup] OFF
GO

-- ----------------------------
-- Table structure for MyRoles
-- ----------------------------
DROP TABLE [dbo].[MyRoles]
GO
CREATE TABLE [dbo].[MyRoles] (
[Id] nvarchar(128) NOT NULL ,
[Name] nvarchar(50) NULL ,
[Description] nvarchar(200) NULL ,
[Status] int NOT NULL 
)


GO

-- ----------------------------
-- Records of MyRoles
-- ----------------------------
INSERT INTO [dbo].[MyRoles] ([Id], [Name], [Description], [Status]) VALUES (N'1', N'超级管理员', N'管理员', N'1')
GO
GO
INSERT INTO [dbo].[MyRoles] ([Id], [Name], [Description], [Status]) VALUES (N'2', N'系统管理员', N'系统管理员', N'1')
GO
GO
INSERT INTO [dbo].[MyRoles] ([Id], [Name], [Description], [Status]) VALUES (N'2017072405362662', N'普通管理员', N'普通', N'1')
GO
GO
INSERT INTO [dbo].[MyRoles] ([Id], [Name], [Description], [Status]) VALUES (N'2017072405400633', N'后台管理员', N'后台', N'1')
GO
GO
INSERT INTO [dbo].[MyRoles] ([Id], [Name], [Description], [Status]) VALUES (N'2017072405410698', N'测试', N'普通', N'1')
GO
GO
INSERT INTO [dbo].[MyRoles] ([Id], [Name], [Description], [Status]) VALUES (N'2017072405424252', N'哈哈', N'哈哈', N'1')
GO
GO
INSERT INTO [dbo].[MyRoles] ([Id], [Name], [Description], [Status]) VALUES (N'2017072405511330', N'12', null, N'1')
GO
GO
INSERT INTO [dbo].[MyRoles] ([Id], [Name], [Description], [Status]) VALUES (N'2017072405535678', N'222', null, N'1')
GO
GO
INSERT INTO [dbo].[MyRoles] ([Id], [Name], [Description], [Status]) VALUES (N'2017072405541537', N'232323', null, N'1')
GO
GO
INSERT INTO [dbo].[MyRoles] ([Id], [Name], [Description], [Status]) VALUES (N'2017072405542639', N'211212', null, N'1')
GO
GO
INSERT INTO [dbo].[MyRoles] ([Id], [Name], [Description], [Status]) VALUES (N'2017072405543638', N'211212', null, N'1')
GO
GO
INSERT INTO [dbo].[MyRoles] ([Id], [Name], [Description], [Status]) VALUES (N'2017072405543756', N'211212', null, N'1')
GO
GO
INSERT INTO [dbo].[MyRoles] ([Id], [Name], [Description], [Status]) VALUES (N'2017072406123991', N'1221', null, N'1')
GO
GO
INSERT INTO [dbo].[MyRoles] ([Id], [Name], [Description], [Status]) VALUES (N'2017072406125663', N'2323', null, N'1')
GO
GO

-- ----------------------------
-- Table structure for MyUsers
-- ----------------------------
DROP TABLE [dbo].[MyUsers]
GO
CREATE TABLE [dbo].[MyUsers] (
[Id] nvarchar(128) NOT NULL ,
[Email] nvarchar(MAX) NULL ,
[EmailConfirmed] bit NOT NULL ,
[PasswordHash] nvarchar(MAX) NULL ,
[SecurityStamp] nvarchar(MAX) NULL ,
[PhoneNumber] nvarchar(MAX) NULL ,
[PhoneNumberConfirmed] bit NOT NULL ,
[TwoFactorEnabled] bit NOT NULL ,
[LockoutEndDateUtc] datetime NULL ,
[LockoutEnabled] bit NOT NULL ,
[AccessFailedCount] int NOT NULL ,
[UserName] nvarchar(MAX) NULL ,
[Discriminator] nvarchar(128) NOT NULL 
)


GO

-- ----------------------------
-- Records of MyUsers
-- ----------------------------
INSERT INTO [dbo].[MyUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Discriminator]) VALUES (N'709ae7fb-28d4-41d8-88ab-be0f4f924f79', N'764297968@qq.com', N'0', N'AAH5k/RVdbs0s605l3T/c9+CpmZtTFOha/rW1DxDo3La51SDFwkT8541v2HF84fM5g==', N'1f043097-e9ff-4719-87a4-de21457e77a7', null, N'0', N'0', null, N'1', N'0', N'764297968@qq.com', N'ApplicationUser')
GO
GO

-- ----------------------------
-- Table structure for UserClaim
-- ----------------------------
DROP TABLE [dbo].[UserClaim]
GO
CREATE TABLE [dbo].[UserClaim] (
[Id] int NOT NULL IDENTITY(1,1) ,
[UserId] nvarchar(MAX) NULL ,
[ClaimType] nvarchar(MAX) NULL ,
[ClaimValue] nvarchar(MAX) NULL ,
[IdentityUser_Id] nvarchar(128) NULL 
)


GO

-- ----------------------------
-- Records of UserClaim
-- ----------------------------
SET IDENTITY_INSERT [dbo].[UserClaim] ON
GO
SET IDENTITY_INSERT [dbo].[UserClaim] OFF
GO

-- ----------------------------
-- Table structure for UserLogin
-- ----------------------------
DROP TABLE [dbo].[UserLogin]
GO
CREATE TABLE [dbo].[UserLogin] (
[UserId] nvarchar(128) NOT NULL ,
[LoginProvider] nvarchar(MAX) NULL ,
[ProviderKey] nvarchar(MAX) NULL ,
[IdentityUser_Id] nvarchar(128) NULL 
)


GO

-- ----------------------------
-- Records of UserLogin
-- ----------------------------

-- ----------------------------
-- Table structure for UserRole
-- ----------------------------
DROP TABLE [dbo].[UserRole]
GO
CREATE TABLE [dbo].[UserRole] (
[UserId] nvarchar(128) NOT NULL ,
[RoleId] nvarchar(MAX) NULL ,
[IdentityRole_Id] nvarchar(128) NULL ,
[IdentityUser_Id] nvarchar(128) NULL 
)


GO

-- ----------------------------
-- Records of UserRole
-- ----------------------------

-- ----------------------------
-- Indexes structure for table __MigrationHistory
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table __MigrationHistory
-- ----------------------------
ALTER TABLE [dbo].[__MigrationHistory] ADD PRIMARY KEY ([MigrationId], [ContextKey])
GO

-- ----------------------------
-- Indexes structure for table C__MigrationHistory
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table C__MigrationHistory
-- ----------------------------
ALTER TABLE [dbo].[C__MigrationHistory] ADD PRIMARY KEY ([MigrationId], [ContextKey])
GO

-- ----------------------------
-- Indexes structure for table Function
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Function
-- ----------------------------
ALTER TABLE [dbo].[Function] ADD PRIMARY KEY ([FunctionID])
GO

-- ----------------------------
-- Indexes structure for table FunctionGroup
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table FunctionGroup
-- ----------------------------
ALTER TABLE [dbo].[FunctionGroup] ADD PRIMARY KEY ([GroupID])
GO

-- ----------------------------
-- Indexes structure for table MyRoles
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table MyRoles
-- ----------------------------
ALTER TABLE [dbo].[MyRoles] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table MyUsers
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table MyUsers
-- ----------------------------
ALTER TABLE [dbo].[MyUsers] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table UserClaim
-- ----------------------------
CREATE INDEX [IX_FK_dbo_UserClaim_dbo_MyUsers_IdentityUser_Id] ON [dbo].[UserClaim]
([IdentityUser_Id] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table UserClaim
-- ----------------------------
ALTER TABLE [dbo].[UserClaim] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table UserLogin
-- ----------------------------
CREATE INDEX [IX_FK_dbo_UserLogin_dbo_MyUsers_IdentityUser_Id] ON [dbo].[UserLogin]
([IdentityUser_Id] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table UserLogin
-- ----------------------------
ALTER TABLE [dbo].[UserLogin] ADD PRIMARY KEY ([UserId])
GO

-- ----------------------------
-- Indexes structure for table UserRole
-- ----------------------------
CREATE INDEX [IX_FK_dbo_UserRole_dbo_MyRoles_IdentityRole_Id] ON [dbo].[UserRole]
([IdentityRole_Id] ASC) 
GO
CREATE INDEX [IX_FK_dbo_UserRole_dbo_MyUsers_IdentityUser_Id] ON [dbo].[UserRole]
([IdentityUser_Id] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table UserRole
-- ----------------------------
ALTER TABLE [dbo].[UserRole] ADD PRIMARY KEY ([UserId])
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[UserClaim]
-- ----------------------------
ALTER TABLE [dbo].[UserClaim] ADD FOREIGN KEY ([IdentityUser_Id]) REFERENCES [dbo].[MyUsers] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[UserLogin]
-- ----------------------------
ALTER TABLE [dbo].[UserLogin] ADD FOREIGN KEY ([IdentityUser_Id]) REFERENCES [dbo].[MyUsers] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[UserRole]
-- ----------------------------
ALTER TABLE [dbo].[UserRole] ADD FOREIGN KEY ([IdentityRole_Id]) REFERENCES [dbo].[MyRoles] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[UserRole] ADD FOREIGN KEY ([IdentityUser_Id]) REFERENCES [dbo].[MyUsers] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
