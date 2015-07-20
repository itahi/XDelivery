
/****** Object:  StoredProcedure [dbo].[spAdicionarPedido]    Script Date: 14/08/2014 23:02:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spAdicionarPedido]
	@Codigo int output,
	@CodPessoa nvarchar(100),
	@TotalPedido decimal(10,2),
	@TrocoPara nvarchar(max),
	@FormaPagamento nvarchar(100),
	@RealizadoEm   datetime,
	@Tipo nvarchar(100),
	@NumeroMesa nvarchar(20)
as
		BEGIN
			INSERT INTO Pedido(CodPessoa,TotalPedido,TrocoPara,FormaPagamento,RealizadoEm,Tipo,NumeroMesa)
			Values(
				@CodPessoa,@TotalPedido,@TrocoPara,@FormaPagamento,@RealizadoEm,@Tipo,@NumeroMesa
			);
			SET @Codigo = SCOPE_IDENTITY()
			RETURN @Codigo
		END


		GO
ALTER PROCEDURE [dbo].[spAlterarTotalPedido]

	@Codigo int,
	@TotalPedido decimal(10,2),
	@Tipo nvarchar(20),
	@NumeroMesa nvarchar(20)

AS
	BEGIN
		UPDATE Pedido

		SET 

		TotalPedido = @TotalPedido,
		Tipo = @Tipo,
		NumeroMesa = @NumeroMesa

		WHERE 
			Codigo = @Codigo --Codigo Produto
	END
