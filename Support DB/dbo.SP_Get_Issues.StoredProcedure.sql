/****** Object:  StoredProcedure [dbo].[SP_Get_Issues]    Script Date: 12/15/2020 9:28:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Get_Issues]
	-- Add the parameters for the stored procedure here
	-- <@Param1, sysname, @p1> <Datatype_For_Param1, , int> = <Default_Value_For_Param1, , 0>, 
	-- <@Param2, sysname, @p2> <Datatype_For_Param2, , int> = <Default_Value_For_Param2, , 0>
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT I.Id, I.Subject, I.Priority, I.Category, 
		I.Department, I.Author, I.Status, I.Created, 
		I.Assignee, I.Assigned, N.Id, N.IssueId, 
		N.Content, N.Flag, N.Author, N.Created
		FROM Issues I
		LEFT JOIN Notes N
		ON I.Id = N.IssueId
		WHERE 1 = 1;
END
GO
