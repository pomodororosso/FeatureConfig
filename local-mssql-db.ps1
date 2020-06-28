param (
  [string]$database = "FeatureConfig",
  [string]$server = ".",
  [string]$password = "SecretPassword123"
)

docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=$password" -p 1433:1433 --name sqlFeature -d mcr.microsoft.com/mssql/server:2019-latest
Start-Sleep -Second 20
sqlcmd -S $server -U sa -P $password -Q "

CREATE DATABASE [$database]

GO
USE [$database]
GO

CREATE TABLE [dbo].[FeatureValue](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Value] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_FeatureValue] PRIMARY KEY CLUSTERED 
	(
	[ID] ASC
	)
)

INSERT [dbo].[FeatureValue] ([Name],[Value]) VALUES (N'NewCardPsp', N'Adyen')
GO
INSERT [dbo].[FeatureValue] ([Name],[Value]) VALUES (N'SavedCardEnabled', N'True')
GO
INSERT [dbo].[FeatureValue] ([Name],[Value]) VALUES (N'PayPalEnabled', N'False')
GO
INSERT [dbo].[FeatureValue] ([Name],[Value]) VALUES (N'FraudEnabled', N'True')
GO
INSERT [dbo].[FeatureValue] ([Name],[Value]) VALUES (N'CashOptionEnabled', N'True')
GO
INSERT [dbo].[FeatureValue] ([Name],[Value]) VALUES (N'3DSEnabled', N'True')
GO


CREATE PROCEDURE [dbo].[SP_Add_FeatureValue]    
    @Name NVARCHAR(250),
    @Value NVARCHAR(250)  
AS    
    BEGIN    
 DECLARE @Id as BIGINT  
        INSERT  INTO [FeatureValue]    
                (Name, Value)    
        VALUES  (@Name, @Value);   
		SET @Id = SCOPE_IDENTITY();   
        SELECT  @Id AS FeatureValueID;    
    END;   
GO	
	
CREATE PROCEDURE [dbo].[SP_Update_FeatureValue] 
		@Id INT,   
		@Name NVARCHAR(250),
        @Value NVARCHAR(250)    
	AS    
		BEGIN    

		UPDATE [FeatureValue] SET [Name] = @Name, [Value] = @Value WHERE ID = @Id 
	           
		END;   
"