/****** Object:  StoredProcedure [dbo].[SP_Update_Note]    Script Date: 12/15/2020 9:28:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[SP_Update_Note]
(
    -- Add the parameters for the stored procedure here
    -- <@Param1, sysname, @p1> <Datatype_For_Param1, , int> = <Default_Value_For_Param1, , 0>,
    -- <@Param2, sysname, @p2> <Datatype_For_Param2, , int> = <Default_Value_For_Param2, , 0>
	@Id int,
	@IssueId int,
	@Content varchar(500),
	@Flag bit,
	@Author varchar(150)
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Insert statements for procedure here
	UPDATE Notes SET IssueId = @IssueId, Content = @Content, Flag = @Flag, Author = @Author WHERE Id = @Id;

	SELECT * FROM Notes WHERE Id = @Id;
END
GO
