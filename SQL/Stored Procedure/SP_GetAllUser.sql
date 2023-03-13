USE ColorPrediction
GO

/****** Object:  StoredProcedure [dbo].[SP_GetAllUser]    Script Date: 6/3/2023 8:41:44 PM ******/
DROP PROCEDURE [dbo].[SP_GetAllUser]
GO

/****** Object:  StoredProcedure [dbo].[SP_GetAllUser]    Script Date: 6/3/2023 8:41:44 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[SP_GetAllUser]
(
	@viewPage	INT,
	@filterType nvarchar(200),
	@keyword	nvarchar(200),
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
	IF @keyword = ''
	BEGIN
		SELECT @pages = COUNT(CUSR_ID)
		FROM dbo.CVD_USER
		WHERE CUSR_DELETIONSTATE = 0
		AND (CROLE_ID = 2 OR CROLE_ID = 3)--2 Member 3 Agent
	END
	ELSE IF @filterType = 'Phone' AND @keyword != ''
	BEGIN
		SELECT @pages = COUNT(CUSR_ID)
		FROM dbo.CVD_USER
		WHERE CUSR_DELETIONSTATE = 0
		AND (CROLE_ID = 2 OR CROLE_ID = 3)--2 Member 3 Agent
		AND (@filterType = 'Phone' AND @keyword != '' AND  CUSR_USERNAME like '%' + @keyword + '%')
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
	
	IF @keyword = ''
	BEGIN
		--get the cash wallet logs
		SELECT * FROM 
		(
			SELECT ROW_NUMBER() OVER (ORDER BY mem.CUSR_CREATEDON DESC) AS rownumber, mem.*
			FROM dbo.CVD_USER mem
			WHERE CUSR_DELETIONSTATE = 0
			AND (CROLE_ID = 2 OR CROLE_ID = 3)--2 Member 3 Agent
		) AS FOO
		WHERE rownumber > ((@viewPage - 1) * @tableRows) AND rownumber < (((@viewPage - 1) * @tableRows) + @tableRows) + 1
	END
	ELSE IF @filterType = 'Phone' AND @keyword != ''
	BEGIN
		--get the cash wallet logs
		SELECT * FROM 
		(
			SELECT ROW_NUMBER() OVER (ORDER BY mem.CUSR_CREATEDON DESC) AS rownumber, mem.*
			FROM dbo.CVD_USER mem
			WHERE CUSR_DELETIONSTATE = 0
			AND (CROLE_ID = 2 OR CROLE_ID = 3)--2 Member 3 Agent
			AND @filterType = 'Phone' AND @keyword != '' AND  CUSR_USERNAME like '%' + @keyword + '%'
		) AS FOO
		WHERE rownumber > ((@viewPage - 1) * @tableRows) AND rownumber < (((@viewPage - 1) * @tableRows) + @tableRows) + 1
	END

	SET @ok = 1
	SET @msg = 'Success'

	RETURN
GO


