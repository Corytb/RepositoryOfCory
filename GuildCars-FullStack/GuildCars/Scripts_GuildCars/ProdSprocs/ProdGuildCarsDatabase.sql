USE master
GO

IF EXISTS(SELECT * FROM sys.databases WHERE name='GuildCarsProd')
DROP DATABASE GuildCarsProd

GO

CREATE DATABASE GuildCarsProd
GO

USE GuildCarsProd
GO



IF EXISTS(SELECT * FROM sys.tables WHERE name='Purchases')
	DROP TABLE Purchases
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Vehicles')
	DROP TABLE Vehicles
GO


IF EXISTS(SELECT * FROM sys.tables WHERE name='CarModels')
	DROP TABLE CarModels
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='CarMakes')
	DROP TABLE CarMakes
GO


IF EXISTS(SELECT * FROM sys.tables WHERE name='Customers')
	DROP TABLE Customers
GO


IF EXISTS(SELECT * FROM sys.tables WHERE name='States')
	DROP TABLE States
GO


IF EXISTS(SELECT * FROM sys.tables WHERE name='PaymentTypes')
	DROP TABLE PaymentTypes
GO


IF EXISTS(SELECT * FROM sys.tables WHERE name='CarBodies')
	DROP TABLE CarBodies
GO


IF EXISTS(SELECT * FROM sys.tables WHERE name='Colors')
	DROP TABLE Colors
GO


IF EXISTS(SELECT * FROM sys.tables WHERE name='Transmissions')
	DROP TABLE Transmissions
GO


IF EXISTS(SELECT * FROM sys.tables WHERE name='Specials')
	DROP TABLE Specials
GO



CREATE TABLE Specials(
	SpecialId int identity(1,1) not null primary key,
	SpecialTitle nvarchar(50) not null,
	SpecialDescription nvarchar(max) not null,
	SpecialImageFileName varchar(30)
)

CREATE TABLE Transmissions(
	TransmissionTypeId int not null primary key,
	TransmissionType nvarchar(9) not null
)

CREATE TABLE Colors(
	ColorTypeId int not null primary key,
	ColorType nvarchar(20) not null
)


CREATE TABLE CarBodies(
	CarBodyTypeId int not null primary key,
	CarBodyType nvarchar(5) not null
)


CREATE TABLE PaymentTypes(
	PaymentTypeId int not null primary key,
	PayType nvarchar(14) not null
)


CREATE TABLE States(
	StateId char(2) not null primary key,
	StateName nvarchar(15) not null
)


CREATE TABLE Customers(
	CustomerId int identity(1,1) not null primary key,
	CustomerName nvarchar(50) not null,
	CustomerPhone nvarchar(12),
	CustomerEmail nvarchar(50),
	CustomerStreet1 nvarchar(50) not null,
	CustomerStreet2 nvarchar(50),
	CustomerCity nvarchar(30) not null,
	StateId char(2) not null foreign key references States(StateId),
	CustomerZip nvarchar(5) not null
)


CREATE TABLE CarMakes(
	CarMakeId int identity(1,1)  not null primary key,
	CarMakeName nvarchar(20) not null,
	UserId nvarchar(128) not null,
	CarMakeAddedDate datetime2 not null default(getDate())
)

CREATE TABLE CarModels(
	CarModelId int identity(1,1) not null primary key,
	CarMakeId int not null foreign key references CarMakes(CarMakeId),
	CarModelName nvarchar(20) not null,
	UserId nvarchar(128) not null,
	CarModelAddedDate datetime2 not null default(getDate())
)


CREATE TABLE Vehicles(
	VehicleId int identity(1,1) not null primary key,
	VinNumber nvarchar(17) not null,
	CarMakeId int not null foreign key references CarMakes(CarMakeId),
	CarModelId int not null foreign key references CarModels(CarModelId),
	BodyColorTypeId int not null foreign key references Colors(ColorTypeId),
	InteriorColorTypeId int not null foreign key references Colors(ColorTypeId),
	TransmissionTypeId int not null foreign key references Transmissions(TransmissionTypeId),
	CarBodyTypeId int not null foreign key references CarBodies(CarBodyTypeId),
	ModelYear int not null,
	VehicleMileage int not null,
	MSRP decimal(10,2) not null,
	SalePrice decimal(10,2) not null,
	VehicleDescription nvarchar(max) not null,
	VehicleImageFileName varchar(30) not null,
	IsNewCar bit not null,
	IsFeaturedCar bit null DEFAULT 0,
	IsPurchasedCar bit null DEFAULT 0
)



CREATE TABLE Purchases(
	PurchaseId int identity(1,1) not null primary key,
	PaymentTypeId int not null foreign key references PaymentTypes(PaymentTypeId),
	UserId nvarchar(128) not null,
	VehicleId int not null foreign key references Vehicles(VehicleId),
	CustomerId int not null foreign key references Customers(CustomerId),
	PurchasePrice decimal(10,2) not null,
	PurchaseDate datetime2 not null
)
