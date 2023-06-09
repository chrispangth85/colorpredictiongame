USE [ColorPrediction]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateUser]    Script Date: 7/3/2023 12:05:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[SP_UpdateUser]
(
	@Username nvarchar(50),	
	@FullName nvarchar(60),	
	@ReferralID NVARCHAR(50) = '',
	@UpdatedBy nvarchar(50),
	@ok int output,
	@msg varchar(50) output
)
AS
	SET NOCOUNT ON
	set @ok = 0

	IF NOT EXISTS(SELECT 1 FROM CVD_USER WHERE CUSR_USERNAME = @ReferralID)
	BEGIN
		SET @ok = -2
		SET @msg = 'Invalid Referral ID'
		RETURN
	END

	UPDATE dbo.CVD_USER
	SET CUSR_FIRSTNAME = @FullName,
	CUSR_REFERRALID = @ReferralID,
	CUSR_UPDATEDBY = @UpdatedBy,
	CUSR_UPDATEDON = getdate()
	WHERE CUSR_USERNAME = @UserName
	AND CUSR_DELETIONSTATE = 0
    
	set @ok = 1
	set @msg = 'Success'

	RETURN


