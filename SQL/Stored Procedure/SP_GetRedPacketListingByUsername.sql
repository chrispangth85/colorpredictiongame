USE ColorPrediction
GO

/****** Object:  StoredProcedure [dbo].[SP_GetRedPacketListingByUsername]    Script Date: 11/1/2023 5:50:25 PM ******/
DROP PROCEDURE [dbo].[SP_GetRedPacketListingByUsername]
GO

/****** Object:  StoredProcedure [dbo].[SP_GetRedPacketListingByUsername]    Script Date: 11/1/2023 5:50:25 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SP_GetRedPacketListingByUsername]
(
	@username	NVARCHAR(500) = ''
)
AS
	SET NOCOUNT ON

	--Get the none complete task first
	SELECT *
	FROM [dbo].[CVD_RED_PACKET]
	where [CREDP_AGENT] = @username
	AND [CREDP_DELETIONSTATE] = 0
	ORDER BY [CREDP_ID] DESC

	RETURN




GO


