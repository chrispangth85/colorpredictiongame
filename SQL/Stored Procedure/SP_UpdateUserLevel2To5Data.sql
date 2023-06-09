USE [ColorPrediction]
GO

DROP PROCEDURE [dbo].[SP_UpdateUserLevel2To5Data]
/****** Object:  StoredProcedure [dbo].[SP_UpdateUserLevel2To5Data]    Script Date: 7/3/2023 12:05:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_UpdateUserLevel2To5Data]
(
	@Username nvarchar(200)
)
AS
	SET NOCOUNT ON
	
	DECLARE @intro NVARCHAR(200) = ''
	DECLARE @count INT = 2

	--First level intro [which we alr stored]
	SELECT @intro = [CUSR_REFERRALID]
	FROM [dbo].[CVD_USER]
	WHERE [CUSR_USERNAME] = @Username

	--Second level intro
	SELECT @intro = [CUSR_REFERRALID]
	FROM [dbo].[CVD_USER]
	WHERE [CUSR_USERNAME] = @intro

	WHILE @intro <> '' AND @count <= 5
	BEGIN
		IF @count = 2
		BEGIN
			UPDATE [dbo].[CVD_USER]
			SET [MEMBER_LEVEL2_INTRO] = @intro
			WHERE [CUSR_USERNAME] = @Username
		END
		ELSE IF @count = 3
		BEGIN
			UPDATE [dbo].[CVD_USER]
			SET [MEMBER_LEVEL3_INTRO] = @intro
			WHERE [CUSR_USERNAME] = @Username
		END
		ELSE IF @count = 4
		BEGIN
			UPDATE [dbo].[CVD_USER]
			SET [MEMBER_LEVEL4_INTRO] = @intro
			WHERE [CUSR_USERNAME] = @Username
		END
		ELSE IF @count = 5
		BEGIN
			UPDATE [dbo].[CVD_USER]
			SET [MEMBER_LEVEL5_INTRO] = @intro
			WHERE [CUSR_USERNAME] = @Username
		END

		--Subsequent intro
		SELECT @intro = [CUSR_REFERRALID]
		FROM [dbo].[CVD_USER]
		WHERE [CUSR_USERNAME] = @intro

		SET @count = @count + 1
	END

	RETURN


