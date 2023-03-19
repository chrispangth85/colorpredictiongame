USE [ColorPrediction]
GO

/****** Object:  Table [dbo].[CVD_CASHWALLETLOGTEMP]    Script Date: 19/3/2023 10:17:25 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CVD_CASHWALLETLOGTEMP](
	[CCASH_ID] [int] IDENTITY(1,1) NOT NULL,
	[CUSR_USERNAME] [nvarchar](200) NOT NULL,
	[CCASH_CASHIN] [decimal](15, 2) NOT NULL,
	[CCASH_CASHOUT] [decimal](15, 2) NOT NULL,
	[CCASH_CASHNAME] [nvarchar](100) NOT NULL,
	[CCASH_WALLET] [decimal](15, 2) NOT NULL,
	[CCASH_APPUSER] [nvarchar](200) NULL,
	[CCASH_APPNUMBER] [decimal](15, 2) NULL,
	[CCASH_APPRATE] [decimal](15, 2) NULL,
	[CCASH_APPOTHER] [nvarchar](200) NULL,
	[CCASH_CREATEDBY] [nvarchar](200) NOT NULL,
	[CCASH_CREATEDON] [datetime] NOT NULL,
	[CCASH_DELETIONSTATE] [bit] NOT NULL,
	[CCASH_STATUS] [int] NULL,
 CONSTRAINT [PK_CVD_CASHWALLETLOGTEMP] PRIMARY KEY CLUSTERED 
(
	[CCASH_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CVD_CASHWALLETLOGTEMP] ADD  DEFAULT ('SYS') FOR [CCASH_CREATEDBY]
GO

ALTER TABLE [dbo].[CVD_CASHWALLETLOGTEMP] ADD  DEFAULT (getdate()) FOR [CCASH_CREATEDON]
GO

ALTER TABLE [dbo].[CVD_CASHWALLETLOGTEMP] ADD  DEFAULT ((0)) FOR [CCASH_DELETIONSTATE]
GO

ALTER TABLE [dbo].[CVD_CASHWALLETLOGTEMP] ADD  DEFAULT ((0)) FOR [CCASH_STATUS]
GO


