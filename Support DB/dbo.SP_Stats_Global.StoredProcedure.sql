/****** Object:  StoredProcedure [dbo].[SP_Stats_Global]    Script Date: 12/15/2020 9:28:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[SP_Stats_Global]
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Insert statements for procedure here
    DECLARE @StatTable TABLE (
	  Id int,
	  Name varchar(100),
	  Value int
	)

	INSERT INTO @StatTable 
		(Id, Name, Value) 
		SELECT 
			1 AS Id, 
			'Total Issues' AS Name, 
			COUNT(*) AS Value
		FROM Issues;

	INSERT INTO @StatTable 
		(Id, Name, Value) 
		SELECT 
			2 AS Id, 
			'Total Notes' AS Name, 
			COUNT(*) AS Value
		FROM Notes;

	INSERT INTO @StatTable 
		(Id, Name, Value) 
		SELECT 
			3 AS Id, 
			'Total Users' AS Name, 
			COUNT(*) AS Value
		FROM Users;

	SELECT Id, Name, Value FROM @StatTable;
END
GO
