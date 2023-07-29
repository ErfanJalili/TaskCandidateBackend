USE [overtimedb]
GO

/****** Object:  StoredProcedure [appdbuser].[GetAllPersonSP]    Script Date: 7/29/2023 8:38:04 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [appdbuser].[GetAllPersonSP] 
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM appdbuser.Person
END
GO


