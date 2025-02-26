﻿CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL IDENTITY PRIMARY KEY,
    [Username] VARCHAR(32) NOT NULL,
    [Password] BINARY(112) NOT NULL,
    [CreatedAt] DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(),
    [UpdatedAt] DATETIMEOFFSET NULL,

    CONSTRAINT [AK_Username] UNIQUE([Username])
)

GO

CREATE TRIGGER [dbo].[TRG_UpdateDatetime_Users] ON [dbo].[Users]
AFTER INSERT, UPDATE
AS
  UPDATE [dbo].[Users]
  SET [UpdatedAt] = SYSDATETIMEOFFSET()
  FROM [INSERTED]