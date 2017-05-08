create procedure spObterPedidoPorSenha
@Senha nvarchar(100)
as
  select Codigo,Senha,CodPessoa,TotalPedido,CodUsuario,Observacao as NomeCliente from Pedido where Senha=@Senha and Finalizado=0
  and [status]='Aberto' 

