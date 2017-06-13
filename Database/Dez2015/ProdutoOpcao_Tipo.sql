
CREATE TABLE [dbo].[Produto_OpcaoTipo](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](30) NOT NULL,
	[Tipo] [char](2) NULL,
	[MaximoOpcionais] [int] NOT NULL,
	[MinimoOpcionais] [int] NOT NULL,
	[OrdenExibicao] [int] NULL,
	[DataAlteracao] [datetime] NULL,
	[DataSincronismo] [datetime] NULL,
 CONSTRAINT [PK_OpcaoTipo01] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


