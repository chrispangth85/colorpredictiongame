USE ColorPrediction
GO

/****** Object:  StoredProcedure [dbo].[SP_GetGameByID]    Script Date: 16/12/2022 2:53:29 PM ******/
DROP PROCEDURE [dbo].[SP_GetGameByID]
GO

/****** Object:  StoredProcedure [dbo].[SP_GetGameByID]    Script Date: 16/12/2022 2:53:29 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[SP_GetGameByID]
(
	@ID INT,
	@ok int output,
	@msg varchar(50) output
)
AS
	SET NOCOUNT ON
	set @ok = 0

	SELECT *
	FROM [dbo].[CVD_GAME]
	WHERE CGAME_ID = @ID

	set @ok = 1
	set @msg = 'Success'

	RETURN
GO


