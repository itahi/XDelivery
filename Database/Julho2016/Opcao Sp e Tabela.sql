alter table Opcao add SinalOpcao nvarchar(10)
go
GO
ALTER procedure [dbo].[spAdicionarOpcao]
--@Codigo int output,
@Nome nvarchar(100),
@Tipo nvarchar(100),
@DataAlteracao datetime,
@OnlineSN bit,
@AtivoSN bit,
@SinalOpcao nvarchar(10)
as
  begin
    insert into Opcao (Nome,Tipo,DataAlteracao,OnlineSN,AtivoSN,SinalOpcao) 
	   values (@Nome,@Tipo,@DataAlteracao,@OnlineSN,@AtivoSN,@SinalOpcao) 
	    
  end
GO
ALTER procedure [dbo].[spAlteraOpcao]
@Codigo int,
@Nome nvarchar(100),
@Tipo nvarchar(100),
@DataAlteracao datetime,
@OnlineSN bit,
@AtivoSN bit,
@SinalOpcao nvarchar(10)
as
begin
  update Opcao set 
  Nome = @Nome,
  Tipo = @Tipo,
  DataAlteracao =@DataAlteracao,
  OnlineSN =@OnlineSN,
  AtivoSN = @AtivoSN,
  SinalOpcao=@SinalOpcao
  where Codigo =@Codigo
end
GO

ALTER procedure [dbo].[spObterOpcao]
as
begin
  select 
  ISNULL(o.Codigo,0) as Codigo,
  ISNULL(o.Tipo,0) as Tipo,
  ISNULL(o.Nome+'( '+PO.Nome+' )',0) as Nome,
  O.AtivoSN,
  O.OnlineSN,
  isnull(SinalOpcao,'') as SinalOpcao
from Opcao O
join Produto_OpcaoTipo PO on PO.Codigo = O.Tipo
end

GO
ALTER procedure [dbo].[spObterOpcaoPorCodigo]
@Codigo int
as
begin
  select 
  ISNULL(o.Codigo,0) as Codigo,
  ISNULL(o.Tipo,0) as Tipo,
  Nome,
  O.AtivoSN,
  O.OnlineSN,
   isnull(SinalOpcao,'') as SinalOpcao
from Opcao O
where O.Codigo=@Codigo and AtivoSN=1
end

GO
ALTER procedure [dbo].[spObterOpcoesProduto]
@Codigo int
as 
  begin
select PO.CodOpcao,PO.Preco,
O.Nome,O.Tipo,isnull(SinalOpcao,'') as SinalOpcao
from 
Produto_Opcao PO
join Opcao O on O.Codigo = PO.CodOpcao
where PO.CodProduto = @Codigo
end