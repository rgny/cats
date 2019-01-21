Use YourDatabase
GO

CREATE TABLE dbo.Cat
(
 Id INT IDENTITY(1,1) PRIMARY KEY,
 Name VARCHAR(255) NOT NULL,
 Breed VARCHAR(255) NOT NULL,
 Age TINYINT NOT NULL,
 Is_Active BIT NOT NULL,
 Created_Dt DATETIME NOT NULL,
 Updated_Dt DATETIME NULL
)

GO

--seed data
INSERT INTO dbo.Cat(Name,Breed,Age,Is_Active,Created_Dt)
       SELECT 'Max','Persian',3,1,GETDATE()

INSERT INTO dbo.Cat(Name,Breed,Age,Is_Active,Created_Dt)
       SELECT 'Liane','Seamise',1,1,GETDATE()

