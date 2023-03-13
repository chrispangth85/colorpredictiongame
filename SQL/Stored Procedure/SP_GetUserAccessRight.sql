USE ColorPrediction
GO

/****** Object:  StoredProcedure [dbo].[SP_GetUserAccessRight]    Script Date: 6/3/2023 8:08:57 PM ******/
DROP PROCEDURE [dbo].[SP_GetUserAccessRight]
GO

/****** Object:  StoredProcedure [dbo].[SP_GetUserAccessRight]    Script Date: 6/3/2023 8:08:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SP_GetUserAccessRight]
(
	@Username nvarchar(50),
	@ok int output,
	@msg varchar(50) output
)
AS
	SET NOCOUNT ON
	SET @ok = 0

	DECLARE @AccessRight NVARCHAR(MAX)
	DECLARE @IsExists BIT = 0
	
	SELECT @IsExists = 1
	FROM CVD_USER_ACCESSRIGHT
	WHERE CUSR_USERNAME = @Username
	AND CUAR_DELETIONSTATE = 0
	
	IF @IsExists = 0
	BEGIN
		SET @ok = -1
	SET @msg = 'Newly created user'
	END
	
	SELECT @AccessRight = COALESCE(@AccessRight + ', ', '') + CAR_CODE
	FROM CVD_USER u
	INNER JOIN CVD_USER_ACCESSRIGHT uac ON uac.CUSR_USERNAME = u.CUSR_USERNAME
	INNER JOIN CVD_ACCESSRIGHT ac ON ac.CAR_ID = uac.CAR_ID
	INNER JOIN CVD_MODULE m ON m.CMDL_ID = ac.CMDL_ID
	WHERE u.CUSR_USERNAME = @Username
	AND u.CUSR_DELETIONSTATE = 0 AND uac.CUAR_DELETIONSTATE = 0
	AND ac.CAR_DELETIONSTATE = 0 AND m.CMDL_DELETIONSTATE = 0
	ORDER BY m.CMDL_MAIN_MODULE_SEQ, m.CMDL_NAME_SEQ, ac.CAR_SEQ

	SELECT @AccessRight

	SET @ok = 1
	SET @msg = 'Success'

	RETURN
GO


