Use ColorPrediction

ALTER TABLE [dbo].CVD_USER
ADD [CUSR_CASHWLT] DECIMAL(15,2) DEFAULT 0
GO

UPDATE [dbo].CVD_USER
SET [CUSR_CASHWLT] = 0


--To know how much member need to finish use before can withdraw. 
--Eg: Recharge 5000 company give extra 1200 points. Thus 6200 need to spend finish before can withdraw
ALTER TABLE [dbo].CVD_USER
ADD [CUSR_RECHARGEWLT] DECIMAL(15,2) DEFAULT 0
GO

UPDATE [dbo].CVD_USER
SET [CUSR_RECHARGEWLT] = 0


---
USE ColorPrediction

ALTER TABLE [dbo].CVD_USER
ADD [CUSR_REDPACKETWLT] DECIMAL(15,2) DEFAULT 0
GO

UPDATE [dbo].CVD_USER
SET [CUSR_REDPACKETWLT] = 0

---

USE ColorPrediction

ALTER TABLE [dbo].CVD_USER
ADD [MEMBER_LEVEL2_INTRO] NVARCHAR(200) DEFAULT ''
GO

ALTER TABLE [dbo].CVD_USER
ADD [MEMBER_LEVEL3_INTRO] NVARCHAR(200) DEFAULT ''
GO

ALTER TABLE [dbo].CVD_USER
ADD [MEMBER_LEVEL4_INTRO] NVARCHAR(200) DEFAULT ''
GO

ALTER TABLE [dbo].CVD_USER
ADD [MEMBER_LEVEL5_INTRO] NVARCHAR(200) DEFAULT ''
GO

UPDATE [dbo].CVD_USER
SET [MEMBER_LEVEL2_INTRO] = '', [MEMBER_LEVEL3_INTRO] = '', [MEMBER_LEVEL4_INTRO] = '', [MEMBER_LEVEL5_INTRO] = ''

---

USE ColorPrediction

ALTER TABLE [dbo].CVD_USER
ADD [MEMBER_DOWNLINE_TOTAL_RECHARGE] DECIMAL(15,2) DEFAULT 0
GO

ALTER TABLE [dbo].CVD_USER
ADD [MEMBER_DOWNLINE_TOTAL_WITHDRAWAL] DECIMAL(15,2) DEFAULT 0
GO

ALTER TABLE [dbo].CVD_USER
ADD [MEMBER_DOWNLINE_TOTAL_BET] DECIMAL(15,2) DEFAULT 0
GO

ALTER TABLE [dbo].CVD_USER
ADD [MEMBER_DOWNLINE_TOTAL_WIN] DECIMAL(15,2) DEFAULT 0
GO

UPDATE [dbo].CVD_USER
SET [MEMBER_DOWNLINE_TOTAL_RECHARGE] = 0, [MEMBER_DOWNLINE_TOTAL_WITHDRAWAL] = 0, [MEMBER_DOWNLINE_TOTAL_BET] = 0, [MEMBER_DOWNLINE_TOTAL_WIN] = 0

---

USE ColorPrediction

ALTER TABLE [dbo].CVD_USER
ADD [MEMBER_DOWNLINE_TOTAL_COMMISSION] DECIMAL(15,2) DEFAULT 0
GO

UPDATE [dbo].CVD_USER
SET [MEMBER_DOWNLINE_TOTAL_COMMISSION] = 0

---

USE ColorPrediction

ALTER TABLE [dbo].CVD_USER
ADD [MEMBER_TOTAL_DOWNLINE] INT DEFAULT 0
GO

UPDATE [dbo].CVD_USER
SET [MEMBER_TOTAL_DOWNLINE] = 0