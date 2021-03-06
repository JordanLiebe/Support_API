/****** Object:  StoredProcedure [dbo].[SP_Create_Session]    Script Date: 12/15/2020 9:28:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Jordan M. Liebe
-- Create Date: 11-16-2020
-- Description: Function to create a new session.
-- =============================================
CREATE PROCEDURE [dbo].[SP_Create_Session]
(
    -- Add the parameters for the stored procedure here
    @UUID varchar(100),
	@JWT varchar(500),
	@CODE varchar(500)
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Insert statements for procedure here
	INSERT INTO Sessions (UUID, JWT, Code, Verified, Created) VALUES(@UUID, @JWT, @CODE, 0, CURRENT_TIMESTAMP);

	DECLARE @Id INT = SCOPE_IDENTITY();

	SELECT Id, UUID, JWT, Code, Verified, Created from Sessions WHERE Id = @Id;
END
GO
