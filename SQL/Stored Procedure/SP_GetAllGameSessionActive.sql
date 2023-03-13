USE ColorPrediction
GO

/****** Object:  StoredProcedure [dbo].[SP_GetAllGameSessionActive]    Script Date: 6/3/2023 8:41:44 PM ******/
DROP PROCEDURE [dbo].[SP_GetAllGameSessionActive]
GO

/****** Object:  StoredProcedure [dbo].[SP_GetAllGameSessionActive]    Script Date: 6/3/2023 8:41:44 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[SP_GetAllGameSessionActive]
AS
	SET NOCOUNT ON
	
	SELECT ses.*, game.CGAME_NAME
	FROM [dbo].[CVD_GAME_SESSION] ses, [dbo].[CVD_GAME] game
	WHERE game.[CGAME_STATUS] = 1 --game status must be active
	AND ses.CGAME_STATUS = 1 --current session which is active
	AND game.CGAME_ID = ses.CGAME_ID

	RETURN
GO


