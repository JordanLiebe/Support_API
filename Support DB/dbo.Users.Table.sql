/****** Object:  Table [dbo].[Users]    Script Date: 12/15/2020 9:28:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UUID] [varchar](100) NOT NULL,
	[Login] [varchar](200) NOT NULL,
	[Hash] [varchar](500) NOT NULL,
	[Email] [varchar](200) NOT NULL,
	[Status] [varchar](25) NOT NULL,
	[Created] [datetime] NOT NULL,
	[First_Name] [varchar](150) NOT NULL,
	[Middle_Name] [varchar](150) NULL,
	[Last_Name] [varchar](150) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UUID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_Users] UNIQUE NONCLUSTERED 
(
	[Login] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
