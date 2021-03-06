/****** Object:  StoredProcedure [dbo].[SP_Get_Notes]    Script Date: 12/15/2020 9:28:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Jordan M. Liebe
-- Create Date: 11-12-2020
-- Description: Get all notes
-- =============================================
CREATE PROCEDURE [dbo].[SP_Get_Notes]
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

	SELECT Id, IssueId, Flag, Author, Created FROM Notes;
END
GO
