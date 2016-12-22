alter table Opcao add DiasDisponivel nvarchar(max)
GO
ALTER procedure [dbo].[spAdicionarOpcao]
--@Codigo int output,
@Nome nvarchar(100),
@Tipo nvarchar(100),
@DataAlteracao datetime,
@OnlineSN bit,
@AtivoSN bit,
@SinalOpcao nvarchar(10),
@DiasDisponivel nvarchar(max)
as
  begin
    insert into Opcao (Nome,Tipo,DataAlteracao,OnlineSN,AtivoSN,SinalOpcao,DiasDisponivel) 
	   values (@Nome,@Tipo,@DataAlteracao,@OnlineSN,@AtivoSN,@SinalOpcao,@DiasDisponivel) 
	    
  end
GO
ALTER procedure [dbo].[spAlteraOpcao]
@Codigo int,
@Nome nvarchar(100),
@Tipo nvarchar(100),
@DataAlteracao datetime,
@OnlineSN bit,
@AtivoSN bit,
@SinalOpcao nvarchar(10),
@DiasDisponivel nvarchar(max)
as
begin
  update Opcao set 
  Nome = @Nome,
  Tipo = @Tipo,
  DataAlteracao =@DataAlteracao,
  OnlineSN =@OnlineSN,
  AtivoSN = @AtivoSN,
  SinalOpcao=@SinalOpcao,
  DiasDisponivel=@DiasDisponivel
  where Codigo =@Codigo
end
go
ALTER procedure [dbo].[spObterOpcao]
as
begin
  select 
  ISNULL(o.Codigo,0) as Codigo,
  ISNULL(o.Tipo,0) as Tipo,
  o.Nome as Nome,
  O.AtivoSN,
  O.OnlineSN,
  isnull(SinalOpcao,'') as SinalOpcao,
  isnull(DiasDisponivel,'') as  DiasDisponivel
from Opcao O
join Produto_OpcaoTipo PO on PO.Codigo = O.Tipo
end
