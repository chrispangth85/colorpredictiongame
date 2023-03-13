USE ColorPrediction
GO

/****** Object:  StoredProcedure [dbo].[SP_GetAllGame]    Script Date: 6/3/2023 8:41:44 PM ******/
DROP PROCEDURE [dbo].[SP_GetAllGame]
GO

/****** Object:  StoredProcedure [dbo].[SP_GetAllGame]    Script Date: 6/3/2023 8:41:44 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[SP_GetAllGame]
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
		SELECT @pages = COUNT(CGAME_ID)
		FROM dbo.CVD_GAME
		WHERE CGAME_DELETIONSTATE = 0
	END
	ELSE IF @filterType = 'Name' AND @keyword != ''
	BEGIN
		SELECT @pages = COUNT(CGAME_ID)
		FROM dbo.CVD_GAME
		WHERE CGAME_DELETIONSTATE = 0
		AND (@filterType = 'Name' AND @keyword != '' AND  CGAME_NAME like '%' + @keyword + '%')
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
			SELECT ROW_NUMBER() OVER (ORDER BY mem.[CGAME_ID] DESC) AS rownumber, mem.*,
			ISNULL((SELECT CGAME_PERIOD FROM [dbo].[CVD_GAME_SESSION] ses WHERE ses.CGAME_ID = mem.CGAME_ID AND ses.[CGAME_STATUS] = 1), '') AS CURRENT_PERIOD
			FROM dbo.CVD_GAME mem
			WHERE CGAME_DELETIONSTATE = 0
		) AS FOO
		WHERE rownumber > ((@viewPage - 1) * @tableRows) AND rownumber < (((@viewPage - 1) * @tableRows) + @tableRows) + 1
	END
	ELSE IF @filterType = 'Name' AND @keyword != ''
	BEGIN
		--get the cash wallet logs
		SELECT * FROM 
		(
			SELECT ROW_NUMBER() OVER (ORDER BY mem.[CGAME_ID] DESC) AS rownumber, mem.*,
			ISNULL((SELECT CGAME_PERIOD FROM [dbo].[CVD_GAME_SESSION] ses WHERE ses.CGAME_ID = mem.CGAME_ID AND ses.[CGAME_STATUS] = 1), '') AS CURRENT_PERIOD
			FROM dbo.CVD_GAME mem
			WHERE CGAME_DELETIONSTATE = 0
			AND @filterType = 'Name' AND @keyword != '' AND  CGAME_NAME like '%' + @keyword + '%'
		) AS FOO
		WHERE rownumber > ((@viewPage - 1) * @tableRows) AND rownumber < (((@viewPage - 1) * @tableRows) + @tableRows) + 1
	END

	SET @ok = 1
	SET @msg = 'Success'

	RETURN
GO


