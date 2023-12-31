
ALTER procedure [dbo].[spObterOpcao]
as
begin
  select 
  ISNULL(o.Codigo,0) as Codigo,
  ISNULL(o.Tipo,0) as Tipo,
  o.Nome as Nome,
  O.AtivoSN,
  O.OnlineSN,
  isnull(SinalOpcao,'') as SinalOpcao
from Opcao O
join Produto_OpcaoTipo PO on PO.Codigo = O.Tipo
end
go

ALTER procedure [dbo].[spObterOpcaoPorTipo]
@Codigo int
as
 begin
    select Codigo,Nome,Tipo,AtivoSN,OnlineSN,SinalOpcao from Opcao where Tipo=@Codigo
 end
