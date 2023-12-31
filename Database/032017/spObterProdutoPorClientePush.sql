
ALTER procedure [dbo].[spObterProdutoPorClientePush]
@DataInicio datetime,
@DataFim datetime,
@CodGrupo int
as
  begin
    select 
	P.Nome,P.Telefone
	from Pessoa P 
	join Pedido PE on PE.CodPessoa = P.Codigo
	join ItemsPedido I on I.CodPedido = PE.Codigo
	join Produto PR on PR.Codigo = I.CodProduto
	where 
	P.user_id!=null and P.user_id!='' and
	PE.RealizadoEM between @DataInicio and @DataFim and 
	 PE.Tipo!='1 - Mesa' and 
	Pr.CodGrupo=@CodGrupo
  end