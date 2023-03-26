USE [ColorPrediction]
GO

/****** Object:  StoredProcedure [dbo].[SP_ExecutePendingJob]    Script Date: 26/3/2023 7:47:17 AM ******/
DROP PROCEDURE [dbo].[SP_ExecutePendingJob]
GO

/****** Object:  StoredProcedure [dbo].[SP_ExecutePendingJob]    Script Date: 26/3/2023 7:47:17 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SP_ExecutePendingJob]
AS
	SET NOCOUNT ON

	DECLARE @id INT
	DECLARE @username NVARCHAR(200)
	DECLARE @cashName NVARCHAR(200)
	DECLARE @appOther1 NVARCHAR(200)
	DECLARE @appOther2 NVARCHAR(200)
	DECLARE @amount DECIMAL(15,2)

	DECLARE tzCurPendingJob CURSOR FOR
			SELECT [CJOB_ID], [CUSR_USERNAME], [CJOB_CASHNAME], [CJOB_AMOUNT], [CJOB_APPOTHER1], [CJOB_APPOTHER2]
			FROM [dbo].[CVD_PENDING_JOB]
			WHERE [CJOB_STATUS] = 0
	OPEN tzCurPendingJob
	FETCH NEXT FROM tzCurPendingJob INTO @id, @username, @cashName, @amount, @appOther1, @appOther2
	WHILE @@FETCH_STATUS = 0
		BEGIN 	

		IF @cashName = 'Recharge' OR @cashName = 'WDR' OR @cashName = 'PlaceBet' OR @cashName = 'BetWin'
		BEGIN
		    EXEC [SP_InsertDailyTrans] @cashName, @amount
			EXEC [SP_PassUpSales] @username, @amount, @cashName

			IF @cashName = 'Recharge'
			BEGIN
				EXEC [SP_CalculateRechargeBonus] @username, @amount
			END
		END
		ELSE IF @cashName = 'PlaceBetReferralBonus'
		BEGIN
			EXEC [SP_CalculateSponsorBonus] @username, @amount, @appOther1
		END
		ELSE IF @cashName = 'BetCommission'
		BEGIN
			EXEC [SP_PassUpSales] @username, @amount, @cashName
		END
		ELSE IF @cashName = 'UpdateTotalSponsor'
		BEGIN
			EXEC [SP_PassUpSales] @username, @amount, @cashName
		END

		UPDATE [dbo].[CVD_PENDING_JOB]
		SET [CJOB_STATUS] = 1
		WHERE [CJOB_ID] = @id

	FETCH NEXT FROM tzCurPendingJob INTO @id, @username, @cashName, @amount, @appOther1, @appOther2
	END
	CLOSE tzCurPendingJob
	DEALLOCATE tzCurPendingJob
			
	RETURN
GO


