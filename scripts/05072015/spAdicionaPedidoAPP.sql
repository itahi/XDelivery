USE [DBExpert_Teste]
GO
/****** Object:  StoredProcedure [dbo].[spAdicionarPedidoApp]    Script Date: 06/07/2015 15:59:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spAdicionarPedidoApp]
	@Codigo int output,
	@CodPessoa nvarchar(100),
	@TotalPedido decimal(10,2),	
	@RealizadoEm datetime,	
	@CodigoMesa int	
as
	BEGIN
		declare @NumMesa nvarchar(20);
		set @NumMesa = (select NumeroMesa from Mesas where Codigo = @CodigoMesa)
			
		INSERT INTO Pedido(CodPessoa, TotalPedido, RealizadoEm, NumeroMesa, Tipo, [Status], PedidoOrigem, CodigoMesa)
		Values(@CodPessoa, @TotalPedido, @RealizadoEm, @NumMesa, '1 - Mesa', 'Aberto', 'Aplicativo', @CodigoMesa);
		SET @Codigo = SCOPE_IDENTITY()
			
		--Atualizando status da mesa
		update Mesas set StatusMesa = 2 where Codigo = @CodigoMesa
			
		RETURN @Codigo
	END
