

ALTER procedure [dbo].[spObterOpcao]
as
begin
  select 
  ISNULL(o.Codigo,0) as Codigo,
  ISNULL(o.Tipo,0) as Tipo,
  ISNULL(o.Nome+'( '+PO.Nome+' )',0) as Nome,
  O.AtivoSN,
  O.OnlineSN
from Opcao O
join Produto_OpcaoTipo PO on PO.Codigo = O.Tipo
end

