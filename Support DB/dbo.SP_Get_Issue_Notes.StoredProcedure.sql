/****** Object:  StoredProcedure [dbo].[SP_Get_Issue_Notes]    Script Date: 12/15/2020 9:28:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[SP_Get_Issue_Notes]
(
    -- Add the parameters for the stored procedure here
    @IssueId int
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Insert statements for procedure here
    SELECT Id, IssueId, Content, Flag, Author, Created
		FROM Notes
		WHERE IssueId = @IssueId;
END
GO
