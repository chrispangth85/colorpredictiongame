USE ColorPrediction
GO

/****** Object:  StoredProcedure [dbo].[SP_GetRedPacketClaimListingByUsername]    Script Date: 11/1/2023 5:50:25 PM ******/
DROP PROCEDURE [dbo].[SP_GetRedPacketClaimListingByUsername]
GO

/****** Object:  StoredProcedure [dbo].[SP_GetRedPacketClaimListingByUsername]    Script Date: 11/1/2023 5:50:25 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SP_GetRedPacketClaimListingByUsername]
(
	@username	NVARCHAR(500) = ''
)
AS
	SET NOCOUNT ON

	--Get the none complete task first
	SELECT *
	FROM [dbo].[CVD_RED_PACKET]
	where CUSR_USERNAME = @username
	AND [CREDP_DELETIONSTATE] = 0
	ORDER BY [CREDP_ID] ASC

	RETURN




GO


