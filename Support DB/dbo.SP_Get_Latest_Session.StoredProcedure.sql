/****** Object:  StoredProcedure [dbo].[SP_Get_Latest_Session]    Script Date: 12/15/2020 9:28:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Jordan M. Liebe
-- Create Date: 11-16-2020
-- Description: Get the latest session for a user identified by their UUID
-- =============================================
CREATE PROCEDURE [dbo].[SP_Get_Latest_Session]
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
    SELECT Id, UUID, JWT, Code, Verified, Created from Sessions WHERE UUID = @UUID ORDER BY Created DESC;
END
GO
