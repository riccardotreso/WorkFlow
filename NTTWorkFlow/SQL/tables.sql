USE [MyWorkFlow]

/****** Object:  Table [dbo].[Delegation]    Script Date: 14/04/2017 12:17:14 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[Delegation](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[From] [nvarchar](500) NOT NULL,
	[To] [nvarchar](500) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Delegation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[Task]    Script Date: 14/04/2017 12:17:14 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[Task](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[RefID] [nvarchar](max) NOT NULL,
	[Complete] [bit] NOT NULL,
	[CreatiionDate] [datetime] NOT NULL,
	[AssignedTo] [nvarchar](500) NOT NULL,
	[DueDate] [datetime] NULL,
	[ApproverComments] [nvarchar](max) NULL,
 CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]




