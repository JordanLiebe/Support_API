/****** Object:  Table [dbo].[User_To_Group]    Script Date: 12/15/2020 9:28:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_To_Group](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UUID] [varchar](500) NOT NULL,
	[UGID] [varchar](500) NOT NULL,
 CONSTRAINT [PK_User_To_Group] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
