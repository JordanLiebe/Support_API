/****** Object:  StoredProcedure [dbo].[SP_Get_Session]    Script Date: 12/15/2020 9:28:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:      Jordan M. Liebe
-- Create Date: 11-21-2020
-- Description: Function to set verify a Session based off of matching JWT and hashed Code.
-- =============================================
CREATE PROCEDURE [dbo].[SP_Get_Session]
(
    -- Add the parameters for the stored procedure here
    @JWT varchar(500)
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Insert statements for procedure here
    SELECT Id, UUID, JWT, Code, Verified, Created FROM Sessions WHERE JWT = @JWT;
END
GO
