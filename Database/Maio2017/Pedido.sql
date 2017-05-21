alter table Pedido add PagoFidelidade bit;
GO
ALTER PROCEDURE [dbo].[spAdicionarPedido]
    @Codigo int output,
    @CodPessoa nvarchar(100),
    @TotalPedido decimal(10,2),
    @TrocoPara nvarchar(max),
    @FormaPagamento nvarchar(100),
    @RealizadoEm   datetime,
    @Tipo nvarchar(100),
    @NumeroMesa nvarchar(100),
    @Status     nvarchar(20),
    @PedidoOrigem nvarchar(10),
	@CodigoMesa int	,
	@DescontoValor decimal(10,2),
	@CodigoPedidoWS int,
	@CodUsuario int,
	@HorarioEntrega nvarchar(max),
	@Observacao nvarchar(max),
	@CodEndereco int,
    @Senha nvarchar(max),
	@PagoFidelidade bit
as
        BEGIN
		set @CodigoMesa= (select Codigo from Mesas where Codigo = @CodigoMesa)
		 if @CodigoMesa>0
		 begin
		     exec spAlteraStatusMesa @CodigoMesa,2;
		 end
		
            INSERT INTO Pedido(CodPessoa,TotalPedido,TrocoPara,FormaPagamento,RealizadoEm,Tipo,NumeroMesa,
            [Status],PedidoOrigem,CodigoMesa,DescontoValor,CodigoPedidoWS,CodUsuario,HorarioEntrega,
			Observacao,CodEndereco,Senha,PagoFidelidade)
            Values(
                @CodPessoa,@TotalPedido,@TrocoPara,@FormaPagamento,@RealizadoEm,@Tipo,@NumeroMesa,
                @Status,@PedidoOrigem, @CodigoMesa ,@DescontoValor,@CodigoPedidoWS,@CodUsuario,@HorarioEntrega,
				@Observacao,@CodEndereco,@Senha,@PagoFidelidade
            );
            SET @Codigo = SCOPE_IDENTITY()
            RETURN @Codigo

        END

go
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
	isnull(PagoFidelidade,0) as PagoFidelidade
	from Pedido Pd
	left join ItemsPedido It on It.CodPedido = Pd.Codigo
    left join Pessoa P on P.Codigo = Pd.CodPessoa
	left join Pessoa_Endereco PE on Pe.CodPessoa = Pd.CodPessoa and PE.Codigo=@CodEndereco
	left join RegiaoEntrega RG on RG.Codigo = P.CodRegiao
	where Pd.Codigo = @Codigo 
	
  end

