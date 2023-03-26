USE ColorPrediction
GO

/****** Object:  StoredProcedure [dbo].[SP_CalculateSponsorBonus]    Script Date: 12/4/2022 9:03:15 PM ******/
DROP PROCEDURE [dbo].[SP_CalculateSponsorBonus]
GO

/****** Object:  StoredProcedure [dbo].[SP_CalculateSponsorBonus]    Script Date: 12/4/2022 9:03:15 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_CalculateSponsorBonus]
(
	@username	NVARCHAR(200),
	@sales		DECIMAL(15,2),
	@cashID		INT
)
AS
	SET NOCOUNT ON
		
	DECLARE @utc_date DATETIME = GETUTCDATE()
	DECLARE @gmt_date DATETIME = SWITCHOFFSET(CONVERT(VARCHAR(20),@utc_date,100), '+08:00')
	SET @gmt_date = DATEADD(SECOND, DATEPART(SECOND, @utc_date), @gmt_date)
	
	DECLARE @intro NVARCHAR(200)
	DECLARE @level INT = 1
	DECLARE @commission DECIMAL(15,2) = 0
	DECLARE @gameServiceFee DECIMAL(15,2) = 0
	DECLARE @gameHandlingFee DECIMAL(15,2) = 0
	DECLARE @sponsorLevel1Bonus DECIMAL(15,2)
	DECLARE @sponsorLevel2Bonus DECIMAL(15,2)
	DECLARE @sponsorLevel3Bonus DECIMAL(15,2)

	SELECT @gameServiceFee = [CPARA_DECIMALVALUE]
	FROM [dbo].[CVD_PARAMETER]
	WHERE [CPARA_NAME] = 'GameServiceFee'

	SELECT @gameHandlingFee = [CPARA_DECIMALVALUE]
	FROM [dbo].[CVD_PARAMETER]
	WHERE [CPARA_NAME] = 'GameHandlingFee'

	SET @commission = @gameServiceFee + @gameHandlingFee

	SELECT @sponsorLevel1Bonus = [CPARA_DECIMALVALUE]
	FROM [dbo].[CVD_PARAMETER]
	WHERE [CPARA_NAME] = 'SponsorBonusLevel1'

	SELECT @sponsorLevel2Bonus = [CPARA_DECIMALVALUE]
	FROM [dbo].[CVD_PARAMETER]
	WHERE [CPARA_NAME] = 'SponsorBonusLevel2'

	SELECT @sponsorLevel3Bonus = [CPARA_DECIMALVALUE]
	FROM [dbo].[CVD_PARAMETER]
	WHERE [CPARA_NAME] = 'SponsorBonusLevel3'

	DECLARE @commissionSales DECIMAL(15,2) = @sales * @commission / 100

	SELECT @intro = [CUSR_REFERRALID]
	FROM [dbo].[CVD_USER]
	WHERE [CUSR_USERNAME] = @username

	WHILE @intro <> '0' AND @intro <> '' AND @level <= 3
	BEGIN
		DECLARE @bonus DECIMAL(15,3) = 0
		DECLARE @bonusRate DECIMAL(15,2) = 0

		IF @level = 1
		BEGIN
			SET @bonusRate = @sponsorLevel1Bonus
		END
		IF @level = 2
		BEGIN
			SET @bonusRate = @sponsorLevel2Bonus
		END
		IF @level = 3
		BEGIN
			SET @bonusRate = @sponsorLevel3Bonus
		END

		SET @bonus = @commissionSales * @bonusRate / 100
		SET @bonus = cast((@bonus-(@bonus%.01)) as decimal (15,2))

		EXEC SP_CashWalletOperation @intro, 'BET_COMMISSION', @bonus, @username, @commissionSales, @bonusRate, @cashID

		SELECT @intro = [CUSR_REFERRALID]
		FROM [dbo].[CVD_USER]
		WHERE [CUSR_USERNAME] = @intro

		SET @level = @level + 1
	END
	
	RETURN


GO


