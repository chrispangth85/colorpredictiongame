USE ColorPrediction
GO

/****** Object:  StoredProcedure [dbo].[SP_GetBetHistory]    Script Date: 6/3/2023 8:41:44 PM ******/
DROP PROCEDURE [dbo].[SP_GetBetHistory]
GO

/****** Object:  StoredProcedure [dbo].[SP_GetBetHistory]    Script Date: 6/3/2023 8:41:44 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[SP_GetBetHistory]
(
	@gameID INT,
	@username NVARCHAR(200)
)
AS
	SET NOCOUNT ON
	
	SELECT * 
	FROM [dbo].[CVD_GAME_SESSION_BET]
	WHERE [CUSR_USERNAME] = @username
	AND [CGAME_ID] = @gameID
	ORDER BY [CBET_ID] DESC

	RETURN
GO


