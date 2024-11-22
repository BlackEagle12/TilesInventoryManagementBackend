USE [TilesInventoryManagement]

DROP TABLE IF EXISTS [dbo].[users]
CREATE TABLE users
(
	[id] INT PRIMARY KEY IDENTITY(1,1),
	[email] VARCHAR(100) UNIQUE NOT NULL,
	[username] VARCHAR(100) UNIQUE NOT NULL,
	[password] VARCHAR(50) NOT NULL,
	[first_name] VARCHAR(100) NOT NULL,
	[last_name] VARCHAR(100) NOT NULL,
	[phone_no] VARCHAR(20) UNIQUE NOT NULL,
	[address1] VARCHAR(1000) NOT NULL,
	[address2] VARCHAR(1000) NOT NULL,
	[country_id] INT NOT NULL,
	[state_id] INT NOT NULL,
	[city] VARCHAR(500) NOT NULL,
	[pincode] VARCHAR(20) NOT NULL,
	[summary] VARCHAR(2000),
	[birth_date] DATETIME2,
	[aniversary_date] DATETIME2,
	[role_id] INT NOT NULL,
	[category_id] INT NOT NULL,
	[added_on] DATETIME2 NOT NULL DEFAULT GETDATE(),
	[last_updated_on] DATETIME2 NOT NULL DEFAULT GETDATE(),
)

DROP TABLE IF EXISTS [dbo].[categories]
CREATE TABLE categories
(
	[id] INT PRIMARY KEY IDENTITY(0,1),
	[category_name] VARCHAR(100) UNIQUE NOT NULL,
	[description] VARCHAR(1000),
	[added_on] DATETIME2 NOT NULL DEFAULT GETDATE(),
	[last_updated_on] DATETIME2 NOT NULL DEFAULT GETDATE()
)

DROP TABLE IF EXISTS [dbo].[roles]
CREATE TABLE roles
(
	[id] INT PRIMARY KEY IDENTITY(1,1),
	[role_name] VARCHAR(100) UNIQUE NOT NULL,
	[description] VARCHAR(1000),
	[added_on] DATETIME2 NOT NULL DEFAULT GETDATE(),
	[last_updated_on] DATETIME2 NOT NULL DEFAULT GETDATE()
)

DROP TABLE IF EXISTS [dbo].[permissions]
CREATE TABLE [permissions]
(
	[id] INT PRIMARY KEY IDENTITY(1,1),
	[permission_name] VARCHAR(100) UNIQUE NOT NULL,
	[description] VARCHAR(1000),
	[added_on] DATETIME2 NOT NULL DEFAULT GETDATE(),
	[last_updated_on] DATETIME2 NOT NULL DEFAULT GETDATE()
)

DROP TABLE IF EXISTS [dbo].[role_permission]
CREATE TABLE [dbo].[role_permission]
(
	[id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[role_id] [int] NOT NULL,
	[permission_id] [int] NOT NULL,
	[description] [varchar](1000),
	[added_on] [datetime2](7) NOT NULL DEFAULT getdate(),
	[last_updated_on] [datetime2](7) NOT NULL DEFAULT getdate(),
)

DROP TABLE IF EXISTS [dbo].[countries]
CREATE TABLE countries
(
	[id] INT PRIMARY KEY IDENTITY(1,1),
	[country_name] VARCHAR(100) UNIQUE NOT NULL,
	[country_code] VARCHAR(10) UNIQUE NOT NULL,
	[description] VARCHAR(1000),
	[added_on] DATETIME2 NOT NULL DEFAULT GETDATE(),
	[last_updated_on] DATETIME2 NOT NULL DEFAULT GETDATE()
)

DROP TABLE IF EXISTS [dbo].[states]
CREATE TABLE states
(
	[id] INT PRIMARY KEY IDENTITY(1,1),
	[state_name] VARCHAR(100) NOT NULL,
	[country_id] INT NOT NULL,
	[description] VARCHAR(1000),
	[added_on] DATETIME2 NOT NULL DEFAULT GETDATE(),
	[last_updated_on] DATETIME2 NOT NULL DEFAULT GETDATE()
)

ALTER TABLE states ADD CONSTRAINT uq_states UNIQUE([state_name], [country_id]);


TRUNCATE TABLE [dbo].[categories]
INSERT INTO [dbo].[categories]
	([category_name], [description])
VALUES
	('ARCHITECH', 'Added for demo'),
	('BUILDER', 'Demo data'),
	('CONSUMER', 'Demo data'),
	('DEALER', 'Demo data'),
	('INTERIOR DESIGNER', 'Demo data'),
	('PLUMBING CONSULTANT', 'Demo data'),
	('RETAILER', 'Demo data'),
	('OTHER', 'Demo data')

TRUNCATE TABLE [dbo].roles
INSERT INTO [dbo].roles
	([role_name], [description])
VALUES
	('CLIENT', 'End user'),
	('MEMBER', 'Level 1 internal user'),
	('ADMIN', 'Level 2 internal user'),
	('OWNER', 'Owner of the system') 

TRUNCATE TABLE [dbo].[permissions]
INSERT INTO [dbo].[permissions]
	([permission_name], [description])
VALUES
	('VIEW_INVENTORY_STOCK', 'User can view stock of the inventories'), -- 4,3,2,1
	('VIEW_INVENTORY_LIST', 'User can view inventory list'), -- 4,3,2,1
	('UPDATE_INVENTORY_STOCK', 'User can update stock of the inventories'), -- 4,3,2
	('UPDATE_INVENTORY_LIST', 'User can view inventory list'), -- 4,3
	('VIEW_USER_LIST', 'Owner can view user list'), -- 4
	('UPDATE_USER_LIST', 'Owner can view user list') -- 4

TRUNCATE TABLE [dbo].[role_permission]
INSERT INTO [dbo].[role_permission]
	([role_id], [permission_id], [description])
VALUES
	(1,1, 'Added for demo'),
	(1,2, 'Added for demo'),
	(2,1, 'Added for demo'),
	(2,2, 'Added for demo'),
	(2,3, 'Added for demo'),
	(3,1, 'Added for demo'),
	(3,2, 'Added for demo'),
	(3,3, 'Added for demo'),
	(3,4, 'Added for demo'),
	(4,1, 'Added for demo'),
	(4,2, 'Added for demo'),
	(4,3, 'Added for demo'),
	(4,4, 'Added for demo'),
	(4,5, 'Added for demo'),
	(4,6, 'Added for demo')

TRUNCATE TABLE [dbo].[countries]
INSERT INTO [dbo].[countries]
	([country_name], [country_code], [description])
VALUES
	('INDIA', 'IND', 'Demo data'),
	('AUSTRALIA', 'AUS', 'Demo data'),
	('PAKISTAN', 'PAK', 'Demo data')

TRUNCATE TABLE [dbo].[states]
INSERT INTO [dbo].[states]
	([state_name], [country_id], [description])
VALUES
	('GUJARAT', 1, 'Demo data'),
	('RAJASTHAN', 1, 'Demo data'),
	('MADHYA PRADESH', 1, 'Demo data'),
	('UTTAR PRADESH', 1, 'Demo data'),
	('GOA', 1, 'Demo data'),
	('KERAL', 1, 'Demo data'),
	('MAHARASTRA', 1, 'Demo data'),
	('PUNJAB', 1, 'Demo data'),
	('WESTAN BANGAL', 1, 'Demo data'),
	('NEW SOUTH WALES', 2, 'Demo data'),
	('VICTORIA', 2, 'Demo data'),
	('SOUTH AUSTRALIA', 2, 'Demo data'),
	('WESTERN AUSTRALIA', 2, 'Demo data'),
	('AUSTRALIAN CAPITAL TERRITORY', 2, 'Demo data'),
	('TASMANIA', 2, 'Demo data'),
	('QUEENSLAND', 2, 'Demo data'),
	('PUNJAB', 3, 'Demo data'),
	('SINDH', 3, 'Demo data'),
	('KHYBER PAKHTUNKHWA', 3, 'Demo data'),
	('BALOCHISTAN', 3, 'Demo data')
