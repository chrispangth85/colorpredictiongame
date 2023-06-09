USE ColorPrediction
GO

/****** Object:  Table [dbo].[CVD_REDPACKETWALLETLOG]    Script Date: 07/20/2022 3:44:58 PM ******/
DROP TABLE [dbo].[CVD_REDPACKETWALLETLOG]
GO

/****** Object:  Table [dbo].[CVD_REDPACKETWALLETLOG]    Script Date: 07/20/2022 3:44:58 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CVD_REDPACKETWALLETLOG](
	[CRED_ID] [int] IDENTITY(1,1) NOT NULL,
	[CUSR_USERNAME] [nvarchar](200) NOT NULL,
	[CRED_CASHIN] [decimal](15, 2) NOT NULL,
	[CRED_CASHOUT] [decimal](15, 2) NOT NULL,
	[CRED_CASHNAME] [nvarchar](100) NOT NULL,
	[CRED_WALLET] [decimal](15, 2) NOT NULL,
	[CRED_APPUSER] [nvarchar](200) NULL,
	[CRED_APPNUMBER] [decimal](15, 2) NULL,
	[CRED_APPRATE] [decimal](15, 2) NULL,
	[CRED_APPOTHER] [nvarchar](200) NULL,
	[CRED_CREATEDBY] [nvarchar](200) NOT NULL,
	[CRED_CREATEDON] [datetime] NOT NULL,
	[CRED_DELETIONSTATE] [bit] NOT NULL,
	[CRED_STATUS] [int] NULL,
 CONSTRAINT [PK_CVD_REDPACKETWALLETLOG] PRIMARY KEY CLUSTERED 
(
	[CRED_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CVD_REDPACKETWALLETLOG] ADD  DEFAULT ('SYS') FOR [CRED_CREATEDBY]
GO

ALTER TABLE [dbo].[CVD_REDPACKETWALLETLOG] ADD  DEFAULT (getdate()) FOR [CRED_CREATEDON]
GO

ALTER TABLE [dbo].[CVD_REDPACKETWALLETLOG] ADD  DEFAULT ((0)) FOR [CRED_DELETIONSTATE]
GO

ALTER TABLE [dbo].[CVD_REDPACKETWALLETLOG] ADD  DEFAULT ((0)) FOR [CRED_STATUS]
GO


