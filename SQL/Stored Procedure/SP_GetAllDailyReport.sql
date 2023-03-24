USE [ColorPrediction]
GO

/****** Object:  StoredProcedure [dbo].[SP_GetAllDailyReport]    Script Date: 24/3/2023 10:54:02 AM ******/
DROP PROCEDURE [dbo].[SP_GetAllDailyReport]
GO

/****** Object:  StoredProcedure [dbo].[SP_GetAllDailyReport]    Script Date: 24/3/2023 10:54:02 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SP_GetAllDailyReport]
(
	@viewPage	INT,
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
	IF @fromDate='' AND @toDate= ''
		BEGIN
			SELECT @pages = COUNT(CDAIL_ID)
			FROM dbo.CVD_DAILYTRANS
		END
	ELSE 
		BEGIN
			SELECT @pages = COUNT(CDAIL_ID)
			FROM dbo.CVD_DAILYTRANS
			WHERE CDAIL_TRANDATE between @fromDate and @toDate
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
	
	IF @fromDate='' AND @toDate= ''
	BEGIN
		--get the cash wallet logs
		SELECT * FROM 
		(
			SELECT ROW_NUMBER() OVER (ORDER BY CDAIL_ID DESC) AS rownumber, * FROM dbo.CVD_DAILYTRANS 
		) AS FOO
		WHERE rownumber > ((@viewPage - 1) * @tableRows) AND rownumber < (((@viewPage - 1) * @tableRows) + @tableRows) + 1
	END
	ELSE
	BEGIN
		--get the cash wallet logs
		SELECT * FROM 
		(
			SELECT ROW_NUMBER() OVER (ORDER BY CDAIL_ID DESC) AS rownumber, * FROM dbo.CVD_DAILYTRANS 
			WHERE CDAIL_TRANDATE between @fromDate and @toDate
		) AS FOO
		WHERE rownumber > ((@viewPage - 1) * @tableRows) AND rownumber < (((@viewPage - 1) * @tableRows) + @tableRows) + 1
	END
	
	SET @ok = 1
	SET @msg = 'Success'

	RETURN
GO


