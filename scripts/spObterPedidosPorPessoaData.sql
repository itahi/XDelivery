create procedure spObterPedidosPessoaPorData
@CodPessoa int ,
@DataInicio date,
@DataFim date 
as
 begin

select 
   P.Codigo as 'Codigo Pedido',
   cast(P.RealizadoEm as date) as 'Data do Pedido ',
   TotalPedido ,
   Tipo,
   [status]
from 
Pedido P
where 
  CodPessoa = @CodPessoa and
  RealizadoEm between  @DataInicio and @DataFim

end



