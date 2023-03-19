Use ColorPrediction

ALTER TABLE [dbo].[CVD_CASHWALLETLOG]
ADD CCASH_CARDNUMBER NVARCHAR(100) DEFAULT ''
GO

ALTER TABLE [dbo].[CVD_CASHWALLETLOG]
ADD CCASH_BRANCH NVARCHAR(100) DEFAULT ''
GO

ALTER TABLE [dbo].[CVD_CASHWALLETLOG]
ADD CCASH_STATE NVARCHAR(100) DEFAULT ''
GO

ALTER TABLE [dbo].[CVD_CASHWALLETLOG]
ADD CCASH_CITY NVARCHAR(100) DEFAULT ''
GO

ALTER TABLE [dbo].[CVD_CASHWALLETLOG]
ADD CCASH_BANKNAME NVARCHAR(100) DEFAULT ''
GO

ALTER TABLE [dbo].[CVD_CASHWALLETLOG]
ADD CCASH_BANKACCOUNTNAME NVARCHAR(100) DEFAULT ''
GO

UPDATE [dbo].[CVD_CASHWALLETLOG]
SET CCASH_CARDNUMBER = '',
CCASH_BRANCH = '',
CCASH_STATE = '',
CCASH_CITY = '',
CCASH_BANKNAME = '',
CCASH_BANKACCOUNTNAME = ''