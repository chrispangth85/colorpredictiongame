USE [ColorPrediction]
GO

DROP PROCEDURE [dbo].[SP_InsertPendingJob]
/****** Object:  StoredProcedure [dbo].[SP_InsertPendingJob]    Script Date: 7/3/2023 12:05:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_InsertPendingJob]
(
	@Username nvarchar(200),
	@Cashname nvarchar(200),
	@Amount DECIMAL,
	@Appother1 nvarchar(200),
	@Appother2 nvarchar(200)
)
AS
	SET NOCOUNT ON

	DECLARE @utc_date DATETIME = GETUTCDATE()
	DECLARE @gmt_date DATETIME = SWITCHOFFSET(CONVERT(VARCHAR(20),@utc_date,100), '+08:00')
	SET @gmt_date = DATEADD(SECOND, DATEPART(SECOND, @utc_date), @gmt_date)
	
	INSERT INTO [dbo].[CVD_PENDING_JOB]([CUSR_USERNAME], [CJOB_CASHNAME], [CJOB_AMOUNT], [CJOB_APPOTHER1], [CJOB_APPOTHER2], [CJOB_STATUS], [CJOB_CREATEDON])
	VALUES (@Username, @Cashname, @Amount, @Appother1, @Appother2, 0, @gmt_date)

	RETURN


