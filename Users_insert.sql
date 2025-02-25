USE [BlazorApp2]
GO

INSERT INTO [dbo].[Users]
           ([Id]
           ,[Username]
           ,[Password]
           ,[CreatedAt]
           ,[UpdatedAt])
     VALUES
		   (1
           ,'admin'
           ,HASHBYTES('SHA2_256', '1234')
           ,getdate()
           ,getdate()),

		   (2
           ,'ricardo'
           ,HASHBYTES('SHA2_256', '1234')
           ,getdate()
           ,getdate()),
GO


