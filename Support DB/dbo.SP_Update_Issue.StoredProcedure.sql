/****** Object:  StoredProcedure [dbo].[SP_Update_Issue]    Script Date: 12/15/2020 9:28:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Jordan M. Liebe
-- Create Date: 11-13-2020
-- Description: Simple SPROC to update an Issue Record
-- =============================================
CREATE PROCEDURE [dbo].[SP_Update_Issue]
(
    -- Add the parameters for the stored procedure here
    -- <@Param1, sysname, @p1> <Datatype_For_Param1, , int> = <Default_Value_For_Param1, , 0>,
    -- <@Param2, sysname, @p2> <Datatype_For_Param2, , int> = <Default_Value_For_Param2, , 0>
	@Id int,
	@Subject varchar(300) = null,
	@Priority varchar(25) = null,
	@Category varchar(100) = null,
	@Department varchar(100) = null,
	@Author varchar(150) = null,
	@Status varchar(100) = null
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Insert statements for procedure here
	UPDATE Issues SET Subject = @Subject, Priority = @Priority, Category = @Category, Department = @Department, Author = @Author, Status = @Status WHERE Id = @Id;

	SELECT * FROM Issues WHERE Id = @Id;
END
GO
