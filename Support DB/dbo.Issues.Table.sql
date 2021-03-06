/****** Object:  Table [dbo].[Issues]    Script Date: 12/15/2020 9:28:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Issues](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Subject] [varchar](300) NOT NULL,
	[Priority] [varchar](25) NOT NULL,
	[Category] [varchar](100) NOT NULL,
	[Department] [varchar](100) NOT NULL,
	[Author] [varchar](150) NOT NULL,
	[Status] [varchar](100) NOT NULL,
	[Created] [datetime] NOT NULL,
	[Assignee] [varchar](150) NULL,
	[Assigned] [datetime] NULL,
 CONSTRAINT [PK_Issues] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
