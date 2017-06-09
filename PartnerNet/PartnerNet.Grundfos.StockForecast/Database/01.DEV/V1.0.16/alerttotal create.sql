CREATE TABLE [dbo].[AlertTotal](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Alert] [int] NOT NULL,
	[Total] [int] NOT NULL,
 CONSTRAINT [PK_AlertTotal] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
) ON [PRIMARY]
)  