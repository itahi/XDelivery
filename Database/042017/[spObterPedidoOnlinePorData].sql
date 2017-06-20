
ALTER procedure [dbo].[spObterPedidoOnlinePorData]
@DataInicio datetime,
@DataFim datetime
as
  begin
    select PS.Nome,  
	CodigoPedidoWS,FormaPagamento,TotalPedido from Pedido P
	join Pessoa PS on PS.Codigo = P.CodPessoa
	where CodigoPedidoWS!=0 and Finalizado=1 and [status]!='Cancelado' and
    RealizadoEm between @DataInicio and @DataFim  
    end


