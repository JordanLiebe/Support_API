/****** Object:  StoredProcedure [dbo].[SP_Get_User_By_UUID]    Script Date: 12/15/2020 9:28:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Jordan M. Liebe
-- Create Date: 11-16-2020
-- Description: Simple function to get a user by UUID.
-- =============================================
CREATE PROCEDURE [dbo].[SP_Get_User_By_UUID]
(
    -- Add the parameters for the stored procedure here
    @UUID varchar(100)
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Insert statements for procedure here
	SELECT UUID, Login, Hash, Email, Status, Created, First_Name, Middle_Name, Last_Name FROM Users WHERE UUID = @UUID;
END
GO
