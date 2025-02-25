CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Username] VARCHAR(32) NOT NULL, 
    [Password] BINARY(64) NOT NULL, 
    [CreatedAt] DATETIME NULL, 
    [UpdatedAt] DATETIME NULL,

    CONSTRAINT AK_Username UNIQUE(Username)
)
