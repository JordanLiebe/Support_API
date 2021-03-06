/****** Object:  StoredProcedure [dbo].[SP_Get_Issue]    Script Date: 12/15/2020 9:28:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Jordan M. Liebe
-- Create Date: 11-12-2020
-- Description: Allows retrieval of 1 issue by IssueId (PARAM)
-- =============================================
CREATE PROCEDURE [dbo].[SP_Get_Issue]
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
    SELECT I.Id, I.Subject, I.Priority, I.Category, 
		I.Department, I.Author, I.Status, I.Created, 
		I.Assignee, I.Assigned, N.Id, N.IssueId, 
		N.Content, N.Flag, N.Author, N.Created
		FROM Issues I
		LEFT JOIN Notes N
		ON I.Id = N.IssueId
		WHERE I.Id = @Id;
END
GO
