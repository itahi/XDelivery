
ALTER procedure [dbo].[spCancelarHistoricoPessoa]
@CodPedido int
as
  begin
     delete from HistoricoPessoa where
	 Historico like '%'+ cast(@CodPedido as nvarchar(100))
  end
