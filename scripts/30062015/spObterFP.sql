create procedure spObterFPPorCodigo
@Codigo int
as
 select * from FormaPagamento
 where Codigo = @Codigo


 