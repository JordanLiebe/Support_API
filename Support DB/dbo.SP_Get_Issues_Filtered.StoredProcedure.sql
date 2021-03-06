/****** Object:  StoredProcedure [dbo].[SP_Get_Issues_Filtered]    Script Date: 12/15/2020 9:28:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Get_Issues_Filtered]
	-- Add the parameters for the stored procedure here
	-- <@Param1, sysname, @p1> <Datatype_For_Param1, , int> = <Default_Value_For_Param1, , 0>, 
	-- <@Param2, sysname, @p2> <Datatype_For_Param2, , int> = <Default_Value_For_Param2, , 0>
	@Id int = null,
	@Subject varchar(300) = null,
	@Priority varchar(25) = null,
	@Category varchar(100) = null,
	@Department varchar(100) = null,
	@Status varchar(100) = null,
	@Author varchar(150) = null,
	@Assignee varchar(150) = null
AS
	BEGIN
		-- SET NOCOUNT ON added to prevent extra result sets from
		-- interfering with SELECT statements.
		SET NOCOUNT ON;

		-- Insert statements for procedure here
		SELECT I.Id, I.Subject, I.Priority, I.Category, 
			I.Department, CONCAT(IU.First_Name, ' ', IU.Middle_Name, ' ', IU.Last_Name) as Author, I.Status, I.Created, 
			I.Assignee, I.Assigned, N.Id, N.IssueId, 
			N.Content, N.Flag, CONCAT(NU.First_Name, ' ', NU.Middle_Name, ' ', NU.Last_Name) as Author, N.Created
			FROM Issues I
			LEFT JOIN Users IU
			ON I.Author = IU.UUID
			LEFT JOIN Notes N
			ON I.Id = N.IssueId
			LEFT JOIN Users NU
			ON N.Author = NU.UUID
			WHERE
					(@Id IS NULL OR (I.Id = @Id))
				AND	(@Subject IS NULL OR (I.Subject LIKE '%' + @Subject + '%'))
				AND (@Priority IS NULL OR (I.Priority = @Priority))
				AND (@Category IS NULL OR (I.Category = @Category))
				AND (@Department IS NULL OR (I.Department = @Department))
				AND (@Status IS NULL OR (I.Status = @Status))
				AND (@Author IS NULL OR (I.Author LIKE '%' + @Author + '%'))
				AND (@Assignee IS NULL OR (I.Assignee LIKE '%' + @Assignee + '%'))
			OPTION (RECOMPILE);
	END
GO
