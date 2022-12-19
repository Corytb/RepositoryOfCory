USE GuildCars
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
	WHERE ROUTINE_NAME='DbResetGuildCars')
	DROP PROCEDURE DbResetGuildCars
GO

CREATE PROCEDURE DbResetGuildCars AS
BEGIN

	DELETE FROM Purchases;
	DELETE FROM Vehicles;
	DELETE FROM CarModels;
	DELETE FROM States;
	DELETE FROM PaymentTypes;
	DELETE FROM CarBodies;
	DELETE FROM Colors;
	DELETE FROM Specials;
	DELETE FROM Transmissions;
	DELETE FROM Contacts;
	--AspNet tables commented out for testing
	--DELETE FROM AspNetRoles;
	--DELETE FROM AspNetUsers;
	DELETE FROM CarMakes;
	
	DBCC CHECKIDENT ('Contacts', RESEED, 1)
	DBCC CHECKIDENT ('CarMakes', RESEED, 1)
	DBCC CHECKIDENT ('CarModels', RESEED, 1)
	DBCC CHECKIDENT ('Specials', RESEED, 1)
	DBCC CHECKIDENT ('Vehicles', RESEED, 0)
	DBCC CHECKIDENT ('Purchases', RESEED, 1)


	SET IDENTITY_INSERT Contacts ON;
	INSERT INTO Contacts(ContactId, ContactName, ContactEmail, ContactPhone, ContactMessage)
	VALUES(1, 'Jamie James', 'jjames@contact.com', '612-220-5640', 'Just checking on the one vehicle I saw last week!'),
	(2, 'Kenny Kanes', 'kenkanes@contact.com', '612-407-9242', 'I love your vehicles you guys are the best!');
	SET IDENTITY_INSERT Contacts OFF;


	INSERT INTO States (StateId, StateName)
	VALUES('OH', 'Ohio'),
	('KY', 'Kentucky'),
	('MN', 'Minnesota');

	INSERT INTO PaymentTypes (PaymentTypeId, PayType)
	VALUES(1, 'Bank Finance'),
	(2, 'Cash'),
	(3, 'Dealer Finance');
	
	INSERT INTO CarBodies (CarBodyTypeId, CarBodyType)
	VALUES(1, 'Car'),
	(2, 'SUV'),
	(3, 'Truck'),
	(4, 'Van');
		
	INSERT INTO Colors (ColorTypeId, ColorTYpe)
	VALUES(1, 'Red'),
	(2, 'Orange'),
	(3, 'Yellow'),
	(4, 'Green'),
	(5, 'Blue');

	SET IDENTITY_INSERT Specials ON;
	INSERT INTO Specials (SpecialId, SpecialTitle, SpecialDescription, SpecialImageFileName)
	VALUES(1, 'Your new car!', 'Get this brand new Lotus for just $1! (with qualifying excessory purchases)', 'specialImagePlaceholder.png'),
	(2, 'Ford Festiva!', 'This classic Ford can be yours if you come to pick it up!', 'specialImagePlaceholder.png'),
	(3, 'SPECIAL: Formula 1 racer!', 'Feel the need for more speed!', 'specialImagePlaceholder.png'),
	(4, 'See it to believe it!', 'Your car, your way!', 'specialImagePlaceholder.png'),
	(5, 'WE pay YOU!', 'Trade in for a car of lesser value and we will give you cash on the spot!', 'specialImagePlaceholder.png'),
	(6, 'Test6', 'Test description', 'specialImagePlaceholder.png');
	SET IDENTITY_INSERT Specials OFF;

	INSERT INTO Transmissions (TransmissionTypeId, TransmissionType)
	VALUES(1, 'Automatic'),
	(2, 'Manual'),
	(3, 'NA');



	--INSERT INTO AspNetRoles (Id, Name)
	--VALUES(1, 'Admin'),
	--(2, 'Sales'),
	--(3, 'Disabled');


	--COMMENTED OUT FOR TESTING

	--INSERT INTO AspNetUsers (FirstName, LastName,Id, UserRoleId, Email, EmailConfirmed, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, UserName)
	--VALUES('Kurt', 'Brathall', '00000000-0000-0000-0000-000000000000', 1, 'adminTest@guildcars.com', 0, 0, 0, 0, 0, 'AdminTest'),
	--('William', 'Klatt', '11111111-1111-1111-1111-111111111111', 2, 'SalesTest1@guildcars.com', 0, 0, 0, 0, 0, 'SalesTest1'),
	--('Chad', 'Rehm', '22222222-2222-2222-2222-222222222222', 2, 'SalesTest2@guildcars.com', 0, 0, 0, 0, 0, 'SalesTest2'),	
	--('Carnell', 'Fayson', '33333333-3333-3333-3333-333333333333', 3, 'DisabledTest@guildcars.com', 0, 0, 0, 0, 0, 'DisabledTest');

	
	SET IDENTITY_INSERT CarMakes ON;
	INSERT INTO CarMakes (CarMakeId, CarMakeName, UserId)
	VALUES(1, 'Ford', '00000000-0000-0000-0000-000000000000'),
	(2, 'Lotus', '00000000-0000-0000-0000-000000000000'),
	(3, 'Tesla', '00000000-0000-0000-0000-000000000000'),
	(4, 'Toyota', '00000000-0000-0000-0000-000000000000');
	SET IDENTITY_INSERT CarMakes OFF;
	

		
	SET IDENTITY_INSERT CarModels ON;
	INSERT INTO CarModels (CarModelId, CarMakeId, CarModelName, UserId)
	VALUES(1, 1, 'Mustang', '00000000-0000-0000-0000-000000000000'),
	(2, 1, 'Festiva', '00000000-0000-0000-0000-000000000000'),
	(3, 2, 'Elise', '00000000-0000-0000-0000-000000000000'),
	(4, 2, 'Mark I', '00000000-0000-0000-0000-000000000000'),
	(5, 3, 'Model S', '00000000-0000-0000-0000-000000000000');
	SET IDENTITY_INSERT CarModels OFF;

	
	INSERT INTO Vehicles (VinNumber, CarMakeId, CarModelId, BodyColorTypeId, InteriorColorTypeId, TransmissionTypeId, CarBodyTypeId, ModelYear, VehicleMileage, 
		MSRP, SalePrice, VehicleDescription, VehicleImageFileName, IsNewCar, IsFeaturedCar, IsPurchasedCar)
	VALUES
	('01111111111111112', 1, 1, 1, 2, 1, 1, '2001', 10000, 100000, 99000, 'A great little car', 'placeholder.png', 0, 1, 0),
	('21111111111111113', 1, 1, 1, 2, 1, 1, '2001', 10000, 100000, 99000, 'A great little car', 'placeholder.png', 0, 1, 0),
	('31111111111111114', 1, 1, 1, 2, 1, 1, '2001', 10000, 100000, 99000, 'A great little car', 'placeholder.png', 0, 1, 0),
	('41111111111111115', 1, 1, 1, 2, 1, 1, '2001', 10000, 100000, 99000, 'A great little car', 'placeholder.png', 0, 1, 0),
	('51111111111111116', 1, 1, 1, 2, 1, 1, '2001', 10000, 100000, 99000, 'A great little car', 'placeholder.png', 0, 1, 0),
	('61111111111111117', 1, 1, 1, 2, 1, 1, '2001', 10000, 100000, 99000, 'A great little car', 'placeholder.png', 0, 1, 0),
	('71111111111111118', 1, 1, 1, 2, 1, 1, '2001', 10000, 100000, 99000, 'A great little car', 'placeholder.png', 0, 1, 0),
	('81111111111111119', 1, 1, 1, 2, 1, 1, '2001', 10000, 100000, 99000, 'A great little car', 'placeholder.png', 0, 1, 0),
	('91111111111111110', 1, 1, 1, 2, 1, 1, '2001', 10000, 100000, 99000, 'A great little car', 'placeholder.png', 0, 1, 0),
	('01111111111111121', 1, 1, 1, 2, 1, 1, '2001', 10000, 100000, 99000, 'A great little car', 'placeholder.png', 0, 1, 0),
	('12111111111111122', 1, 1, 1, 2, 1, 1, '2001', 10000, 100000, 99000, 'A great little car', 'placeholder.png', 0, 1, 0),
	('13111111111111123', 1, 1, 1, 2, 1, 1, '2001', 10000, 100000, 99000, 'A great little car', 'placeholder.png', 0, 1, 0),
	('14111111111111124', 1, 1, 1, 2, 1, 1, '2001', 10000, 100000, 99000, 'A great little car', 'placeholder.png', 0, 1, 0),
	('15111111111111125', 1, 1, 1, 2, 1, 1, '2001', 10000, 100000, 99000, 'A great little car', 'placeholder.png', 0, 1, 0),
	('16111111111111126', 1, 1, 1, 2, 1, 1, '2001', 10000, 100000, 99000, 'A great little car', 'placeholder.png', 0, 1, 0),
	('17111111111111127', 1, 1, 1, 2, 1, 1, '2001', 10000, 100000, 99000, 'A great little car', 'placeholder.png', 0, 1, 0),
	('18111111111111128', 1, 1, 1, 2, 1, 1, '2001', 10000, 100000, 99000, 'A great little car', 'placeholder.png', 0, 1, 0),
	('19111111111111129', 1, 1, 1, 2, 1, 1, '2001', 10000, 100000, 99000, 'A great little car', 'placeholder.png', 0, 1, 0),
	('10111111111111130', 1, 1, 1, 2, 1, 1, '2001', 10000, 100000, 99000, 'A great little car', 'placeholder.png', 0, 1, 0),
	('11211111111111111', 1, 1, 1, 2, 1, 1, '1968', 10000, 50000, 50000, 'A great little car', 'placeholder.png', 0, 1, 0),
	('22322222222222222', 1, 1, 2, 3, 2, 1, '1992', 10000, 50000, 50000, 'A great little car', 'placeholder.png', 0, 1, 0),
	('33433333333333333', 1, 1, 3, 4, 3, 1, '1993', 10000, 50000, 50000, 'A great little car', 'placeholder.png', 0, 1, 0),
	('44544444444444444', 1, 1, 4, 5, 1, 1, '1994', 10000, 50000, 50000, 'A great little car', 'placeholder.png', 0, 1, 0),
	('55655555555555555', 1, 1, 5, 1, 2, 1, '1995', 10000, 50000, 50000, 'A great little car', 'placeholder.png', 0, 1, 0),
	('66766666666666666', 1, 1, 4, 5, 1, 1, '1994', 10000, 50000, 50000, 'A great little car', 'placeholder.png', 0, 1, 0),
	('11111111111111112', 1, 1, 1, 2, 1, 1, '2001', 10000, 100000, 99000, 'A great little car', 'placeholder.png', 0, 1, 1),
	('11111111111111113', 1, 1, 1, 2, 1, 1, '2001', 10000, 100000, 99000, 'A great little car', 'placeholder.png', 0, 1, 1),
	('11111111111111114', 1, 1, 1, 2, 1, 1, '2001', 10000, 100000, 99000, 'A great little car', 'placeholder.png', 0, 1, 1),
	('11111111111111115', 1, 1, 1, 2, 1, 1, '2001', 10000, 100000, 99000, 'A great little car', 'placeholder.png', 0, 1, 1),
	('11111111111111116', 1, 1, 1, 2, 1, 1, '2001', 10000, 100000, 99000, 'A great little car', 'placeholder.png', 0, 1, 1),
	('11111111111111117', 1, 1, 1, 2, 1, 1, '2001', 10000, 100000, 99000, 'A great little car', 'placeholder.png', 0, 1, 1),
	('11111111111111118', 1, 1, 1, 2, 1, 1, '2001', 10000, 100000, 99000, 'A great little car', 'placeholder.png', 0, 1, 1),
	('11111111111111119', 1, 1, 1, 2, 1, 1, '2001', 10000, 100000, 99000, 'A great little car', 'placeholder.png', 0, 1, 1),
	('11111111111111110', 1, 1, 1, 2, 1, 1, '2001', 10000, 100000, 99000, 'A great little car', 'placeholder.png', 0, 1, 1),
	('11111111111111121', 1, 1, 1, 2, 1, 1, '2001', 10000, 100000, 99000, 'A great little car', 'placeholder.png', 0, 1, 1),
	('11111111111111122', 1, 1, 1, 2, 1, 1, '2001', 10000, 100000, 99000, 'A great little car', 'placeholder.png', 0, 1, 1),
	('11111111111111123', 1, 1, 1, 2, 1, 1, '2001', 10000, 100000, 99000, 'A great little car', 'placeholder.png', 0, 1, 1),
	('11111111111111124', 1, 1, 1, 2, 1, 1, '2001', 10000, 100000, 99000, 'A great little car', 'placeholder.png', 0, 1, 1),
	('11111111111111125', 1, 1, 1, 2, 1, 1, '2001', 10000, 100000, 99000, 'A great little car', 'placeholder.png', 0, 1, 1),
	('11111111111111126', 1, 1, 1, 2, 1, 1, '2001', 10000, 100000, 99000, 'A great little car', 'placeholder.png', 0, 1, 1),
	('11111111111111127', 1, 1, 1, 2, 1, 1, '2001', 10000, 100000, 99000, 'A great little car', 'placeholder.png', 0, 1, 1),
	('11111111111111128', 1, 1, 1, 2, 1, 1, '2001', 10000, 100000, 99000, 'A great little car', 'placeholder.png', 0, 1, 1),
	('11111111111111129', 1, 1, 1, 2, 1, 1, '2001', 10000, 100000, 99000, 'A great little car', 'placeholder.png', 0, 1, 1),
	('11111111111111130', 1, 1, 1, 2, 1, 1, '2001', 10000, 100000, 99000, 'A great little car', 'placeholder.png', 0, 1, 1),
	('11111111111111111', 1, 1, 1, 2, 1, 1, '1968', 10000, 50000, 50000, 'A great little car', 'placeholder.png', 0, 1, 1),
	('22222222222222222', 1, 1, 2, 3, 2, 1, '1992', 10000, 50000, 50000, 'A great little car', 'placeholder.png', 0, 1, 1),
	('33333333333333333', 1, 1, 3, 4, 3, 1, '1993', 10000, 50000, 50000, 'A great little car', 'placeholder.png', 0, 1, 1),
	('44444444444444444', 1, 1, 4, 5, 1, 1, '1994', 10000, 50000, 50000, 'A great little car', 'placeholder.png', 0, 0, 1),
	('55555555555555555', 1, 1, 5, 1, 2, 1, '1995', 10000, 50000, 50000, 'A great little car', 'placeholder.png', 0, 0, 1),
	('66666666666666666', 1, 1, 4, 5, 1, 1, '1994', 10000, 50000, 50000, 'A great little car', 'placeholder.png', 0, 0, 1),
	('77777777777777777', 1, 2, 4, 5, 1, 1, '2015', 950, 70000, 69000, 'A great little car', 'placeholder.png', 1, 0, 0),
	('88888888888888888', 2, 3, 4, 5, 1, 2, '2016', 1000, 80000, 79000, 'A great little car', 'placeholder.png', 1, 0, 0),
	('99999999999999999', 2, 4, 4, 5, 1, 3, '2017', 10, 90000, 89000, 'A great little car', 'placeholder.png', 1, 0, 0),
	('wwwwwwwwwwwwwwwww', 2, 4, 4, 5, 1, 3, '2022', 100, 110000, 109000, 'A great little car', 'placeholder.png', 1, 0, 0),
	('zzzzzzzzzzzzzzzzz', 2, 4, 4, 5, 1, 3, '2023', 200, 120000, 119000, 'A great little car', 'placeholder.png', 1, 0, 0),
	('zzzzzzzzzzzzzzzz1', 2, 4, 4, 5, 1, 3, '2023', 200, 120000, 119000, 'A great little car', 'placeholder.png', 1, 0, 0),
	('zzzzzzzzzzzzzzzz2', 2, 4, 4, 5, 1, 3, '2023', 200, 120000, 119000, 'A great little car', 'placeholder.png', 1, 0, 0),
	('zzzzzzzzzzzzzzzz3', 2, 4, 4, 5, 1, 3, '2023', 200, 120000, 119000, 'A great little car', 'placeholder.png', 1, 0, 0),
	('zzzzzzzzzzzzzzzz4', 2, 4, 4, 5, 1, 3, '2023', 200, 120000, 119000, 'A great little car', 'placeholder.png', 1, 0, 0),
	('zzzzzzzzzzzzzzzz5', 2, 4, 4, 5, 1, 3, '2023', 200, 120000, 119000, 'A great little car', 'placeholder.png', 1, 0, 0),
	('zzzzzzzzzzzzzzzz6', 2, 4, 4, 5, 1, 3, '2023', 200, 120000, 119000, 'A great little car', 'placeholder.png', 1, 0, 0),
	('zzzzzzzzzzzzzzzz7', 2, 4, 4, 5, 1, 3, '2023', 200, 120000, 119000, 'A great little car', 'placeholder.png', 1, 0, 0),
	('zzzzzzzzzzzzzzzz8', 2, 4, 4, 5, 1, 3, '2023', 200, 120000, 119000, 'A great little car', 'placeholder.png', 1, 0, 0),
	('zzzzzzzzzzzzzzzz9', 2, 4, 4, 5, 1, 3, '2023', 200, 120000, 119000, 'A great little car', 'placeholder.png', 1, 0, 0),
	('zzzzzzzzzzzzzzz10', 2, 4, 4, 5, 1, 3, '2023', 200, 120000, 119000, 'A great little car', 'placeholder.png', 1, 0, 0),
	('zzzzzzzzzzzzzzz11', 2, 4, 4, 5, 1, 3, '2023', 200, 120000, 119000, 'A great little car', 'placeholder.png', 1, 0, 0),
	('zzzzzzzzzzzzzzz12', 2, 4, 4, 5, 1, 3, '2023', 200, 120000, 119000, 'A great little car', 'placeholder.png', 1, 0, 0),
	('zzzzzzzzzzzzzzz13', 2, 4, 4, 5, 1, 3, '2023', 200, 120000, 119000, 'A great little car', 'placeholder.png', 1, 0, 0),
	('zzzzzzzzzzzzzzz14', 2, 4, 4, 5, 1, 3, '2023', 200, 120000, 119000, 'A great little car', 'placeholder.png', 1, 0, 0),
	('zzzzzzzzzzzzzzz15', 2, 4, 4, 5, 1, 3, '2023', 200, 120000, 119000, 'A great little car', 'placeholder.png', 1, 0, 0),
	('yyyyyyyyyyyyyyyyy', 3, 5, 4, 5, 1, 4, '2021', 500, 130000, 129000, 'A great little car', 'placeholder.png', 1, 0, 0);


	SET IDENTITY_INSERT Purchases ON;
	INSERT INTO Purchases (PurchaseId, PaymentTypeId, UserId, VehicleId, PurchasePrice, PurchaseDate, CustomerName, CustomerPhone, CustomerEmail, CustomerStreet1, CustomerStreet2, CustomerCity, StateId, CustomerZip)
	VALUES
	(1, 1, '11111111-1111-1111-1111-111111111111', 30, 99000, '10/10/2000', 'Dave Brathall', '612-866-6537', 'dave@brathall.com', '123 Hudson St', 'Suite 208', 'Stillwater', 'MN', '55417'),
	(2, 2, '11111111-1111-1111-1111-111111111111', 27, 99000, '10/10/2001', 'Dave Brathall', '612-866-6537', 'dave@brathall.com', '123 Hudson St', 'Suite 208', 'Stillwater', 'MN', '55417'),
	(3, 3, '22222222-2222-2222-2222-222222222222', 28, 99000, '10/10/2002', 'Dave Brathall', '612-866-6537', 'dave@brathall.com', '123 Hudson St', 'Suite 208', 'Stillwater', 'MN', '55417'),
	(4, 1, 'f68319c3-5213-4eda-b646-16415ca1618c', 29, 99000, '10/10/2003', 'Dave Brathall', '612-866-6537', 'dave@brathall.com', '123 Hudson St', 'Suite 208', 'Stillwater', 'MN', '55417'),
	(5, 2, '11111111-1111-1111-1111-111111111111', 30, 99000, '10/10/2004', 'Dave Brathall', '612-866-6537', 'dave@brathall.com', '123 Hudson St', 'Suite 208', 'Stillwater', 'MN', '55417'),
	(6, 3, '22222222-2222-2222-2222-222222222222', 31, 99000, '10/10/2005', 'Dave Brathall', '612-866-6537', 'dave@brathall.com', '123 Hudson St', 'Suite 208', 'Stillwater', 'MN', '55417'),
	(7, 1, '11111111-1111-1111-1111-111111111111', 32, 99000, '10/10/2006', 'Dave Brathall', '612-866-6537', 'dave@brathall.com', '123 Hudson St', 'Suite 208', 'Stillwater', 'MN', '55417'),
	(8, 2, '11111111-1111-1111-1111-111111111111', 33, 99000, '10/10/2007', 'Dave Brathall', '612-866-6537', 'dave@brathall.com', '123 Hudson St', 'Suite 208', 'Stillwater', 'MN', '55417'),
	(9, 3, 'f68319c3-5213-4eda-b646-16415ca1618c', 34, 99000, '10/10/2008', 'Dave Brathall', '612-866-6537', 'dave@brathall.com', '123 Hudson St', 'Suite 208', 'Stillwater', 'MN', '55417'),
	(10, 1, 'f68319c3-5213-4eda-b646-16415ca1618c', 35, 99000, '10/10/2009', 'Dave Brathall', '612-866-6537', 'dave@brathall.com', '123 Hudson St', 'Suite 208', 'Stillwater', 'MN', '55417'),
	(11, 2, '22222222-2222-2222-2222-222222222222', 36, 99000, '10/10/2010', 'Dave Brathall', '612-866-6537', 'dave@brathall.com', '123 Hudson St', 'Suite 208', 'Stillwater', 'MN', '55417'),
	(12, 1, '00000000-0000-0000-0000-000000000000', 37, 99000, '10/10/2011', 'Dave Brathall', '612-866-6537', 'dave@brathall.com', '123 Hudson St', 'Suite 208', 'Stillwater', 'MN', '55417'),
	(13, 1, '33333333-3333-3333-3333-333333333333', 38, 99000, '10/10/2012', 'Dave Brathall', '612-866-6537', 'dave@brathall.com', '123 Hudson St', 'Suite 208', 'Stillwater', 'MN', '55417'),
	(14, 1, '33333333-3333-3333-3333-333333333333', 39, 99000, '10/10/2013', 'Dave Brathall', '612-866-6537', 'dave@brathall.com', '123 Hudson St', 'Suite 208', 'Stillwater', 'MN', '55417');
	SET IDENTITY_INSERT Purchases OFF;
	
END
