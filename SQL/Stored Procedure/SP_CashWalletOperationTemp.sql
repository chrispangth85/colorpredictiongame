USE ColorPrediction
GO

/****** Object:  StoredProcedure [dbo].[SP_CashWalletOperationTemp]    Script Date: 11/21/2021 10:33:47 AM ******/
DROP PROCEDURE [dbo].[SP_CashWalletOperationTemp]
GO

/****** Object:  StoredProcedure [dbo].[SP_CashWalletOperationTemp]    Script Date: 11/21/2021 10:33:47 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_CashWalletOperationTemp]
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

	IF @cashNum < 0
	BEGIN
		INSERT INTO CVD_CASHWALLETLOGTEMP(CUSR_USERNAME, CCASH_CASHIN, CCASH_CASHOUT, CCASH_CASHNAME, CCASH_WALLET, CCASH_APPUSER, CCASH_APPNUMBER, CCASH_APPRATE, CCASH_APPOTHER, CCASH_CREATEDBY, CCASH_STATUS, [CCASH_CREATEDON])
		VALUES(@username, 0, 0 - @cashNum, @cashName, @wallet, @appuser, @appnumber, @apprate, @appother, 'SYS', @status, @gmt_date)
	END
	ELSE
	BEGIN
		INSERT INTO CVD_CASHWALLETLOGTEMP(CUSR_USERNAME, CCASH_CASHIN, CCASH_CASHOUT, CCASH_CASHNAME, CCASH_WALLET, CCASH_APPUSER, CCASH_APPNUMBER, CCASH_APPRATE, CCASH_APPOTHER, CCASH_CREATEDBY, CCASH_STATUS, [CCASH_CREATEDON])
		VALUES(@username, @cashNum, 0, @cashName, @wallet, @appuser, @appnumber, @apprate, @appother, 'SYS', @status, @gmt_date)
	END	
			
	RETURN




GO


