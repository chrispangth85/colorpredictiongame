USE ColorPrediction
GO

INSERT INTO [dbo].[CVD_MODULE] ([CMDL_ID] ,[CMDL_MAIN_MODULE] ,[CMDL_MAIN_MODULE_SEQ] ,[CMDL_NAME] ,[CMDL_NAME_SEQ], CMDL_CREATEDBY, CMDL_UPDATEDBY)
     VALUES (704 ,'Transaction' ,7 ,'CashWalletLog' ,1, 'SYS', 'SYS')
GO


INSERT INTO [dbo].[CVD_ACCESSRIGHT] ([CAR_ID],[CMDL_ID],[CAR_CODE],[CAR_SEQ],CAR_CREATEDBY,CAR_UPDATEDBY)
     VALUES (704 ,704 ,'ViewCashWalletLog' ,1, 'SYS', 'SYS')
GO




