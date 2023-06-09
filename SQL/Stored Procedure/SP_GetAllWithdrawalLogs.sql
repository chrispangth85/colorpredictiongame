USE [ColorPrediction]
GO

/****** Object:  StoredProcedure [dbo].[SP_GetAllWithdrawalLogs]    Script Date: 19/3/2023 11:38:59 AM ******/
DROP PROCEDURE [dbo].[SP_GetAllWithdrawalLogs]
GO

/****** Object:  StoredProcedure [dbo].[SP_GetAllWithdrawalLogs]    Script Date: 19/3/2023 11:38:59 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetAllWithdrawalLogs]
(
	@viewPage	INT,
	@filterType nvarchar(200),
	@keyword	nvarchar(200),
	@fromDate	NVARCHAR(200) = '',
	@toDate		NVARCHAR(200) = '',
	@pages		INT OUTPUT,
	@ok			INT OUTPUT,
	@msg		VARCHAR(50) OUTPUT
)
AS
	SET NOCOUNT ON
	SET @ok = 0
	SET @pages = 0
	
	DECLARE @tableRows INT = 0
	SELECT @tableRows = CPARA_DECIMALVALUE
	FROM dbo.CVD_PARAMETER
	WHERE CPARA_NAME = 'TableRows'
	
	--select the rows
	IF @keyword = '' AND @fromDate='' AND @toDate= ''
	BEGIN
		SELECT @pages = COUNT(CCASH_ID)
		FROM dbo.CVD_CASHWALLETLOG
		WHERE CCASH_CASHNAME = 'WDR'
		AND CCASH_DELETIONSTATE = 0 AND CCASH_STATUS != 0
	END
	ELSE IF @filterType = 'Name' AND @keyword != '' AND @fromDate='' AND @toDate= ''
	BEGIN
		SELECT @pages = COUNT(CCASH_ID)
		FROM dbo.CVD_CASHWALLETLOG mem, CVD_USER usr
		WHERE mem.CUSR_USERNAME = usr.CUSR_USERNAME
		AND mem.CCASH_DELETIONSTATE = 0
		AND mem.CCASH_CASHNAME = 'WDR' AND CCASH_STATUS != 0
		AND (@filterType = 'Name' AND @keyword != '' AND  usr.CUSR_FIRSTNAME like '%' + @keyword + '%')
	END
	ELSE IF @keyword = '' AND @fromDate!='' AND @toDate!= ''
		BEGIN
		SELECT @pages = COUNT(CCASH_ID)
		FROM dbo.CVD_CASHWALLETLOG
		WHERE CCASH_CASHNAME = 'WDR'
		AND CCASH_DELETIONSTATE = 0 AND CCASH_STATUS != 0
		AND CCASH_CREATEDON between @fromDate and @toDate
	END
	ELSE IF @filterType = 'Name' AND @keyword != '' AND @fromDate!='' AND @toDate!= ''
		BEGIN
		SELECT @pages = COUNT(CCASH_ID)
		FROM dbo.CVD_CASHWALLETLOG mem, CVD_USER usr
		WHERE mem.CUSR_USERNAME = usr.CUSR_USERNAME
		AND mem.CCASH_DELETIONSTATE = 0
		AND mem.CCASH_CASHNAME = 'WDR' AND CCASH_STATUS != 0
		AND (@filterType = 'Name' AND @keyword != '' AND  usr.CUSR_FIRSTNAME like '%' + @keyword + '%')
		AND CCASH_CREATEDON between @fromDate and @toDate
	END
		
	--check if modulus, if it is 0, if not add another page
	DECLARE @mod INT = 0
	SET @mod = @pages % @tableRows
	SET @pages = @pages / @tableRows
	
	IF @mod <> 0
	BEGIN
		SET @pages = @pages + 1
	END
	
	--if the selected page is -1, set the selected page to the first page
	IF @viewPage = -1
	BEGIN
		SET @viewPage = 0
	END
	
	IF @keyword = '' AND @fromDate='' AND @toDate= ''
	BEGIN
		--get the cash wallet logs
		SELECT * FROM 
		(
			SELECT ROW_NUMBER() OVER (ORDER BY mem.[CCASH_ID] DESC) AS rownumber, mem.*, usr.CUSR_FIRSTNAME, usr.CUSR_REFERRALID
			FROM dbo.CVD_CASHWALLETLOG mem, CVD_USER usr
			WHERE mem.CUSR_USERNAME = usr.CUSR_USERNAME
			AND mem.CCASH_CASHNAME = 'WDR'
			AND CCASH_STATUS != 0
			AND mem.CCASH_DELETIONSTATE = 0
		) AS FOO
		WHERE rownumber > ((@viewPage - 1) * @tableRows) AND rownumber < (((@viewPage - 1) * @tableRows) + @tableRows) + 1
	END
	ELSE IF @filterType = 'Name' AND @keyword != '' AND @fromDate='' AND @toDate= ''
	BEGIN
		--get the cash wallet logs
		SELECT * FROM 
		(
			SELECT ROW_NUMBER() OVER (ORDER BY mem.[CCASH_ID] DESC) AS rownumber, mem.*, usr.CUSR_FIRSTNAME, usr.CUSR_REFERRALID
			FROM dbo.CVD_CASHWALLETLOG mem, CVD_USER usr
			WHERE mem.CUSR_USERNAME = usr.CUSR_USERNAME
			AND mem.CCASH_DELETIONSTATE = 0
			AND CCASH_STATUS != 0
			AND mem.CCASH_CASHNAME = 'WDR'
			AND @filterType = 'Name' AND @keyword != '' AND  usr.CUSR_FIRSTNAME like '%' + @keyword + '%'
		) AS FOO
		WHERE rownumber > ((@viewPage - 1) * @tableRows) AND rownumber < (((@viewPage - 1) * @tableRows) + @tableRows) + 1
	END
	ELSE IF @keyword = '' AND @fromDate!='' AND @toDate!= ''
	BEGIN
		--get the cash wallet logs
		SELECT * FROM 
		(
			SELECT ROW_NUMBER() OVER (ORDER BY mem.[CCASH_ID] DESC) AS rownumber, mem.*, usr.CUSR_FIRSTNAME, usr.CUSR_REFERRALID
			FROM dbo.CVD_CASHWALLETLOG mem, CVD_USER usr
			WHERE mem.CUSR_USERNAME = usr.CUSR_USERNAME
			AND mem.CCASH_CASHNAME = 'WDR'
			AND CCASH_STATUS != 0
			AND mem.CCASH_DELETIONSTATE = 0
			AND CCASH_CREATEDON between @fromDate and @toDate
		) AS FOO
		WHERE rownumber > ((@viewPage - 1) * @tableRows) AND rownumber < (((@viewPage - 1) * @tableRows) + @tableRows) + 1
	END
	ELSE IF @filterType = 'Name' AND @keyword != '' AND @fromDate!='' AND @toDate!=''
	BEGIN
		--get the cash wallet logs
		SELECT * FROM 
		(
			SELECT ROW_NUMBER() OVER (ORDER BY mem.[CCASH_ID] DESC) AS rownumber, mem.*, usr.CUSR_FIRSTNAME, usr.CUSR_REFERRALID
			FROM dbo.CVD_CASHWALLETLOG mem, CVD_USER usr
			WHERE mem.CUSR_USERNAME = usr.CUSR_USERNAME
			AND mem.CCASH_DELETIONSTATE = 0
			AND CCASH_STATUS != 0
			AND mem.CCASH_CASHNAME = 'WDR'
			AND @filterType = 'Name' AND @keyword != '' AND  usr.CUSR_FIRSTNAME like '%' + @keyword + '%'
			AND CCASH_CREATEDON between @fromDate and @toDate
		) AS FOO
		WHERE rownumber > ((@viewPage - 1) * @tableRows) AND rownumber < (((@viewPage - 1) * @tableRows) + @tableRows) + 1
	END
	SET @ok = 1
	SET @msg = 'Success'

	RETURN
GO


