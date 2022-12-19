USE GuildCarsProd
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
	WHERE ROUTINE_NAME='StatesSelectAll')
	DROP PROCEDURE StatesSelectAll
GO

CREATE PROCEDURE StatesSelectAll AS
BEGIN
	SELECT StateId, StateName
	FROM States
END
GO



IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
	WHERE ROUTINE_NAME='PaymentTypesSelectAll')
	DROP PROCEDURE PaymentTypesSelectAll
GO

CREATE PROCEDURE PaymentTypesSelectAll AS
BEGIN
	SELECT PaymentTypeId, PayType
	FROM PaymentTypes
	ORDER BY PaymentTypeId
END
GO



IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
	WHERE ROUTINE_NAME='CarBodiesSelectAll')
	DROP PROCEDURE CarBodiesSelectAll
GO

CREATE PROCEDURE CarBodiesSelectAll AS
BEGIN
	SELECT CarBodyTypeId, CarBodyType
	FROM CarBodies
	ORDER BY CarBodyTypeId
END
GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
	WHERE ROUTINE_NAME='ColorsSelectAll')
	DROP PROCEDURE ColorsSelectAll
GO

CREATE PROCEDURE ColorsSelectAll AS
BEGIN
	SELECT ColorTypeId, ColorType
	FROM Colors
	ORDER BY ColorTypeId
END
GO



IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
	WHERE ROUTINE_NAME='SpecialsSelectAll')
	DROP PROCEDURE SpecialsSelectAll
GO

CREATE PROCEDURE SpecialsSelectAll AS
BEGIN
	SELECT SpecialId, SpecialTitle, SpecialDescription, SpecialImageFileName
	FROM Specials
	ORDER BY SpecialId DESC
END
GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
	WHERE ROUTINE_NAME='GetSpecialImageById')
	DROP PROCEDURE GetSpecialImageById
GO
CREATE PROCEDURE GetSpecialImageById (
	@SpecialId int
)AS
BEGIN
	SELECT SpecialId, SpecialImageFileName
	FROM Specials
	WHERE SpecialId = @SpecialId
END
GO



IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
	WHERE ROUTINE_NAME='SpecialsInsert')
	DROP PROCEDURE SpecialsInsert
GO
CREATE PROCEDURE SpecialsInsert (
	@SpecialId int output,
	@SpecialTitle nvarchar(50),
	@SpecialDescription nvarchar(MAX),
	@SpecialImageFileName varchar(30)
)
AS
BEGIN
	INSERT INTO Specials (SpecialTitle, SpecialDescription, SpecialImageFileName)
	VALUES(@SpecialTitle, @SpecialDescription, @SpecialImageFileName);

	SET @SpecialId = SCOPE_IDENTITY();
END
GO




IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'SpecialsDelete')
		DROP PROCEDURE SpecialsDelete
GO 
CREATE PROCEDURE SpecialsDelete (
	@SpecialId int
) AS
BEGIN
	DELETE FROM Specials
	WHERE SpecialId = @SpecialId
END
GO





IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
	WHERE ROUTINE_NAME='TransmissionsSelectAll')
	DROP PROCEDURE TransmissionsSelectAll
GO

CREATE PROCEDURE TransmissionsSelectAll AS
BEGIN
	SELECT TransmissionTypeId, TransmissionType
	FROM Transmissions
	ORDER BY TransmissionTypeId
END
GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
	WHERE ROUTINE_NAME='CarMakesSelectAll')
	DROP PROCEDURE CarMakesSelectAll
GO

CREATE PROCEDURE CarMakesSelectAll AS
BEGIN
	SELECT CarMakes.CarMakeId, CarMakes.CarMakeName, CarMakes.UserId, CarMakes.CarMakeAddedDate, AspNetUsers.Email
	FROM CarMakes
		LEFT JOIN AspNetUsers ON CarMakes.UserId = AspNetUsers.Id
	ORDER BY CarMakeName ASC
END
GO



IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
	WHERE ROUTINE_NAME='CarMakesInsert')
	DROP PROCEDURE CarMakesInsert
GO
CREATE PROCEDURE CarMakesInsert (
	@CarMakeId int output,
	@CarMakeName nvarchar(20),
	@UserId nvarchar(128)
)
AS
BEGIN
	INSERT INTO CarMakes (CarMakeName, UserId)
	VALUES(@CarMakeName, @UserId);

	SET @CarMakeId = SCOPE_IDENTITY();
END
GO




IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
	WHERE ROUTINE_NAME='CarModelsSelectAll')
	DROP PROCEDURE CarModelsSelectAll
GO

CREATE PROCEDURE CarModelsSelectAll
AS
BEGIN
	SELECT CarModels.CarMakeId, CarMakes.CarMakeName, CarModels.CarModelId, CarModels.CarModelName, CarModels.CarModelAddedDate, CarModels.UserId, AspNetUsers.Email
	FROM CarModels
		LEFT JOIN CarMakes ON CarModels.CarMakeId = CarMakes.CarMakeId
		LEFT JOIN AspNetUsers ON CarModels.UserId = AspNetUsers.Id
	WHERE CarModels.CarMakeId = CarMakes.CarMakeId
	ORDER BY CarMakeName ASC
END
GO



IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
	WHERE ROUTINE_NAME='CarModelsSelectByMake')
	DROP PROCEDURE CarModelsSelectByMake
GO

CREATE PROCEDURE CarModelsSelectByMake (
	@CarMakeId int
)
AS
BEGIN
	SELECT CarModels.CarMakeId, CarMakes.CarMakeName, CarModels.CarModelId, CarModels.CarModelName, CarModels.CarModelAddedDate, CarModels.UserId, AspNetUsers.Email
	FROM CarModels
		LEFT JOIN CarMakes ON CarModels.CarMakeId = CarMakes.CarMakeId
		LEFT JOIN AspNetUsers ON CarModels.UserId = AspNetUsers.Id
	WHERE CarModels.CarMakeId = @CarMakeId
	ORDER BY CarMakeName ASC
END
GO





IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
	WHERE ROUTINE_NAME='CarModelsInsert')
	DROP PROCEDURE CarModelsInsert
GO
CREATE PROCEDURE CarModelsInsert (
	@CarModelId int output,
	@CarMakeId int,
	@CarModelName nvarchar(20),
	@UserId nvarchar(128)
)
AS
BEGIN
	INSERT INTO CarModels (CarMakeId, CarModelName, UserId)
	VALUES(@CarMakeId, @CarModelName, @UserId);

	SET @CarModelId = SCOPE_IDENTITY();
END
GO





IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
	WHERE ROUTINE_NAME='SelectFeaturedVehicles')
	DROP PROCEDURE SelectFeaturedVehicles
GO

CREATE PROCEDURE SelectFeaturedVehicles AS
BEGIN
	SELECT Vehicles.VehicleId, Vehicles.VinNumber, Vehicles.VehicleImageFileName, Vehicles.ModelYear, Vehicles.SalePrice, CarMakes.CarMakeId, CarMakes.CarMakeName, 
	CarModels.CarModelId, CarModels.CarModelName
	FROM Vehicles
		LEFT JOIN CarMakes ON Vehicles.CarMakeId = CarMakes.CarMakeId
		LEFT JOIN CarModels ON Vehicles.CarModelId = CarModels.CarModelId
	WHERE Vehicles.IsFeaturedCar = 1 AND IsPurchasedCar = 0
	ORDER BY MSRP DESC, CarMakeName ASC
END
GO


--Gets details for individual vehicle (by VehicleId)
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
	WHERE ROUTINE_NAME='SelectVehicleDetails')
	DROP PROCEDURE SelectVehicleDetails
GO

CREATE PROCEDURE SelectVehicleDetails (
	@VehicleId int
)AS
BEGIN
	SELECT v.VehicleId, v.VinNumber, v.ModelYear, v.MSRP, v.SalePrice, v.VehicleDescription, v.VehicleImageFileName, v.IsNewCar, v.VehicleMileage, 
		v.BodyColorTypeId, bodyColor.ColorType AS BodyColor, v.InteriorColorTypeId, interiorColor.ColorType AS InteriorColor, v.CarBodyTypeId, body.CarBodyType, 
		v.CarMakeId, make.CarMakeName, v.CarModelId, model.CarModelName, 
		v.TransmissionTypeId, t.TransmissionType, v.IsFeaturedCar, v.IsPurchasedCar
	FROM Vehicles v
		LEFT JOIN Colors bodyColor ON v.BodyColorTypeId = bodyColor.ColorTypeId
		LEFT JOIN Colors interiorColor ON v.InteriorColorTypeId = interiorColor.ColorTypeId
		LEFT JOIN CarBodies body ON v.CarBodyTypeId = body.CarBodyTypeId
		LEFT JOIN CarMakes make ON v.CarMakeId = make.CarMakeId
		LEFT JOIN CarModels model ON v.CarModelId = model.CarModelId
		LEFT JOIN Transmissions t ON v.TransmissionTypeId = t.TransmissionTypeId
	WHERE v.VehicleId = @VehicleId
END
GO


--Selects all vehicles
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
	WHERE ROUTINE_NAME='VehicleDetailsSelectAll')
	DROP PROCEDURE VehicleDetailsSelectAll
GO

CREATE PROCEDURE VehicleDetailsSelectAll AS
BEGIN
	SELECT v.VehicleId, v.VinNumber, v.ModelYear, v.MSRP, v.SalePrice, v.VehicleDescription, v.VehicleImageFileName, v.IsNewCar, v.VehicleMileage, 
		v.BodyColorTypeId, bodyColor.ColorType AS BodyColor, v.InteriorColorTypeId, interiorColor.ColorType AS InteriorColor, v.CarBodyTypeId, body.CarBodyType, 
		v.CarMakeId, make.CarMakeName, v.CarModelId, model.CarModelName, 
		v.TransmissionTypeId, t.TransmissionType
	FROM Vehicles v
		LEFT JOIN Colors bodyColor ON v.BodyColorTypeId = bodyColor.ColorTypeId
		LEFT JOIN Colors interiorColor ON v.InteriorColorTypeId = interiorColor.ColorTypeId
		LEFT JOIN CarBodies body ON v.CarBodyTypeId = body.CarBodyTypeId
		LEFT JOIN CarMakes make ON v.CarMakeId = make.CarMakeId
		LEFT JOIN CarModels model ON v.CarModelId = model.CarModelId
		LEFT JOIN Transmissions t ON v.TransmissionTypeId = t.TransmissionTypeId
END
GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'VehiclesInsert')
		DROP PROCEDURE VehiclesInsert
GO
CREATE PROCEDURE VehiclesInsert (
	@VehicleId int output,
	@VinNumber nvarchar(17),
	@CarMakeId int,
	@CarModelId int,
	@BodyColorTypeId int,
	@InteriorColorTypeId int,
	@TransmissionTypeId int,
	@CarBodyTypeId int,
	@Modelyear smallint,
	@VehicleMileage int,
	@MSRP decimal(10,2),
	@SalePrice decimal(10,2),
	@VehicleDescription nvarchar(max),
	@VehicleImageFileName varchar(30),
	@IsNewCar bit

)
AS
BEGIN
	INSERT INTO Vehicles (VinNumber, CarMakeId, CarModelId, BodyColorTypeId, InteriorColorTypeId, TransmissionTypeId, CarBodyTypeId, ModelYear, 
	VehicleMileage, MSRP, SalePrice, VehicleDescription, VehicleImageFileName, IsNewCar)
	VALUES(@VinNumber, @CarMakeId, @CarModelId, @BodyColorTypeId, @InteriorColorTypeId, @TransmissionTypeId, @CarBodyTypeId, @ModelYear, 
	@VehicleMileage, @MSRP, @SalePrice, @VehicleDescription, @VehicleImageFileName, @IsNewCar);

	SET @VehicleId = SCOPE_IDENTITY();
END
GO



IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'VehiclesUpdate')
		DROP PROCEDURE VehiclesUpdate
GO

CREATE PROCEDURE VehiclesUpdate (
	@VehicleId int,
	@VinNumber nvarchar(17),
	@CarMakeId int,
	@CarModelId int,
	@BodyColorTypeId int,
	@InteriorColorTypeId int,
	@TransmissionTypeId int,
	@CarBodyTypeId int,
	@Modelyear smallint,
	@VehicleMileage int,
	@MSRP decimal(10,2),
	@SalePrice decimal(10,2),
	@VehicleDescription nvarchar(max),
	@VehicleImageFileName varchar(30),
	@IsNewCar bit,
	@IsFeaturedCar bit
)
AS
BEGIN
	UPDATE Vehicles SET
	VinNumber = @VinNumber,
	CarMakeId = @CarMakeId,
	CarModelId = @CarModelId,
	BodyColorTypeId = @BodyColorTypeId,
	InteriorColorTypeId = @InteriorColorTypeId,
	TransmissionTypeId = @TransmissionTypeId,
	CarBodyTypeId = @CarBodyTypeId,
	ModelYear = @Modelyear,
	VehicleMileage = @VehicleMileage,
	MSRP = @MSRP,
	SalePrice = @SalePrice,
	VehicleDescription = @VehicleDescription,
	VehicleImageFileName = @VehicleImageFileName,
	IsNewCar = @IsNewCar,
	IsFeaturedCar = @IsFeaturedCar

WHERE VehicleId = @VehicleId
END
GO




IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'DeleteVehicle')
		DROP PROCEDURE DeleteVehicle
GO 
CREATE PROCEDURE DeleteVehicle (
	@VehicleId int
) AS
BEGIN
	DELETE FROM Vehicles
	WHERE VehicleId = @VehicleId
END
GO



IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
	WHERE ROUTINE_NAME='PurchasesInsert')
	DROP PROCEDURE PurchasesInsert
GO
CREATE PROCEDURE PurchasesInsert (
	@PurchaseId int output,
	@PurchasePaymentTypeId int,
	@UserId nvarchar(128),	
	@VehicleId int,
	@PurchasePrice decimal(10,2),
	@CustomerName nvarchar(50),
	@CustomerPhone nvarchar(12),
	@CustomerEmail nvarchar(50),
	@CustomerStreet1 nvarchar(50),
	@CustomerStreet2 nvarchar(50),
	@CustomerCity nvarchar(30),
	@StateId char(2),
	@CustomerZip nvarchar(5)
)
AS
BEGIN

	INSERT INTO Purchases (PaymentTypeId, UserId, VehicleId, PurchasePrice, CustomerName, CustomerPhone, 
	CustomerEmail, CustomerStreet1, CustomerStreet2, CustomerCity, StateId, CustomerZip)

	VALUES(@PurchasePaymentTypeId, @UserId, @VehicleId, @PurchasePrice, @CustomerName, @CustomerPhone, 
	@CustomerEmail, @CustomerStreet1, @CustomerStreet2, @CustomerCity, @StateId, @CustomerZip);
	
	SET @PurchaseId = SCOPE_IDENTITY();
	
	UPDATE Vehicles SET
	IsPurchasedCar = 1
	WHERE VehicleId = @VehicleId;
END
GO



IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
	WHERE ROUTINE_NAME='PurchaseSelectById')
	DROP PROCEDURE PurchaseSelectById
GO

CREATE PROCEDURE PurchaseSelectById (
	@PurchaseId int
)AS
BEGIN
	SELECT p.PurchaseId, p.PaymentTypeId, pt.PayType, p.UserId, p.VehicleId, p.PurchasePrice, p.PurchaseDate, p.CustomerName, p.CustomerPhone, 
	p.CustomerEmail, p.CustomerStreet1, p.CustomerStreet2, p.CustomerCity, p.StateId, p.CustomerZip
	FROM Purchases p
	LEFT JOIN PaymentTypes pt ON p.PaymentTypeId = pt.PaymentTypeId
	WHERE PurchaseId = @PurchaseId
END
GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetNewVehicleInventoryReport')
	DROP PROCEDURE GetNewVehicleInventoryReport
GO
CREATE PROCEDURE GetNewVehicleInventoryReport AS
BEGIN
SELECT v.ModelYear AS 'Year', make.CarMakeName AS Make, model.CarModelName AS Model, COUNT(model.CarModelName) AS 'Count', SUM(v.MSRP) AS 'Stock Value'
FROM Vehicles v
LEFT JOIN CarMakes make ON v.CarMakeId = make.CarMakeId
LEFT JOIN CarModels model ON v.CarModelId = model.CarModelId
WHERE v.IsPurchasedCar = 0 AND v.IsNewCar = 1
GROUP BY make.CarMakeName, model.CarModelName, v.ModelYear
ORDER BY 'Stock Value' DESC, make.CarMakeName ASC, model.CarModelName DESC, v.ModelYear DESC

END
GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetUsedVehicleInventoryReport')
	DROP PROCEDURE GetUsedVehicleInventoryReport
GO
CREATE PROCEDURE GetUsedVehicleInventoryReport AS
BEGIN
SELECT v.ModelYear AS 'Year', make.CarMakeName AS Make, model.CarModelName AS Model, COUNT(model.CarModelName) AS 'Count', SUM(v.MSRP) AS 'Stock Value'
FROM Vehicles v
LEFT JOIN CarMakes make ON v.CarMakeId = make.CarMakeId
LEFT JOIN CarModels model ON v.CarModelId = model.CarModelId
WHERE v.IsPurchasedCar = 0 AND v.IsNewCar = 0
GROUP BY make.CarMakeName, model.CarModelName, v.ModelYear
ORDER BY 'Stock Value' DESC, make.CarMakeName ASC, model.CarModelName DESC, v.ModelYear DESC

END
GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetUserList')
	DROP PROCEDURE GetUserList
GO
CREATE PROCEDURE GetUserList AS
BEGIN
SELECT users.Id AS UserId, users.LastName, users.FirstName, users.Email, roles.Name AS UserRole
FROM AspNetUsers users
LEFT JOIN AspNetRoles roles ON users.UserRoleId = roles.Id
ORDER BY users.Id ASC

END
GO



IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetUsersWithSales')
	DROP PROCEDURE GetUsersWithSales
GO
CREATE PROCEDURE GetUsersWithSales AS
BEGIN
SELECT CONCAT(users.Firstname, ' ', users.LastName) AS 'UserName', users.Id AS UserId
FROM Purchases p
LEFT JOIN AspNetUsers users ON p.UserId = users.Id
GROUP BY users.Firstname, users.LastName, users.Id
END
GO

--Basis for SalesReport:
--Finds Total$ and #vehicles sold for each user; can sort on specific user, and min/max date
--USE GuildCarsProd
--SELECT users.Id AS UserId, CONCAT(users.Firstname, ' ', users.LastName) AS 'UserName', SUM(p.PurchasePrice) AS 'TotalSales', COUNT(p.UserId) AS 'TotalVehicles'
--FROM Purchases p
--LEFT JOIN AspNetUsers users ON p.UserId = users.Id
--WHERE p.PurchaseDate <= @MaxDate AND p.PurchaseDate >= @MinDate
--AND UserId = @UserId
--GROUP BY users.Firstname, users.LastName, users.Id
--ORDER BY TotalSales DESC, TotalVehicles DESC



IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
	WHERE ROUTINE_NAME='GetAspNetRoles')
	DROP PROCEDURE GetAspNetRoles
GO
CREATE PROCEDURE GetAspNetRoles AS
BEGIN
	SELECT role.Id, role.Name AS 'Name'
	FROM AspNetRoles role
	ORDER BY role.Name ASC
END
GO


--Updates role ID for AspNetUser then
--Deletes existing roles from Bridge-table and adds new role
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
	WHERE ROUTINE_NAME='UpdateUserRole')
	DROP PROCEDURE UpdateUserRole
GO
CREATE PROCEDURE UpdateUserRole (
	@UserId nvarchar(128),
	@UserRoleId nvarchar(128)
)
AS
BEGIN

	UPDATE AspNetUsers SET
	UserRoleId = @UserRoleId
	WHERE Id = @UserId;

	DELETE FROM AspNetUserRoles
	WHERE UserId = @UserId

	INSERT INTO AspNetUserRoles (UserId, RoleId)
	VALUES (@UserId, @UserRoleId)

END
GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
	WHERE ROUTINE_NAME='GetUserById')
	DROP PROCEDURE GetUserById
GO
CREATE PROCEDURE GetUserById (
	@UserId nvarchar(128)
)AS
BEGIN
	SELECT Id AS UserId, Firstname, LastName, Email, UserRoleId
	FROM AspNetUsers
	WHERE Id = @UserId
END
GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
	WHERE ROUTINE_NAME='GetAllUserRoles')
	DROP PROCEDURE GetAllUserRoles
GO
CREATE PROCEDURE GetAllUserRoles AS
BEGIN
	SELECT UserId, RoleId
	FROM AspNetUserRoles role
	ORDER BY UserId ASC
END
GO




IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
	WHERE ROUTINE_NAME='ContactsInsert')
	DROP PROCEDURE ContactsInsert
GO
CREATE PROCEDURE ContactsInsert (
	@ContactId int output,
	@ContactName nvarchar(50),
	@ContactEmail nvarchar(50),
	@ContactPhone nvarchar(12),
	@ContactMessage nvarchar(500)
)
AS
BEGIN
	INSERT INTO Contacts (ContactName, ContactEmail, ContactPhone, ContactMessage)
	VALUES(@ContactName, @ContactEmail, @ContactPhone, @ContactMessage);

	SET @ContactId = SCOPE_IDENTITY();
END
GO



IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'ContactsDelete')
		DROP PROCEDURE ContactsDelete
GO 
CREATE PROCEDURE ContactsDelete (
	@ContactId int
) AS
BEGIN
	DELETE FROM Contacts
	WHERE ContactId = @ContactId
END
GO




IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
	WHERE ROUTINE_NAME='ContactsSelectAll')
	DROP PROCEDURE ContactsSelectAll
GO

CREATE PROCEDURE ContactsSelectAll AS
BEGIN
	SELECT ContactId, ContactName, ContactEmail, ContactPhone, ContactMessage, ContactSentDate
	FROM Contacts
	ORDER BY ContactId DESC
END
GO