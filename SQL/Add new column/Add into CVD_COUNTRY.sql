---

USE ColorPrediction

ALTER TABLE [dbo].CVD_COUNTRY
ADD CCOUNTRY_NAME NVARCHAR(100) DEFAULT ''
GO

UPDATE [dbo].CVD_COUNTRY
SET CCOUNTRY_NAME = ''

-----------------------

ALTER TABLE [dbo].CVD_COUNTRY
ADD CCOUNTRY_BUY DECIMAL(12, 2) DEFAULT 0
GO

UPDATE [dbo].CVD_COUNTRY
SET CCOUNTRY_BUY = 0

--------------------------

ALTER TABLE [dbo].CVD_COUNTRY
ADD CCOUNTRY_SELL DECIMAL(12, 2) DEFAULT 0
GO

UPDATE [dbo].CVD_COUNTRY
SET CCOUNTRY_SELL = 0