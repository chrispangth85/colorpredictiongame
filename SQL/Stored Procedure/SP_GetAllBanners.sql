USE [ColorPrediction]
GO

/****** Object:  StoredProcedure [dbo].[SP_GetAllBanners]    Script Date: 16/3/2023 4:59:58 AM ******/
DROP PROCEDURE [dbo].[SP_GetAllBanners]
GO

/****** Object:  StoredProcedure [dbo].[SP_GetAllBanners]    Script Date: 16/3/2023 4:59:58 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_GetAllBanners]
(
	@viewPage	INT,
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
	BEGIN
		SELECT @pages = COUNT(CBANNER_ID)
		FROM dbo.CVD_BANNER
		WHERE CBANNER_DELETIONSTATE = 0
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
	
	BEGIN
		--get the cash wallet logs
		SELECT * FROM 
		(
			SELECT ROW_NUMBER() OVER (ORDER BY mem.CBANNER_ID DESC) AS rownumber, mem.*
			FROM dbo.CVD_BANNER mem
			WHERE CBANNER_DELETIONSTATE = 0
		) AS FOO
		WHERE rownumber > ((@viewPage - 1) * @tableRows) AND rownumber < (((@viewPage - 1) * @tableRows) + @tableRows) + 1
	END

	SET @ok = 1
	SET @msg = 'Success'

	RETURN
GO


