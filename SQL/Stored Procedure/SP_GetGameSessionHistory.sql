USE ColorPrediction
GO

/****** Object:  StoredProcedure [dbo].[SP_GetGameSessionHistory]    Script Date: 6/3/2023 8:41:44 PM ******/
DROP PROCEDURE [dbo].[SP_GetGameSessionHistory]
GO

/****** Object:  StoredProcedure [dbo].[SP_GetGameSessionHistory]    Script Date: 6/3/2023 8:41:44 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[SP_GetGameSessionHistory]
(
	@gameID INT
)
AS
	SET NOCOUNT ON
	
	SELECT TOP 10 * 
	FROM [CVD_GAME_SESSION_DAILY_HISTORY]
	WHERE [CGAME_ID] = @gameID
	ORDER BY [CGAME_RESULTON] DESC

	RETURN
GO


