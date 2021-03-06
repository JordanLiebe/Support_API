/****** Object:  StoredProcedure [dbo].[SP_Delete_Note]    Script Date: 12/15/2020 9:28:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Jordan M. Liebe
-- Create Date: 11-13-2020
-- Description: Simple SPROC to delete a note
-- =============================================
CREATE PROCEDURE [dbo].[SP_Delete_Note]
(
    -- Add the parameters for the stored procedure here
    @Id int
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Insert statements for procedure here
    DELETE FROM Notes WHERE Id = @Id;

	SELECT @@ROWCOUNT as Success;
END
GO
