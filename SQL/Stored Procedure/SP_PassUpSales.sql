USE ColorPrediction
GO

/****** Object:  StoredProcedure [dbo].[SP_PassUpSales]    Script Date: 12/4/2022 9:03:15 PM ******/
DROP PROCEDURE [dbo].[SP_PassUpSales]
GO

/****** Object:  StoredProcedure [dbo].[SP_PassUpSales]    Script Date: 12/4/2022 9:03:15 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_PassUpSales]
(
	@username	NVARCHAR(200),
	@sales		DECIMAL(15,2),
	@cashName	NVARCHAR(200)
)
AS
	SET NOCOUNT ON
		
	DECLARE @utc_date DATETIME = GETUTCDATE()
	DECLARE @gmt_date DATETIME = SWITCHOFFSET(CONVERT(VARCHAR(20),@utc_date,100), '+08:00')
	SET @gmt_date = DATEADD(SECOND, DATEPART(SECOND, @utc_date), @gmt_date)
	
	DECLARE @intro NVARCHAR(200)

	SELECT @intro = [CUSR_REFERRALID]
	FROM [dbo].[CVD_USER]
	WHERE [CUSR_USERNAME] = @username

	WHILE @intro <> '0' AND @intro <> ''
	BEGIN
		IF @cashName = 'PlaceBet'
		BEGIN
			UPDATE [dbo].[CVD_USER]
			SET [MEMBER_DOWNLINE_TOTAL_BET] = [MEMBER_DOWNLINE_TOTAL_BET] + @sales
			WHERE [CUSR_USERNAME] = @intro
		END
		ELSE IF @cashName = 'BetWin'
		BEGIN
			UPDATE [dbo].[CVD_USER]
			SET [MEMBER_DOWNLINE_TOTAL_WIN] = [MEMBER_DOWNLINE_TOTAL_WIN] + @sales
			WHERE [CUSR_USERNAME] = @intro
		END

		SELECT @intro = [CUSR_REFERRALID]
		FROM [dbo].[CVD_USER]
		WHERE [CUSR_USERNAME] = @intro
	END
	
	RETURN


GO


