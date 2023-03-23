USE ColorPrediction
GO

/****** Object:  StoredProcedure [dbo].[SP_CashWalletOperation]    Script Date: 11/21/2021 10:33:47 AM ******/
DROP PROCEDURE [dbo].[SP_CashWalletOperation]
GO

/****** Object:  StoredProcedure [dbo].[SP_CashWalletOperation]    Script Date: 11/21/2021 10:33:47 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_CashWalletOperation]
(
	@username		NVARCHAR(200),
	@cashName		NVARCHAR(100),
	@cashNum		decimal(15, 2),
	@appuser		NVARCHAR(200) = '',
	@appnumber		DECIMAL(15,2) = 0,
	@apprate		DECIMAL(15,2) = 0,
	@appother		NVARCHAR(200) = '',
	@bankid			INT = 0,
	@status			INT = 0
)
AS
	SET NOCOUNT ON

	DECLARE @utc_date DATETIME = GETUTCDATE()
	DECLARE @gmt_date DATETIME = SWITCHOFFSET(CONVERT(VARCHAR(20),@utc_date,100), '+08:00')
	SET @gmt_date = DATEADD(SECOND, DATEPART(SECOND, @utc_date), @gmt_date)
	
	IF @cashNum = 0  
		RETURN		
				
	DECLARE @wallet decimal(15, 2) = 0
	
	UPDATE [dbo].CVD_USER
	SET CUSR_CASHWLT = CUSR_CASHWLT + @cashNum 
	WHERE CUSR_USERNAME = @username

	--This Recharge wallet is to keep track how much customer will need to spend on 'betting' in order for them to withdraw
	IF @cashName = 'RED_PACKET'
	BEGIN
		UPDATE [dbo].CVD_USER
		SET CUSR_RECHARGEWLT = CUSR_RECHARGEWLT + @cashNum 
		WHERE CUSR_USERNAME = @username
	END

	IF @cashName = 'WIN_BET'
	BEGIN
		--This is to pass all upline total downline win
		INSERT INTO [dbo].[CVD_PENDING_JOB]([CUSR_USERNAME], [CJOB_CASHNAME], [CJOB_AMOUNT], [CJOB_APPOTHER1], [CJOB_APPOTHER2], [CJOB_STATUS], [CJOB_CREATEDON])
		VALUES (@username, 'BetWin', @cashNum, '', '', 0, @gmt_date)
	END

	IF @cashName = 'BET_COMMISSION'
	BEGIN
		--This is to pass all upline total downline commission
		INSERT INTO [dbo].[CVD_PENDING_JOB]([CUSR_USERNAME], [CJOB_CASHNAME], [CJOB_AMOUNT], [CJOB_APPOTHER1], [CJOB_APPOTHER2], [CJOB_STATUS], [CJOB_CREATEDON])
		VALUES (@username, 'BetCommission', @cashNum, '', '', 0, @gmt_date)
	END

	SELECT @wallet = CUSR_CASHWLT
	FROM [dbo].CVD_USER
	WHERE CUSR_USERNAME = @username					

	IF @cashName = 'WDR'
		BEGIN
			declare @cardnumber NVARCHAR(100)
			declare @cardbranch NVARCHAR(100)
			declare @cardstate NVARCHAR(100)
			declare @cardcity NVARCHAR(100)
			declare @cardbankname NVARCHAR(100)
			declare @cardbankaccountname NVARCHAR(100)
			declare @cardmobile NVARCHAR(100)
			declare @cardaddress NVARCHAR(100)
			declare @cardemail NVARCHAR(100)
		
			SELECT @cardnumber = [CBANK_BANKACCOUNT],  @cardbranch = [CBANK_IFSCCODE], @cardstate= [CBANK_STATE], @cardcity=[CBANK_STATE], @cardbankname=[CBANK_NAME], @cardbankaccountname = [CBANK_BANKACCOUNTNAME],
			@cardmobile = CBANK_MOBILE,
			@cardemail = CBANK_EMAIL,
			@cardaddress = [CBANK_ADDRESS]
			FROM [dbo].[CVD_MEMBER_BANK]
			WHERE [CBANK_ID] = @bankid

			INSERT INTO CVD_CASHWALLETLOG(CUSR_USERNAME, CCASH_CASHIN, CCASH_CASHOUT, CCASH_CASHNAME, CCASH_WALLET, CCASH_APPUSER, CCASH_APPNUMBER, CCASH_APPRATE, CCASH_APPOTHER, CCASH_CREATEDBY, CCASH_STATUS, CCASH_CARDNUMBER, CCASH_BRANCH, CCASH_STATE, CCASH_CITY, CCASH_BANKNAME ,CCASH_BANKACCOUNTNAME, [CCASH_CREATEDON], CCASH_ADDRESS, CCASH_MOBILE, CCASH_EMAIL)
			VALUES(@username, 0, 0 - @cashNum, @cashName, @wallet, @appuser, @appnumber, @apprate, @appother, 'SYS', @status,@cardnumber,@cardbranch,@cardstate,@cardcity,@cardbankname,@cardbankaccountname, @gmt_date, @cardaddress, @cardmobile, @cardemail)
		END
	ELSE
	BEGIN
		IF @cashNum < 0
		BEGIN
			INSERT INTO CVD_CASHWALLETLOG(CUSR_USERNAME, CCASH_CASHIN, CCASH_CASHOUT, CCASH_CASHNAME, CCASH_WALLET, CCASH_APPUSER, CCASH_APPNUMBER, CCASH_APPRATE, CCASH_APPOTHER, CCASH_CREATEDBY, CCASH_STATUS, [CCASH_CREATEDON])
			VALUES(@username, 0, 0 - @cashNum, @cashName, @wallet, @appuser, @appnumber, @apprate, @appother, 'SYS', @status, @gmt_date)
		END
		ELSE
		BEGIN
			INSERT INTO CVD_CASHWALLETLOG(CUSR_USERNAME, CCASH_CASHIN, CCASH_CASHOUT, CCASH_CASHNAME, CCASH_WALLET, CCASH_APPUSER, CCASH_APPNUMBER, CCASH_APPRATE, CCASH_APPOTHER, CCASH_CREATEDBY, CCASH_STATUS, [CCASH_CREATEDON])
			VALUES(@username, @cashNum, 0, @cashName, @wallet, @appuser, @appnumber, @apprate, @appother, 'SYS', @status, @gmt_date)
		END	
	END
	
	RETURN




GO


