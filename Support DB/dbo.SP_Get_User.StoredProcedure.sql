/****** Object:  StoredProcedure [dbo].[SP_Get_User]    Script Date: 12/15/2020 9:28:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Jordan M. Liebe
-- Create Date: 11-16-2020
-- Description: Simple Function to retrieve a user by login.
-- =============================================
CREATE PROCEDURE [dbo].[SP_Get_User]
(
    -- Add the parameters for the stored procedure here
    @Login varchar(150) = '',
	@UUID varchar(500) = ''
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Insert statements for procedure here
    SELECT UUID, Login, Hash, Email, Created, Status, First_Name, Middle_Name, Last_Name FROM Users Where Login = @Login OR UUID = @UUID COLLATE Latin1_General_CS_AS;
END
GO
