USE ColorPrediction
GO

/****** Object:  StoredProcedure [dbo].[SP_PlaceBet]    Script Date: 6/3/2023 8:41:44 PM ******/
DROP PROCEDURE [dbo].[SP_PlaceBet]
GO

/****** Object:  StoredProcedure [dbo].[SP_PlaceBet]    Script Date: 6/3/2023 8:41:44 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[SP_PlaceBet]
(
	@username	nvarchar(200),
	@number		INT,
	@amount		INT,
	@gameid		INT,
	@period		nvarchar(200),
	@ok			INT OUTPUT,
	@msg		VARCHAR(50) OUTPUT
)
AS
	SET NOCOUNT ON
	SET @ok = 0

	DECLARE @utc_date DATETIME = GETUTCDATE()
	DECLARE @gmt_date DATETIME = SWITCHOFFSET(CONVERT(VARCHAR(20),@utc_date,100), '+08:00')
	SET @gmt_date = DATEADD(SECOND, DATEPART(SECOND, @utc_date), @gmt_date)
	
	DECLARE @found INT = 0
	DECLARE @gameSessionID INT = 0
	DECLARE @gameEnd DATETIME

	SELECT @found = 1, @gameEnd = [CGAME_END], @gameSessionID = [CGAMESES_ID]
	FROM [dbo].[CVD_GAME_SESSION]
	WHERE [CGAME_ID] = @gameid
	AND [CGAME_PERIOD] = @period
	AND [CGAME_STATUS] = 1--Game must be 'Active' state

	--Check game session valid or not
	IF @found = 0
	BEGIN
		SET @ok = -1
		SET @msg = 'Game session not found'
		RETURN
	END

	SET @found = 0

	--it's too late to bet
	IF @gmt_date >= DATEADD(SECOND, -30, @gameEnd)
	BEGIN
		SET @ok = -2
		SET @msg = 'It is too late to bet'
		RETURN
	END

	--Check if member have sufficient cash wallet [recharge wallet or whatever we want to call it]
	DECLARE @memberCashWallet DECIMAL(15,2)
	
	SELECT @memberCashWallet = [CUSR_CASHWLT]
	FROM [dbo].[CVD_USER]
	WHERE [CUSR_USERNAME] = @username

	IF @amount > @memberCashWallet
	BEGIN
		SET @ok = -3
		SET @msg = 'Insufficient amount. Please topup'
		RETURN
	END

	--Place bet
	INSERT INTO [dbo].[CVD_GAME_SESSION_BET] ([CGAMESES_ID], [CGAME_ID], [CGAME_PERIOD], [CGAME_NUMBER], [CUSR_USERNAME], [CGAME_AMOUNT], [CGAME_WIN_AMOUNT], [CBET_DELETIONSTATE])
	VALUES (@gameSessionID, @gameid, @period, @number, @username, @amount, 0, 0)

	--Deduct the Cash Wallet for betting
	DECLARE @bet DECIMAL(15,2) = 0 - @amount
	EXEC SP_CashWalletOperation @username, 'PLACE_BET', @bet, '', @amount, 0, @gameSessionID

	SET @ok = 1
	SET @msg = 'Success'

	RETURN
GO


