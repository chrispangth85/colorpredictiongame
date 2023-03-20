USE ColorPrediction
GO

/****** Object:  StoredProcedure [dbo].[SP_GenerateGameSessionByID]    Script Date: 16/12/2022 2:53:29 PM ******/
DROP PROCEDURE [dbo].[SP_GenerateGameSessionByID]
GO

/****** Object:  StoredProcedure [dbo].[SP_GenerateGameSessionByID]    Script Date: 16/12/2022 2:53:29 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[SP_GenerateGameSessionByID]
(
	@ID INT
)
AS
	SET NOCOUNT ON
	
	DECLARE @utc_date DATETIME = GETUTCDATE()
	DECLARE @gmt_date DATETIME = SWITCHOFFSET(CONVERT(VARCHAR(20),@utc_date,100), '+08:00')
	SET @gmt_date = DATEADD(SECOND, DATEPART(SECOND, @utc_date), @gmt_date)

	DECLARE @startDateTime DATETIME = SUBSTRING(CONVERT(varchar(12), DATEADD(DAY, 0, @gmt_date), 120 ),1,11)+'00:00'
	DECLARE @startOfMonthDateTime DATETIME = DATEADD(month, DATEDIFF(month, 0, @startDateTime), 0)
	DECLARE @endOf7MonthDateTime DATETIME = DATEADD(MONTH, 6, @startOfMonthDateTime)

	DECLARE @durationSec INT
	DECLARE @found INT = 0

	SELECT @durationSec = [CGAME_DURATION_SECONDS], @found = 1
	FROM [dbo].[CVD_GAME]
	WHERE [CGAME_ID] = @ID

	DECLARE @DAYz INT = 0
	DECLARE @counter INT = 1

	IF @found = 0
		RETURN

	--Lets generate 6 month
	WHILE @startDateTime < @endOf7MonthDateTime
	BEGIN
		SET @found = 0

		IF DAY(@startDateTime) <> @DAYz
		BEGIN
			SET @counter = 1

			SET @DAYz = DAY(@startDateTime)

			SELECT @found = 1
			FROM [dbo].[CVD_GAME_SESSION]
			WHERE [CGAME_ID] = @ID
			AND [CGAME_START] = @startDateTime

			--If already contain the session on that day 00:00, skip the whole day
			IF @found = 1
			BEGIN
				SET @startDateTime = DATEADD(DAY, 1, @startDateTime)
			END
		END

		IF @found = 0
		BEGIN
			DECLARE @period NVARCHAR(200) = convert(varchar, @startDateTime, 112) + RIGHT('0000' + CAST(@counter AS VARCHAR(4)), 4)

			--Just some checking whenever day changed (00:00) we check if there exists this start date
			INSERT INTO [dbo].[CVD_GAME_SESSION] ([CGAME_ID], [CGAME_PERIOD], [CGAME_RESULT], [CGAME_START], [CGAME_END], [CGAME_STATUS], CGAME_TOTAL_BET, CGAME_TOTAL_WIN, [CGAMESES_DELETIONSTATE])
			VALUES (@ID, @period, -1, @startDateTime, DATEADD(SECOND, @durationSec, @startDateTime), 0, 0, 0, 0)

			SET @counter = @counter + 1
			SET @startDateTime = DATEADD(SECOND, @durationSec, @startDateTime)
		END
		
	END


	RETURN
GO


