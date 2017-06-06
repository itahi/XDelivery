create procedure spCancelarHistoricoPessoa
@CodPedido int
as
  begin
     delete from HistoricoPessoa where
	 Historico like '%'+@CodPedido
  end