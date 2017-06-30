create procedure spReeimprimeItemsMesa
@CodigoMesa int
as
begin
  update ItemsPedido set ImpressoSN = 0 where CodPedido in ( select Codigo from Pedido where CodigoMesa=@CodigoMesa and Finalizado=0)
end