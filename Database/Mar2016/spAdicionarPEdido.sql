
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
	@CodigoPedidoWS int
	--@CodUsuario int
as
        BEGIN
		set @CodigoMesa= (select Codigo from Mesas where Codigo = @CodigoMesa)
		
            INSERT INTO Pedido(CodPessoa,TotalPedido,TrocoPara,FormaPagamento,RealizadoEm,Tipo,NumeroMesa,
            [Status],PedidoOrigem,CodigoMesa,DescontoValor,CodigoPedidoWS)
            Values(
                @CodPessoa,@TotalPedido,@TrocoPara,@FormaPagamento,@RealizadoEm,@Tipo,@NumeroMesa,
                @Status,@PedidoOrigem, @CodigoMesa ,@DescontoValor,@CodigoPedidoWS
            );
            SET @Codigo = SCOPE_IDENTITY()
            RETURN @Codigo
            
        
        END
