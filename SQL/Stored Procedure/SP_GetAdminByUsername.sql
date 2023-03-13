USE Commitz
GO

/****** Object:  StoredProcedure [dbo].[SP_GetAdminByUsername]    Script Date: 16/12/2022 2:53:29 PM ******/
DROP PROCEDURE [dbo].[SP_GetAdminByUsername]
GO

/****** Object:  StoredProcedure [dbo].[SP_GetAdminByUsername]    Script Date: 16/12/2022 2:53:29 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[SP_GetAdminByUsername]
(
	@Username varchar(50),
	@ok int output,
	@msg varchar(50) output
)
AS
	SET NOCOUNT ON
	set @ok = 0

	SELECT *
	FROM dbo.CVD_USER usr
	WHERE CROLE_ID = 1
	AND CUSR_DELETIONSTATE = 0 AND CUSR_USERNAME = @Username

	set @ok = 1
	set @msg = 'Success'

	RETURN
GO


