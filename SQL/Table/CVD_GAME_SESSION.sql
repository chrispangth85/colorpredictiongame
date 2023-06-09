USE ColorPrediction
GO

/****** Object:  Table [dbo].[CVD_GAME_SESSION]    Script Date: 16/12/2022 2:33:11 PM ******/
DROP TABLE [dbo].[CVD_GAME_SESSION]
GO

/****** Object:  Table [dbo].[CVD_GAME_SESSION]    Script Date: 16/12/2022 2:33:11 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CVD_GAME_SESSION](
	[CGAMESES_ID] [int] IDENTITY(1,1) NOT NULL,
	[CGAME_ID] INT NOT NULL,
	[CGAME_PERIOD] NVARCHAR(200) NOT NULL,
	[CGAME_RESULT] INT NOT NULL,
	[CGAME_START] [datetime] NOT NULL,
	[CGAME_END] [datetime] NOT NULL,
	[CGAME_STATUS] INT NOT NULL,--0 Inactive 1 Active(Current Game Running) 2 Completed
	[CGAMESES_DELETIONSTATE] [bit] NOT NULL,
 CONSTRAINT [PK_CVD_GAME_SESSION] PRIMARY KEY CLUSTERED 
(
	[CGAMESES_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


