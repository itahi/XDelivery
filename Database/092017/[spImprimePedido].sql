ALTER procedure [dbo].[spImprimePedido]
@Codigo int,
@CodEndereco int
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
	Pd.TrocoPara ,
	It.CodProduto,
	It.NomeProduto,
	Isnull(IT.Item,'') as Item,
	It.PrecoTotalItem,
	It.Quantidade,
	It.FidelidadeSN,
	Isnull(RG.TaxaServico,0) as TaxaServico,
	IsNull(Pd.DescontoValor,0) as  DescontoValor,
	(select CodGrupo from Produto where Produto.Codigo=It.CodProduto) as CodGrupo,
	HorarioEntrega,
	Isnull((select Nome from Usuario where Cod=Pd.CodUsuario),'Online' )as 'Atendente',
	Pd.Observacao,
	CodEndereco,
	Convert(char(8),cast(HorarioFechamento-RealizadoEM as datetime),114) as TempoPermanencia, 
		case Pd.Tipo
		when '1 - Mesa' then  P.Nome +' - '+ Pd.NumeroMesa 
		when '2 - Balcao' then 'Cliente Balcao ' +Pd.Senha +' ' + Pd.Observacao
		when '0 - Entrega' then P.Nome 
		end as  Nome
	
	,P.Telefone,P.Telefone2,PE.Endereco+' '+'Nº'+PE.Numero as EndereComNumero,PE.Complemento,PE.PontoReferencia,PE.Bairro,PE.Cidade,
	TotalPedido/NumeroPessoas as ValorPorPessoa,
	(Select Nome from Empresa ) as NOmeEmpresa,
    ( select TaxaServico from RegiaoEntrega  where Codigo=P.CodRegiao) as TaxaServico,
    (select Convert(char(8),Dateadd(n, cast(RG.PrevisaoEntrega as int),PD.RealizadoEM),114) from RegiaoEntrega where Codigo=P.CodRegiao) as PrevisaoEntrega,
	(select count(Codigo) from Pedido where CodPessoa=P.Codigo) as ClienteNovo,
	Senha,
	isnull(PagoFidelidade,0) as PagoFidelidade,
	PD.Observacao as ObsPedido,
	PD.Cupom as Cupom
	from Pedido Pd
	left join ItemsPedido It on It.CodPedido = Pd.Codigo
    left join Pessoa P on P.Codigo = Pd.CodPessoa
	left join Pessoa_Endereco PE on Pe.CodPessoa = Pd.CodPessoa and PE.Codigo=@CodEndereco
	left join RegiaoEntrega RG on RG.Codigo = P.CodRegiao
	where Pd.Codigo = @Codigo 
	
  end


