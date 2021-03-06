/****** Object:  StoredProcedure [dbo].[SP_Create_User]    Script Date: 12/15/2020 9:28:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Jordan M. Liebe
-- Create Date: 11-16-2020
-- Description: Simple SPROC to create user in Users db.
-- =============================================
CREATE PROCEDURE [dbo].[SP_Create_User]
(
    -- Add the parameters for the stored procedure here
	@UUID varchar(100),
	@Login varchar(200),
	@Hash varchar(500),
	@Email varchar(200),
	@First_Name varchar(150),
	@Middle_Name varchar(150) = null,
	@Last_Name varchar(150)
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Insert statements for procedure here
	DECLARE @Existing int;
	SET @Existing = (SELECT COUNT(*) FROM Users WHERE UUID = @UUID OR Login = @Login);

	IF @Existing = 0
		INSERT INTO Users (UUID, Login, Hash, Email, Created, Status, First_Name, Middle_Name, Last_Name) VALUES(@UUID, @Login, @Hash, @Email, CURRENT_TIMESTAMP, 'Active', @First_Name, @Middle_Name, @Last_Name);
		SELECT UUID, Login, Hash, Email, Created, Status, First_Name, Middle_Name, Last_Name FROM Users Where UUID = @UUID;
END

GO
