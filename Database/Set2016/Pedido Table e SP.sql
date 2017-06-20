alter table Pedido add HorarioEntrega nvarchar(max);
go
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
	@HorarioEntrega nvarchar(max)
as
        BEGIN
		set @CodigoMesa= (select Codigo from Mesas where Codigo = @CodigoMesa)
		
            INSERT INTO Pedido(CodPessoa,TotalPedido,TrocoPara,FormaPagamento,RealizadoEm,Tipo,NumeroMesa,
            [Status],PedidoOrigem,CodigoMesa,DescontoValor,CodigoPedidoWS,CodUsuario,HorarioEntrega)
            Values(
                @CodPessoa,@TotalPedido,@TrocoPara,@FormaPagamento,@RealizadoEm,@Tipo,@NumeroMesa,
                @Status,@PedidoOrigem, @CodigoMesa ,@DescontoValor,@CodigoPedidoWS,@CodUsuario,@HorarioEntrega
            );
            SET @Codigo = SCOPE_IDENTITY()
            RETURN @Codigo
        
        END
go
ALTER PROCEDURE [dbo].[spAlterarTotalPedido]

	@Codigo int,
	@TotalPedido decimal(10,2),
	@Tipo nvarchar(20),
	@NumeroMesa nvarchar(20),
	@CodUsuario int,
	@HorarioEntrega nvarchar(max)
AS
	BEGIN
		UPDATE Pedido

		SET 

		TotalPedido = @TotalPedido,
		Tipo = @Tipo,
		NumeroMesa = @NumeroMesa,
		CodUsuario =@CodUsuario,
		HorarioEntrega= @HorarioEntrega
		WHERE 
			Codigo = @Codigo --Codigo Produto
	END

	GO
GO
ALTER PROCEDURE [dbo].[spObterPedido]
as
	BEGIN
		SELECT P.Nome ,Pe.Codigo,Pe.Finalizado,Pe.FormaPagamento,Pe.TotalPedido,
		Pe.NumeroMesa,Pe.PedidoOrigem,Pe.Tipo,Pe.HorarioEntrega
		FROM Pedido Pe
		join Pessoa P on P.Codigo = Pe.CodPessoa
	  WHERE Finalizado = 0 and Pe.[status] ='Aberto'
	   ORDER BY Codigo DESC
	END
go
ALTER PROCEDURE [dbo].[spObterPedidoFinalizadoPorCodigo]
@Codigo int
as
	BEGIN
		SELECT P.Nome ,
		ISNULL(Pe.Codigo,0) as Codigo,
		ISNULL(Pe.CodPessoa,0) as CodPessoa,
		ISNULL(Pe.TotalPedido,0) as TotalPedido,
		ISNULL(Pe.TrocoPara,0) as TrocoPara,
		ISNULL(Pe.FormaPagamento,'Dinheiro') as FormaPagamento,
		ISNULL(Pe.Finalizado,0) as Finalizado,
		ISNULL(Pe.RealizadoEm,GETDATE()) as RealizadoEm,
		ISNULL(Pe.Tipo,0) as Tipo,
		ISNULL(Pe.NumeroMesa,0) as NumeroMesa,
		ISNULL(Pe.status,'Aberto') as status,
		ISNULL(Pe.PedidoOrigem,'Balcao') as PedidoOrigem,
		ISNULL(Pe.CodigoMesa,0) as CodigoMesa,
		ISNULL(Pe.CodUsuario,0) as CodUsuario,
		ISNULL(Pe.DescontoValor,0) as DescontoValor,
		ISNULL(Pe.CodMotoboy,0) as CodMotoboy,
		ISNULL(PE.MargemGarcon,0) as MargemGarcon,
		ISNULL(PE.HorarioEntrega,0) as HorarioEntrega

		FROM Pedido Pe
		join Pessoa P on P.Codigo = Pe.CodPessoa
	  WHERE Pe.Codigo = @Codigo and
	  Finalizado = 1 --and Pe.[status] ='Aberto'
	   ORDER BY Codigo DESC
	END
GO
ALTER PROCEDURE [dbo].[spObterPedidoPorCodigo]
@Codigo int
as
	BEGIN
		SELECT P.Nome ,
		ISNULL(Pe.Codigo,0) as Codigo,
		ISNULL(Pe.CodPessoa,0) as CodPessoa,
		ISNULL(Pe.TotalPedido,0) as TotalPedido,
		ISNULL(Pe.TrocoPara,0) as TrocoPara,
		ISNULL(Pe.FormaPagamento,'Dinheiro') as FormaPagamento,
		ISNULL(Pe.Finalizado,0) as Finalizado,
		ISNULL(Pe.RealizadoEm,GETDATE()) as RealizadoEm,
		ISNULL(Pe.Tipo,0) as Tipo,
		ISNULL(Pe.NumeroMesa,0) as NumeroMesa,
		ISNULL(Pe.status,'Aberto') as status,
		ISNULL(Pe.PedidoOrigem,'Balcao') as PedidoOrigem,
		ISNULL(Pe.CodigoMesa,0) as CodigoMesa,
		ISNULL(Pe.CodUsuario,0) as CodUsuario,
		ISNULL(Pe.DescontoValor,0) as DescontoValor,
		ISNULL(Pe.CodMotoboy,0) as CodMotoboy,
		ISNULL(PE.MargemGarcon,0) as MargemGarcon,
        ISNULL(PE.CodUsuario,0) as CodUsuario,
		ISNULL(PE.HorarioEntrega,0) as HorarioEntrega
		FROM Pedido Pe
		join Pessoa P on P.Codigo = Pe.CodPessoa
	  WHERE Pe.Codigo = @Codigo and
	  Finalizado = 0 and Pe.[status] ='Aberto'
	   ORDER BY Codigo DESC
	END

