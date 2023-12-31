
ALTER procedure [dbo].[sbObterUltimoPedido]
  @CodPessoa int
  as
   begin
	select 
	top 1 P.Codigo,
		 P.CodPessoa,
		 TotalPedido,
		 (select FormaPagamento from Pedido PS where PS.Codigo = p.Codigo) as FP, 
		RealizadoEm,
		(select top 1 Codigo from Pessoa_Endereco where CodPessoa = P.CodPessoa order by Codigo asc) as CodEndereco
	from Pedido P
	left join Pessoa_Endereco PE on PE.CodPessoa=P.CodPessoa
	where P.CodPessoa =@CodPessoa and Finalizado =1
	order by RealizadoEm desc
  end
