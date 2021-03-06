/****** Object:  StoredProcedure [dbo].[SP_Create_Issue]    Script Date: 12/15/2020 9:28:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[SP_Create_Issue]
(
    -- Add the parameters for the stored procedure here
    -- <@Param1, sysname, @p1> <Datatype_For_Param1, , int> = <Default_Value_For_Param1, , 0>,
    -- <@Param2, sysname, @p2> <Datatype_For_Param2, , int> = <Default_Value_For_Param2, , 0>
	@Subject varchar(300) = null,
	@Priority varchar(25) = null,
	@Category varchar(100) = null,
	@Department varchar(100) = null,
	@Initial_Note varchar(500) = null,
	@Author varchar(150) = null,
	@Status varchar(100) = null
	
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Insert statements for procedure here
	INSERT INTO Issues (Subject, Priority, Category, Department, Author, Status, Created) VALUES(@Subject, @Priority, @Category, @Department, @Author, @Status, CURRENT_TIMESTAMP);

	DECLARE @Id INT = SCOPE_IDENTITY();

	-- Creating Initial Note --
	INSERT INTO Notes (IssueId, Content, Flag, Author, Created) VALUES(@Id, @Initial_Note, 0, @Author, CURRENT_TIMESTAMP);

	SELECT * FROM Issues WHERE Id = @Id;
END
GO
