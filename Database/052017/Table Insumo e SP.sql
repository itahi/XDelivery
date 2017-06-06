drop table Insumo
go
CREATE TABLE [dbo].[Insumo](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](max) NULL,
	[UnidadeMedida] [char](4) NULL,
	[AtivoSN] [bit] NULL,
	[DataCadastro] [datetime] NULL,
	[DataAlteracao] [datetime] NULL,
	[Preco] [decimal] (10,2),
 CONSTRAINT [PK01_CodInsumo] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER procedure [dbo].[spAdicionarInsumo]
@Nome nvarchar(max),
@UnidadeMedida char(4),
@AtivoSN bit,
@Preco decimal
as 
 begin
  insert into Insumo (Nome,UnidadeMedida,AtivoSN,DataCadastro,DataAlteracao,Preco) 
           values (@Nome,@UnidadeMedida,@AtivoSN,Getdate(),Getdate(),@Preco) 
 end
 GO
ALTER procedure [dbo].[spAlterarInsumo]
@Codigo int,
@Nome nvarchar(max),
@UnidadeMedida char(4),
@AtivoSN bit,
@Preco decimal
as 
 begin
  update  Insumo 
  set
  Nome=@Nome,
  UnidadeMedida=@UnidadeMedida,
  AtivoSN=@AtivoSN,
  DataCadastro=Getdate(),
  DataAlteracao=Getdate(),
  Preco = @Preco 
  where Codigo=@Codigo
 end
