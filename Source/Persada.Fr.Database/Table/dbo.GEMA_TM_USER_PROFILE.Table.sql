SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GEMA_TM_USER_PROFILE](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[USER_ID_ID] [int] NOT NULL,
	[PHOTO_PATH] [varchar](100) NULL,
	[GENDER] [char](1) NULL,
	[BORN] [datetime] NULL,
	[ADDRESS] [varchar](100) NULL,
	[DESCRIPTION] [varchar](100) NULL,
	[JOB] [varchar](100) NULL,
	[COMPANY] [varchar](100) NULL,
	[HOBBY] [varchar](100) NULL,
	[CREATED_TIME] [datetime] NOT NULL,
	[CREATED_BY] [varchar](50) NOT NULL,
	[LAST_MODIFIED_TIME] [datetime] NULL,
	[LAST_MODIFIED_BY] [varchar](50) NULL,
	[ROW_STATUS] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[GEMA_TM_USER_PROFILE] ADD  DEFAULT ((0)) FOR [ROW_STATUS]
GO
ALTER TABLE [dbo].[GEMA_TM_USER_PROFILE]  WITH CHECK ADD  CONSTRAINT [FK_GEMA_TM_USER_PROFILE_GEMA_TM_USER] FOREIGN KEY([USER_ID_ID])
REFERENCES [dbo].[GEMA_TM_USER] ([ID])
GO
ALTER TABLE [dbo].[GEMA_TM_USER_PROFILE] CHECK CONSTRAINT [FK_GEMA_TM_USER_PROFILE_GEMA_TM_USER]
GO
