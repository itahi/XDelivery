
create procedure [dbo].[spObterOpcaoPorCodigo]
@Codigo int
as
begin
  select 
  ISNULL(o.Codigo,0) as Codigo,
  ISNULL(o.Tipo,0) as Tipo,
  Nome,
  O.AtivoSN,
  O.OnlineSN
from Opcao O
where O.Codigo=@Codigo
end

