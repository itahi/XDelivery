
ALTER procedure [dbo].[spObterOpcaoApp]
as
begin
  select 
  ISNULL(o.Codigo,0) as Codigo,
  ISNULL(o.Tipo,0) as Tipo,
  ISNULL(o.Nome,0) as Nome,
  O.AtivoSN,
  O.OnlineSN,
  O.DataSincronismo,
  O.DataAlteracao,
  O.SinalOpcao,
  O.DiasDisponivel
from Opcao O
join Produto_OpcaoTipo PO on PO.Codigo = O.Tipo
end

