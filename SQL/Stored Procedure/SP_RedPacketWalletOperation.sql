USE ColorPrediction
GO

/****** Object:  StoredProcedure [dbo].[SP_RedPacketWalletOperation]    Script Date: 11/21/2021 10:33:47 AM ******/
DROP PROCEDURE [dbo].[SP_RedPacketWalletOperation]
GO

/****** Object:  StoredProcedure [dbo].[SP_RedPacketWalletOperation]    Script Date: 11/21/2021 10:33:47 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_RedPacketWalletOperation]
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
	SET CUSR_REDPACKETWLT = CUSR_REDPACKETWLT + @cashNum 
	WHERE CUSR_USERNAME = @username

	SELECT @wallet = CUSR_REDPACKETWLT
	FROM [dbo].CVD_USER
	WHERE CUSR_USERNAME = @username
					
	IF @cashNum < 0
	BEGIN
		INSERT INTO CVD_REDPACKETWALLETLOG(CUSR_USERNAME, CRED_CASHIN, CRED_CASHOUT, CRED_CASHNAME, CRED_WALLET, CRED_APPUSER, CRED_APPNUMBER, CRED_APPRATE, CRED_APPOTHER, CRED_CREATEDBY, CRED_STATUS, [CRED_CREATEDON])
		VALUES(@username, 0, 0 - @cashNum, @cashName, @wallet, @appuser, @appnumber, @apprate, @appother, 'SYS', @status, @gmt_date)
	END
	ELSE
	BEGIN
		INSERT INTO CVD_REDPACKETWALLETLOG(CUSR_USERNAME, CRED_CASHIN, CRED_CASHOUT, CRED_CASHNAME, CRED_WALLET, CRED_APPUSER, CRED_APPNUMBER, CRED_APPRATE, CRED_APPOTHER, CRED_CREATEDBY, CRED_STATUS, [CRED_CREATEDON])
		VALUES(@username, @cashNum, 0, @cashName, @wallet, @appuser, @appnumber, @apprate, @appother, 'SYS', @status, @gmt_date)
	END	
			
	RETURN




GO


