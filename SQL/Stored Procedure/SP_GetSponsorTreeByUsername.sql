USE ColorPrediction
GO

/****** Object:  StoredProcedure [dbo].[SP_GetSponsorTreeByUsername]    Script Date: 11/1/2023 5:50:25 PM ******/
DROP PROCEDURE [dbo].[SP_GetSponsorTreeByUsername]
GO

/****** Object:  StoredProcedure [dbo].[SP_GetSponsorTreeByUsername]    Script Date: 11/1/2023 5:50:25 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SP_GetSponsorTreeByUsername]
(
	@username	NVARCHAR(500) = ''
)
AS
	SET NOCOUNT ON

	--First level sponsor
	SELECT *
	FROM [dbo].[CVD_USER]
	WHERE [CUSR_REFERRALID] = @username

	--Second level sponsor
	SELECT *
	FROM [dbo].[CVD_USER]
	WHERE [MEMBER_LEVEL2_INTRO] = @username

	--Third level sponsor
	SELECT *
	FROM [dbo].[CVD_USER]
	WHERE [MEMBER_LEVEL3_INTRO] = @username

	--Fourth level sponsor
	SELECT *
	FROM [dbo].[CVD_USER]
	WHERE [MEMBER_LEVEL4_INTRO] = @username

	--Fifth level sponsor
	SELECT *
	FROM [dbo].[CVD_USER]
	WHERE [MEMBER_LEVEL5_INTRO] = @username

	RETURN




GO


