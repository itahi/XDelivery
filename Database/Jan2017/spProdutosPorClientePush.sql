create procedure spObterProdutoPorClientePush
@DataInicio datetime,
@DataFim datetime,
@CodGrupo int
as
  begin
    select 
	Pr.*
	from ItemsPedido I 
	join Produto Pr on Pr.Codigo= I.CodProduto or Pr.CodigoPersonalizado=I.CodProduto
	join Pedido P on P.Codigo = I.CodPedido 
	where RealizadoEM between @DataInicio and @DataFim and 
	Pr.CodGrupo=@CodGrupo
  end