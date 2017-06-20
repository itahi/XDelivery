alter table Pedido add Observacao nvarchar(max);
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
	@Observacao nvarchar(max)
as
        BEGIN
		set @CodigoMesa= (select Codigo from Mesas where Codigo = @CodigoMesa)
		
            INSERT INTO Pedido(CodPessoa,TotalPedido,TrocoPara,FormaPagamento,RealizadoEm,Tipo,NumeroMesa,
            [Status],PedidoOrigem,CodigoMesa,DescontoValor,CodigoPedidoWS,CodUsuario,HorarioEntrega,Observacao)
            Values(
                @CodPessoa,@TotalPedido,@TrocoPara,@FormaPagamento,@RealizadoEm,@Tipo,@NumeroMesa,
                @Status,@PedidoOrigem, @CodigoMesa ,@DescontoValor,@CodigoPedidoWS,@CodUsuario,@HorarioEntrega,@Observacao
            );
            SET @Codigo = SCOPE_IDENTITY()
            RETURN @Codigo
        
        END
GO

ALTER PROCEDURE [dbo].[spAlterarTotalPedido]

	@Codigo int,
	@TotalPedido decimal(10,2),
	@Tipo nvarchar(20),
	@NumeroMesa nvarchar(20),
	@CodUsuario int,
	@HorarioEntrega nvarchar(max),
	@DescontoValor decimal(10,2),
	@Observacao nvarchar(max)
AS
	BEGIN
		UPDATE Pedido

		SET 

		TotalPedido = @TotalPedido,
		Tipo = @Tipo,
		NumeroMesa = @NumeroMesa,
		CodUsuario =@CodUsuario,
		HorarioEntrega= @HorarioEntrega,
		DescontoValor= @DescontoValor,
		Observacao=@Observacao
		WHERE 
			Codigo = @Codigo --Codigo Produto
	END
	go	
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
	HorarioEntrega,
	Isnull((select Nome from Usuario where Cod=Pd.CodUsuario),'Online' )as 'Atendente',
	Pd.Observacao
	from Pedido Pd
	left join ItemsPedido It on It.CodPedido = Pd.Codigo
	left join Pessoa P on P.Codigo = Pd.CodPessoa
	left join RegiaoEntrega RG on RG.Codigo = P.CodRegiao
	where Pd.Codigo = @Codigo 
	
  end
