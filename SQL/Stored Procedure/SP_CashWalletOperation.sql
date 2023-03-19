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

	SELECT @wallet = CUSR_CASHWLT
	FROM [dbo].CVD_USER
	WHERE CUSR_USERNAME = @username					

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
			
	RETURN




GO


