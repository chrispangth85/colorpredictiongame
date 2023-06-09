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

-----------------------

ALTER TABLE [dbo].[CVD_CASHWALLETLOG]
ADD CCASH_MOBILE NVARCHAR(500) DEFAULT ''
GO

ALTER TABLE [dbo].[CVD_CASHWALLETLOG]
ADD CCASH_EMAIL NVARCHAR(500) DEFAULT ''
GO

ALTER TABLE [dbo].[CVD_CASHWALLETLOG]
ADD CCASH_ADDRESS NVARCHAR(1000) DEFAULT ''
GO

ALTER TABLE [dbo].[CVD_CASHWALLETLOG]
ADD CCASH_APPROVALDATE DATETIME DEFAULT ''
GO

ALTER TABLE [dbo].[CVD_CASHWALLETLOG]
ADD CCASH_PAYMENTMSG NVARCHAR(1000) DEFAULT ''
GO

ALTER TABLE [dbo].[CVD_CASHWALLETLOG]
ADD CCASH_PAYMENTTRANDID NVARCHAR(1000) DEFAULT ''
GO

UPDATE [dbo].[CVD_CASHWALLETLOG]
SET CCASH_MOBILE = '',
CCASH_EMAIL = '',
CCASH_ADDRESS = '',
CCASH_APPROVALDATE = NULL,
CCASH_PAYMENTMSG = '',
CCASH_PAYMENTTRANDID = ''
