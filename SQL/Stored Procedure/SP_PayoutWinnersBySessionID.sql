USE ColorPrediction
GO

/****** Object:  StoredProcedure [dbo].[SP_PayoutWinnersBySessionID]    Script Date: 16/12/2022 2:53:29 PM ******/
DROP PROCEDURE [dbo].[SP_PayoutWinnersBySessionID]
GO

/****** Object:  StoredProcedure [dbo].[SP_PayoutWinnersBySessionID]    Script Date: 16/12/2022 2:53:29 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[SP_PayoutWinnersBySessionID]
(
	@SessionID INT
)
AS
	SET NOCOUNT ON
	
	DECLARE @utc_date DATETIME = GETUTCDATE()
	DECLARE @gmt_date DATETIME = SWITCHOFFSET(CONVERT(VARCHAR(20),@utc_date,100), '+08:00')
	SET @gmt_date = DATEADD(SECOND, DATEPART(SECOND, @utc_date), @gmt_date)
	DECLARE @commission DECIMAL(15,2) = 0
	DECLARE @gameSessionStartDT DATETIME
	DECLARE @totalPayout DECIMAL(15,2) = 0
	DECLARE @gameID INT = 0
	DECLARE @period NVARCHAR(200) = ''

	SELECT @commission = [CPARA_DECIMALVALUE]
	FROM [dbo].[CVD_PARAMETER]
	WHERE [CPARA_NAME] = 'BetCommission'

	--Check if this session status = 1(Already started and not yet complete) + EndTime <= Now
	DECLARE @exists INT = 0
	DECLARE @winningNumberMain INT = -1
	DECLARE @winningNumber2 INT = -1
	DECLARE @winningNumber3 INT = -1

	SELECT @exists = 1, @winningNumberMain = [CGAME_RESULT], 
	@gameSessionStartDT = CGAME_START, @gameID = [CGAME_ID],
	@period = [CGAME_PERIOD]
	FROM [dbo].[CVD_GAME_SESSION]
	WHERE [CGAMESES_ID] = @SessionID
	AND [CGAME_STATUS] = 1
	AND [CGAME_END] <= @gmt_date

	IF @exists = 0 OR @winningNumberMain = -1
	BEGIN
		RETURN
	END

	--55 Green
	--66 Violet
	--77 Red
	IF @winningNumberMain = 0
	BEGIN
		SET @winningNumber2 = 66--Violet
		SET @winningNumber3 = 77--Red
	END
	ELSE IF @winningNumberMain = 1
	BEGIN
		SET @winningNumber2 = 55--Green
	END
	ELSE IF @winningNumberMain = 2
	BEGIN
		SET @winningNumber2 = 77--Red
	END
	ELSE IF @winningNumberMain = 3
	BEGIN
		SET @winningNumber2 = 55--Green
	END
	ELSE IF @winningNumberMain = 4
	BEGIN
		SET @winningNumber2 = 77--Red
	END
	ELSE IF @winningNumberMain = 5
	BEGIN
		SET @winningNumber2 = 66--Violet
		SET @winningNumber3 = 55--Green
	END
	ELSE IF @winningNumberMain = 6
	BEGIN
		SET @winningNumber2 = 77--Red
	END
	ELSE IF @winningNumberMain = 7
	BEGIN
		SET @winningNumber2 = 55--Green
	END
	ELSE IF @winningNumberMain = 8
	BEGIN
		SET @winningNumber2 = 77--Red
	END
	ELSE IF @winningNumberMain = 9
	BEGIN
		SET @winningNumber2 = 55--Green
	END

	DECLARE @totalBetReturn0 DECIMAL(15,2) = 0, @totalBetReturn1 DECIMAL(15,2) = 0, @totalBetReturn2 DECIMAL(15,2) = 0, @totalBetReturn3 DECIMAL(15,2) = 0,
	@totalBetReturn4 DECIMAL(15,2) = 0, @totalBetReturn5 DECIMAL(15,2) = 0, @totalBetReturn6 DECIMAL(15,2) = 0, @totalBetReturn7 DECIMAL(15,2) = 0, 
	@totalBetReturn8 DECIMAL(15,2) = 0, @totalBetReturn9 DECIMAL(15,2) = 0, @totalBetReturnGreen DECIMAL(15,2) = 0, 
	@totalBetReturnViolet DECIMAL(15,2) = 0, @totalBetReturnRed DECIMAL(15,2) = 0, @totalBetReturnRedZero DECIMAL(15,2) = 0,
	@totalBetReturnGreenFive DECIMAL(15,2) = 0

	SET @exists = 0

	SELECT @exists = 1,
	@totalBetReturn0 = [CGAME_NO0_RETURN], @totalBetReturn1 = [CGAME_NO1_RETURN], @totalBetReturn2 = [CGAME_NO2_RETURN], 
	@totalBetReturn3 = [CGAME_NO3_RETURN], @totalBetReturn4 = [CGAME_NO4_RETURN], @totalBetReturn5 = [CGAME_NO5_RETURN],
	@totalBetReturn6 = [CGAME_NO6_RETURN], @totalBetReturn7 = [CGAME_NO7_RETURN], @totalBetReturn8 = [CGAME_NO8_RETURN],
	@totalBetReturn9 = [CGAME_NO9_RETURN], @totalBetReturnGreen = [CGAME_GREEN_RETURN], @totalBetReturnViolet = [CGAME_VIOLET_RETURN],
	@totalBetReturnRed = [CGAME_RED_RETURN], @totalBetReturnRedZero = CGAME_RED0_RETURN, @totalBetReturnGreenFive = CGAME_GREEN5_RETURN
	FROM [dbo].[CVD_GAME]

	DECLARE @betID INT = 0
	DECLARE @username NVARCHAR(200)
	DECLARE @number INT
	DECLARE @amount DECIMAL(15,2)

	DECLARE tzCur20 CURSOR FOR
			SELECT [CBET_ID], [CUSR_USERNAME], [CGAME_NUMBER], [CGAME_AMOUNT]
			FROM [dbo].[CVD_GAME_SESSION_BET]
			WHERE [CGAMESES_ID] = @SessionID
			AND [CGAME_NUMBER] IN (@winningNumberMain, @winningNumber2, @winningNumber3)

	OPEN tzCur20
	FETCH NEXT FROM tzCur20 INTO @betID, @username, @number, @amount
	WHILE @@FETCH_STATUS = 0
		BEGIN 	

		DECLARE @betAmount DECIMAL(15,2) = @amount * (100 - @commission) / 100
		DECLARE @winAmount DECIMAL(15,2) = 0

		IF @winningNumberMain = 0
		BEGIN
			IF @number = 0
			BEGIN
				SET @winAmount = @betAmount * @totalBetReturn0
				EXEC SP_CashWalletOperation @username, 'WIN_BET', @winAmount, @number, @betAmount, @totalBetReturn0, @SessionID

				UPDATE [dbo].[CVD_GAME_SESSION_BET]
				SET [CGAME_WIN_AMOUNT] = @winAmount
				WHERE [CGAMESES_ID] = @SessionID
				AND [CGAME_NUMBER] = 0

				SET @totalPayout = @totalPayout + @winAmount
			END
			ELSE IF @number = 66
			BEGIN
				SET @winAmount = @betAmount * @totalBetReturnViolet
				EXEC SP_CashWalletOperation @username, 'WIN_BET', @winAmount, @number, @betAmount, @totalBetReturnViolet, @SessionID

				UPDATE [dbo].[CVD_GAME_SESSION_BET]
				SET [CGAME_WIN_AMOUNT] = @winAmount
				WHERE [CGAMESES_ID] = @SessionID
				AND [CGAME_NUMBER] = 66

				SET @totalPayout = @totalPayout + @winAmount
			END
			ELSE IF @number = 77
			BEGIN
				SET @winAmount = @betAmount * @totalBetReturnRedZero
				EXEC SP_CashWalletOperation @username, 'WIN_BET', @winAmount, @number, @betAmount, @totalBetReturnRedZero, @SessionID

				UPDATE [dbo].[CVD_GAME_SESSION_BET]
				SET [CGAME_WIN_AMOUNT] = @winAmount
				WHERE [CGAMESES_ID] = @SessionID
				AND [CGAME_NUMBER] = 77

				SET @totalPayout = @totalPayout + @winAmount
			END
		END
		ELSE IF @winningNumberMain = 1
		BEGIN
			IF @number = 1
			BEGIN
				SET @winAmount = @betAmount * @totalBetReturn1
				EXEC SP_CashWalletOperation @username, 'WIN_BET', @winAmount, @number, @betAmount, @totalBetReturn1, @SessionID

				UPDATE [dbo].[CVD_GAME_SESSION_BET]
				SET [CGAME_WIN_AMOUNT] = @winAmount
				WHERE [CGAMESES_ID] = @SessionID
				AND [CGAME_NUMBER] = 1

				SET @totalPayout = @totalPayout + @winAmount
			END
			ELSE IF @number = 55
			BEGIN
				SET @winAmount = @betAmount * @totalBetReturnGreen
				EXEC SP_CashWalletOperation @username, 'WIN_BET', @winAmount, @number, @betAmount, @totalBetReturnGreen, @SessionID

				UPDATE [dbo].[CVD_GAME_SESSION_BET]
				SET [CGAME_WIN_AMOUNT] = @winAmount
				WHERE [CGAMESES_ID] = @SessionID
				AND [CGAME_NUMBER] = 55

				SET @totalPayout = @totalPayout + @winAmount
			END
		END
		ELSE IF @winningNumberMain = 2
		BEGIN
			IF @number = 2
			BEGIN
				SET @winAmount = @betAmount * @totalBetReturn8
				EXEC SP_CashWalletOperation @username, 'WIN_BET', @winAmount, @number, @betAmount, @totalBetReturn8, @SessionID

				UPDATE [dbo].[CVD_GAME_SESSION_BET]
				SET [CGAME_WIN_AMOUNT] = @winAmount
				WHERE [CGAMESES_ID] = @SessionID
				AND [CGAME_NUMBER] = 2

				SET @totalPayout = @totalPayout + @winAmount
			END
			ELSE IF @number = 77
			BEGIN
				SET @winAmount = @betAmount * @totalBetReturnRed
				EXEC SP_CashWalletOperation @username, 'WIN_BET', @winAmount, @number, @betAmount, @totalBetReturnRed, @SessionID

				UPDATE [dbo].[CVD_GAME_SESSION_BET]
				SET [CGAME_WIN_AMOUNT] = @winAmount
				WHERE [CGAMESES_ID] = @SessionID
				AND [CGAME_NUMBER] = 77

				SET @totalPayout = @totalPayout + @winAmount
			END
		END
		ELSE IF @winningNumberMain = 3
		BEGIN
			IF @number = 3
			BEGIN
				SET @winAmount = @betAmount * @totalBetReturn3
				EXEC SP_CashWalletOperation @username, 'WIN_BET', @winAmount, @number, @betAmount, @totalBetReturn3, @SessionID

				UPDATE [dbo].[CVD_GAME_SESSION_BET]
				SET [CGAME_WIN_AMOUNT] = @winAmount
				WHERE [CGAMESES_ID] = @SessionID
				AND [CGAME_NUMBER] = 3

				SET @totalPayout = @totalPayout + @winAmount
			END
			ELSE IF @number = 55
			BEGIN
				SET @winAmount = @betAmount * @totalBetReturnGreen
				EXEC SP_CashWalletOperation @username, 'WIN_BET', @winAmount, @number, @betAmount, @totalBetReturnGreen, @SessionID

				UPDATE [dbo].[CVD_GAME_SESSION_BET]
				SET [CGAME_WIN_AMOUNT] = @winAmount
				WHERE [CGAMESES_ID] = @SessionID
				AND [CGAME_NUMBER] = 55

				SET @totalPayout = @totalPayout + @winAmount
			END
		END
		ELSE IF @winningNumberMain = 4
		BEGIN
			IF @number = 4
			BEGIN
				SET @winAmount = @betAmount * @totalBetReturn8
				EXEC SP_CashWalletOperation @username, 'WIN_BET', @winAmount, @number, @betAmount, @totalBetReturn8, @SessionID

				UPDATE [dbo].[CVD_GAME_SESSION_BET]
				SET [CGAME_WIN_AMOUNT] = @winAmount
				WHERE [CGAMESES_ID] = @SessionID
				AND [CGAME_NUMBER] = 4

				SET @totalPayout = @totalPayout + @winAmount
			END
			ELSE IF @number = 77
			BEGIN
				SET @winAmount = @betAmount * @totalBetReturnRed
				EXEC SP_CashWalletOperation @username, 'WIN_BET', @winAmount, @number, @betAmount, @totalBetReturnRed, @SessionID

				UPDATE [dbo].[CVD_GAME_SESSION_BET]
				SET [CGAME_WIN_AMOUNT] = @winAmount
				WHERE [CGAMESES_ID] = @SessionID
				AND [CGAME_NUMBER] = 77

				SET @totalPayout = @totalPayout + @winAmount
			END
		END
		ELSE IF @winningNumberMain = 5
		BEGIN
			IF @number = 5
			BEGIN
				SET @winAmount = @betAmount * @totalBetReturn5
				EXEC SP_CashWalletOperation @username, 'WIN_BET', @winAmount, @number, @betAmount, @totalBetReturn5, @SessionID

				UPDATE [dbo].[CVD_GAME_SESSION_BET]
				SET [CGAME_WIN_AMOUNT] = @winAmount
				WHERE [CGAMESES_ID] = @SessionID
				AND [CGAME_NUMBER] = 5

				SET @totalPayout = @totalPayout + @winAmount
			END
			ELSE IF @number = 66
			BEGIN
				SET @winAmount = @betAmount * @totalBetReturnViolet
				EXEC SP_CashWalletOperation @username, 'WIN_BET', @winAmount, @number, @betAmount, @totalBetReturnViolet, @SessionID

				UPDATE [dbo].[CVD_GAME_SESSION_BET]
				SET [CGAME_WIN_AMOUNT] = @winAmount
				WHERE [CGAMESES_ID] = @SessionID
				AND [CGAME_NUMBER] = 66

				SET @totalPayout = @totalPayout + @winAmount
			END
			ELSE IF @number = 55
			BEGIN
				SET @winAmount = @betAmount * @totalBetReturnGreenFive
				EXEC SP_CashWalletOperation @username, 'WIN_BET', @winAmount, @number, @betAmount, @totalBetReturnGreenFive, @SessionID

				UPDATE [dbo].[CVD_GAME_SESSION_BET]
				SET [CGAME_WIN_AMOUNT] = @winAmount
				WHERE [CGAMESES_ID] = @SessionID
				AND [CGAME_NUMBER] = 55

				SET @totalPayout = @totalPayout + @winAmount
			END
		END
		ELSE IF @winningNumberMain = 6
		BEGIN
			IF @number = 6
			BEGIN
				SET @winAmount = @betAmount * @totalBetReturn8
				EXEC SP_CashWalletOperation @username, 'WIN_BET', @winAmount, @number, @betAmount, @totalBetReturn8, @SessionID

				UPDATE [dbo].[CVD_GAME_SESSION_BET]
				SET [CGAME_WIN_AMOUNT] = @winAmount
				WHERE [CGAMESES_ID] = @SessionID
				AND [CGAME_NUMBER] = 6

				SET @totalPayout = @totalPayout + @winAmount
			END
			ELSE IF @number = 77
			BEGIN
				SET @winAmount = @betAmount * @totalBetReturnRed
				EXEC SP_CashWalletOperation @username, 'WIN_BET', @winAmount, @number, @betAmount, @totalBetReturnRed, @SessionID

				UPDATE [dbo].[CVD_GAME_SESSION_BET]
				SET [CGAME_WIN_AMOUNT] = @winAmount
				WHERE [CGAMESES_ID] = @SessionID
				AND [CGAME_NUMBER] = 77

				SET @totalPayout = @totalPayout + @winAmount
			END
		END
		ELSE IF @winningNumberMain = 7
		BEGIN
			IF @number = 7
			BEGIN
				SET @winAmount = @betAmount * @totalBetReturn7
				EXEC SP_CashWalletOperation @username, 'WIN_BET', @winAmount, @number, @betAmount, @totalBetReturn7, @SessionID

				UPDATE [dbo].[CVD_GAME_SESSION_BET]
				SET [CGAME_WIN_AMOUNT] = @winAmount
				WHERE [CGAMESES_ID] = @SessionID
				AND [CGAME_NUMBER] = 7

				SET @totalPayout = @totalPayout + @winAmount
			END
			ELSE IF @number = 55
			BEGIN
				SET @winAmount = @betAmount * @totalBetReturnGreen
				EXEC SP_CashWalletOperation @username, 'WIN_BET', @winAmount, @number, @betAmount, @totalBetReturnGreen, @SessionID

				UPDATE [dbo].[CVD_GAME_SESSION_BET]
				SET [CGAME_WIN_AMOUNT] = @winAmount
				WHERE [CGAMESES_ID] = @SessionID
				AND [CGAME_NUMBER] = 55

				SET @totalPayout = @totalPayout + @winAmount
			END
		END
		ELSE IF @winningNumberMain = 8
		BEGIN
			IF @number = 8
			BEGIN
				SET @winAmount = @betAmount * @totalBetReturn8
				EXEC SP_CashWalletOperation @username, 'WIN_BET', @winAmount, @number, @betAmount, @totalBetReturn8, @SessionID

				UPDATE [dbo].[CVD_GAME_SESSION_BET]
				SET [CGAME_WIN_AMOUNT] = @winAmount
				WHERE [CGAMESES_ID] = @SessionID
				AND [CGAME_NUMBER] = 8

				SET @totalPayout = @totalPayout + @winAmount
			END
			ELSE IF @number = 77
			BEGIN
				SET @winAmount = @betAmount * @totalBetReturnRed
				EXEC SP_CashWalletOperation @username, 'WIN_BET', @winAmount, @number, @betAmount, @totalBetReturnRed, @SessionID

				UPDATE [dbo].[CVD_GAME_SESSION_BET]
				SET [CGAME_WIN_AMOUNT] = @winAmount
				WHERE [CGAMESES_ID] = @SessionID
				AND [CGAME_NUMBER] = 77

				SET @totalPayout = @totalPayout + @winAmount
			END
		END
		ELSE IF @winningNumberMain = 9
		BEGIN
			IF @number = 9
			BEGIN
				SET @winAmount = @betAmount * @totalBetReturn9
				EXEC SP_CashWalletOperation @username, 'WIN_BET', @winAmount, @number, @betAmount, @totalBetReturn9, @SessionID

				UPDATE [dbo].[CVD_GAME_SESSION_BET]
				SET [CGAME_WIN_AMOUNT] = @winAmount
				WHERE [CGAMESES_ID] = @SessionID
				AND [CGAME_NUMBER] = 9

				SET @totalPayout = @totalPayout + @winAmount
			END
			ELSE IF @number = 55
			BEGIN
				SET @winAmount = @betAmount * @totalBetReturnGreen
				EXEC SP_CashWalletOperation @username, 'WIN_BET', @winAmount, @number, @betAmount, @totalBetReturnGreen, @SessionID

				UPDATE [dbo].[CVD_GAME_SESSION_BET]
				SET [CGAME_WIN_AMOUNT] = @winAmount
				WHERE [CGAMESES_ID] = @SessionID
				AND [CGAME_NUMBER] = 55

				SET @totalPayout = @totalPayout + @winAmount
			END
		END
		

	FETCH NEXT FROM tzCur20 INTO @betID, @username, @number, @amount
	END
	CLOSE tzCur20
	DEALLOCATE tzCur20

	--To store the result history
	DECLARE @totalBet DECIMAL(15,2) = 0

	SELECT @totalBet = ISNULL(SUM([CGAME_AMOUNT]), 0)
	FROM [dbo].[CVD_GAME_SESSION_BET]
	WHERE [CGAMESES_ID] = @SessionID

	DECLARE @startDateTime DATETIME = SUBSTRING(CONVERT(varchar(12), DATEADD(DAY, 0, @gameSessionStartDT), 120 ),1,11)+'00:00'--make it the hour is 00:00

	SET @exists = 0

	SELECT @exists = 1
	FROM [dbo].[CVD_GAME_SESSION_DAILY_HISTORY]
	WHERE [CGAME_ID] = @gameID
	AND [CGAME_RESULTON] = @startDateTime

	DECLARE @resultz NVARCHAR(200) = @period + '_' + convert(nvarchar(255), @totalBet) + '_' + convert(nvarchar(255), @totalPayout) + '_' + convert(nvarchar(255), @winningNumberMain) + ';'

	IF @exists = 1
	BEGIN
		UPDATE [dbo].[CVD_GAME_SESSION_DAILY_HISTORY]
		SET [CGAME_RESULT] += @resultz
		WHERE [CGAME_ID] = @gameID
		AND [CGAME_RESULTON] = @startDateTime
	END
	ELSE
	BEGIN
		INSERT INTO [dbo].[CVD_GAME_SESSION_DAILY_HISTORY] ([CGAME_ID], [CGAME_RESULT], [CGAME_RESULTON], [CHIS_DELETIONSTATE])
		VALUES (@gameID, @resultz, @startDateTime, 0)
	END

	--To store the total bet + total payout on CVD_GAME_SESSION
	UPDATE CVD_GAME_SESSION
	SET [CGAME_TOTAL_BET] = @totalBet, [CGAME_TOTAL_WIN] = @totalPayout
	WHERE [CGAME_ID] = @gameID
	AND [CGAMESES_ID] = @SessionID


	RETURN
GO


