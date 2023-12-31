
ALTER PROCEDURE [dbo].[spAdicionarPedido]
    @Codigo int output,
    @CodPessoa nvarchar(100),
    @TotalPedido decimal(10,2),
    @TrocoPara nvarchar(max),
    @FormaPagamento nvarchar(100),
    @RealizadoEm   datetime,
    @Tipo nvarchar(100),
    @NumeroMesa nvarchar(20),
    @Status     nvarchar(20),
    @PedidoOrigem nvarchar(10),
	@CodigoMesa int	,
	@DescontoValor decimal(10,2)
as
        BEGIN
		set @CodigoMesa= (select NumeroMesa from Mesas where Codigo = @CodigoMesa)
            INSERT INTO Pedido(CodPessoa,TotalPedido,TrocoPara,FormaPagamento,RealizadoEm,Tipo,NumeroMesa,[Status],PedidoOrigem,CodigoMesa,DescontoValor)
            Values(
                @CodPessoa,@TotalPedido,@TrocoPara,@FormaPagamento,@RealizadoEm,@Tipo,@NumeroMesa,@Status,@PedidoOrigem, @CodigoMesa ,@DescontoValor
            );
            SET @Codigo = SCOPE_IDENTITY()
            RETURN @Codigo
        END