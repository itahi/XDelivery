alter table Opcao add AtivoSN bit;
GO
ALTER procedure [dbo].[spAdicionarOpcao]
--@Codigo int output,
@Nome nvarchar(100),
@Tipo nvarchar(100),
@DataAlteracao datetime,
@OnlineSN bit,
@AtivoSN bit
as
  begin
    insert into Opcao (Nome,Tipo,DataAlteracao,OnlineSN,AtivoSN) 
	   values (@Nome,@Tipo,@DataAlteracao,@OnlineSN,@AtivoSN) 
	    
  end
  GO
ALTER procedure [dbo].[spAlteraOpcao]
@Codigo int,
@Nome nvarchar(100),
@Tipo nvarchar(100),
@DataAlteracao datetime,
@OnlineSN bit,
@AtivoSN bit
as
begin
  update Opcao set 
  Nome = @Nome,
  Tipo = @Tipo,
  DataAlteracao =@DataAlteracao,
  OnlineSN =@OnlineSN,
  AtivoSN = @AtivoSN
  where Codigo =@Codigo
end
go
ALTER procedure [dbo].[spObterOpcao]
as
begin
 select 
  ISNULL(o.Codigo,0) as Codigo,
  ISNULL(o.Tipo,0) as Tipo,
  ISNULL(o.Nome+'( '+PO.Nome+' )',0) as NomeExibir,
  ISNULL(o.Nome,0) as Nome,
  ISNULL(o.AtivoSN,0) as AtivoSN,
  ISNULL(o.OnlineSN,0) as OnlineSN
from Opcao O
join Produto_OpcaoTipo PO on PO.Codigo = O.Tipo
where PO.AtivoSN =1

end
GO
ALTER procedure [dbo].[spObterTipoOpcao]
as
begin
   select * from Produto_OpcaoTipo WHERE ATIVOSN=1
end




