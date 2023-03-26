USE [ColorPrediction]
GO

/****** Object:  StoredProcedure [dbo].[SP_CalculateRechargeBonus]    Script Date: 26/3/2023 7:35:42 AM ******/
DROP PROCEDURE [dbo].[SP_CalculateRechargeBonus]
GO

/****** Object:  StoredProcedure [dbo].[SP_CalculateRechargeBonus]    Script Date: 26/3/2023 7:35:42 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_CalculateRechargeBonus]
(
	@username	NVARCHAR(200),
	@recharge	DECIMAL(12, 2)
)
AS
	SET NOCOUNT ON
		
	DECLARE @utc_date DATETIME = GETUTCDATE()
	DECLARE @gmt_date DATETIME = SWITCHOFFSET(CONVERT(VARCHAR(20),@utc_date,100), '+08:00')
	SET @gmt_date = DATEADD(SECOND, DATEPART(SECOND, @utc_date), @gmt_date)
	
	DECLARE @intro NVARCHAR(200)
	DECLARE @Level1Percentage DECIMAL(15,2)
	DECLARE @Level2Percentage DECIMAL(15,2)

	SELECT @Level1Percentage = [CPARA_DECIMALVALUE]
	FROM [dbo].[CVD_PARAMETER]
	WHERE [CPARA_NAME] = 'FirstChargeLevel1'

	SELECT @Level2Percentage = [CPARA_DECIMALVALUE]
	FROM [dbo].[CVD_PARAMETER]
	WHERE [CPARA_NAME] = 'FirstChargeLevel2'

	DECLARE @Level1Bonus DECIMAL(15,3) = @recharge * @Level1Percentage / 100
	DECLARE @Level2Bonus DECIMAL(15,3) = @recharge * @Level2Percentage / 100

	SELECT @intro = [CUSR_REFERRALID]
	FROM [dbo].[CVD_USER]
	WHERE [CUSR_USERNAME] = @username

	SET @Level1Bonus = cast((@Level1Bonus-(@Level1Bonus%.01)) as decimal (15,2))
	SET @Level2Bonus = cast((@Level2Bonus-(@Level2Bonus%.01)) as decimal (15,2))

	IF NOT EXISTS(SELECT 1 FROM CVD_CASHWALLETLOG WHERE CUSR_USERNAME = @username AND CCASH_CASHNAME = 'RECHARGE_COM1' AND CCASH_APPUSER = @username)
	BEGIN	

		EXEC SP_CashWalletOperation @username, 'RECHARGE_COM1', @Level1Bonus, @username, @recharge, @Level1Percentage

		IF @intro != ''
		BEGIN
			EXEC SP_CashWalletOperation @intro, 'RECHARGE_COM2', @Level2Bonus, @username, @recharge, @Level2Percentage
		END
	END
	
	RETURN
GO


