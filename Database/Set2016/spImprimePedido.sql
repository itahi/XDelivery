
ALTER procedure [dbo].[spImprimePedido]
@Codigo int
as
  begin
    select 
	Pd.Codigo, 
	Pd.NumeroMesa,
	Pd.CodPessoa,
	pd.FormaPagamento,
	Pd.TotalPedido,
	Pd.Tipo,
	
	 (select 
count(Codigo)   
from
Pedido
where Tipo =Pd.Tipo
and Cast(RealizadoEm as date) =cast(Pd.RealizadoEm as date)
group by Cast(RealizadoEM as  date),Tipo) as NumeroVenda,
	Pd.RealizadoEm,
	Isnull(Pd.TrocoPara,'S/ Troco') as  TrocoPara,
	It.CodProduto,
	It.NomeProduto,
	Isnull(IT.Item,'') as Item,
	It.PrecoTotalItem,
	It.Quantidade,
	Isnull(RG.TaxaServico,0) as TaxaServico,
	IsNull(Pd.DescontoValor,0) as  DescontoValor,
	(SELECT PrevisaoEntrega from Configuracao) as PrevisaoEntrega,
	(SELECT PrevisaoEntregaSN from Configuracao) as PrevisaoEntregaSN,
	(select CodGrupo from Produto where Produto.Codigo=It.CodProduto) as CodGrupo,
	HorarioEntrega
	from Pedido Pd
	left join ItemsPedido It on It.CodPedido = Pd.Codigo
	left join Pessoa P on P.Codigo = Pd.CodPessoa
	left join RegiaoEntrega RG on RG.Codigo = P.CodRegiao
	where Pd.Codigo = @Codigo 
	exec spObterOpcaoProdutoPedido @Codigo
  end

