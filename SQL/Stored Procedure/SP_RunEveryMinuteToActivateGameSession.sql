USE ColorPrediction
GO

/****** Object:  StoredProcedure [dbo].[SP_RunEveryMinuteToActivateGameSession]    Script Date: 16/12/2022 2:53:29 PM ******/
DROP PROCEDURE [dbo].[SP_RunEveryMinuteToActivateGameSession]
GO

/****** Object:  StoredProcedure [dbo].[SP_RunEveryMinuteToActivateGameSession]    Script Date: 16/12/2022 2:53:29 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[SP_RunEveryMinuteToActivateGameSession]
AS
	SET NOCOUNT ON
	
	DECLARE @utc_date DATETIME = GETUTCDATE()
	DECLARE @gmt_date DATETIME = SWITCHOFFSET(CONVERT(VARCHAR(20),@utc_date,100), '+08:00')
	SET @gmt_date = DATEADD(SECOND, DATEPART(SECOND, @utc_date), @gmt_date)

	DECLARE @startDateTime DATETIME = SUBSTRING(CONVERT(varchar(12), DATEADD(DAY, 0, @gmt_date), 120 ),1,11)+'00:00'
	DECLARE @gameSessionID INT
	DECLARE @gameResult INT

	--First is to deactivate the previous game that already passed
	DECLARE tzCur CURSOR FOR
			SELECT [CGAMESES_ID], [CGAME_RESULT]
			FROM [dbo].[CVD_GAME_SESSION]
			WHERE [CGAME_STATUS] = 1
			AND [CGAME_END] <= @gmt_date
	OPEN tzCur
	FETCH NEXT FROM tzCur INTO @gameSessionID, @gameResult
	WHILE @@FETCH_STATUS = 0
		BEGIN 	

		--if it's Beast game type > the result will be compute on another script run every minutes on 00:00:35 
		--(we stopped people from betting before 30 seconds
		--in case result is -1, we just fire the ComputeBeastResult
		IF @gameResult = -1
		BEGIN
			EXEC SP_ComputeBeastResult
		END

		--compute the winners payment - based on the result on CVD_GAME_SESSION
		EXEC [SP_PayoutWinnersBySessionID] @gameSessionID

		--set the game to complete
		UPDATE [dbo].[CVD_GAME_SESSION]
		SET [CGAME_STATUS] = 2
		WHERE [CGAMESES_ID] = @gameSessionID


	FETCH NEXT FROM tzCur INTO @gameSessionID, @gameResult
	END
	CLOSE tzCur
	DEALLOCATE tzCur

	DECLARE @gameID INT = 0

	--Second is to activate the new session
	DECLARE tzCur1 CURSOR FOR
			SELECT [CGAMESES_ID], [CGAME_ID]
			FROM [dbo].[CVD_GAME_SESSION]
			WHERE @gmt_date >= [CGAME_START]
			AND @gmt_date <= [CGAME_END]
			AND [CGAME_STATUS] = 0
	OPEN tzCur1
	FETCH NEXT FROM tzCur1 INTO @gameSessionID, @gameID
	WHILE @@FETCH_STATUS = 0
		BEGIN 	

		DECLARE @gameType INT = 0

		SELECT @gameType = [CGAME_TYPE]
		FROM [dbo].[CVD_GAME]
		WHERE [CGAME_ID] = @gameID

		--set the game to active
		UPDATE [dbo].[CVD_GAME_SESSION]
		SET [CGAME_STATUS] = 1
		WHERE [CGAMESES_ID] = @gameSessionID

		--if it is beauty game type randomly pick 0-9 number
		IF @gameType = 0
		BEGIN
			UPDATE [dbo].[CVD_GAME_SESSION]
			SET [CGAME_RESULT] = FLOOR(RAND() * 10)
			WHERE [CGAMESES_ID] = @gameSessionID
		END
		

	FETCH NEXT FROM tzCur1 INTO @gameSessionID, @gameID
	END
	CLOSE tzCur1
	DEALLOCATE tzCur1

	RETURN
GO


