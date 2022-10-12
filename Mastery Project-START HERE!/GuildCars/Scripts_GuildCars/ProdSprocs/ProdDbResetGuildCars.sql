USE GuildCarsProd
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
	WHERE ROUTINE_NAME='DbResetGuildCarsProd')
	DROP PROCEDURE DbResetGuildCarsProd
GO

CREATE PROCEDURE DbResetGuildCarsProd AS
BEGIN

	DELETE FROM States;
	DELETE FROM PaymentTypes;
	DELETE FROM CarBodies;
	DELETE FROM Colors;
	DELETE FROM Transmissions;
	--AspNet tables commented out for testing
	DELETE FROM AspNetRoles;
	--DELETE FROM AspNetUserRoles;


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
		
	INSERT INTO Colors (ColorTypeId, ColorType)
	VALUES(1, 'Red'),
	(2, 'Orange'),
	(3, 'Yellow'),
	(4, 'Green'),
	(5, 'Blue');


	INSERT INTO Transmissions (TransmissionTypeId, TransmissionType)
	VALUES(1, 'Automatic'),
	(2, 'Manual'),
	(3, 'NA');



	INSERT INTO AspNetRoles (Id, Name)
	VALUES(1, 'Admin'),
	(2, 'Sales'),
	(3, 'Disabled');


	INSERT INTO AspNetUserRoles (UserId, RoleId)
	VALUES('cd24bb86-49f9-4015-bcc1-42a6bf8f7a17', 1);
END