alter table Pedido add Senha nvarchar(max);
go
drop procedure [spAdicionarPedidoBalcaoApp]
go
create PROCEDURE [dbo].[spAdicionarPedidoBalcaoApp]
	@Codigo int output,
	@CodPessoa nvarchar(100),
	@CodUsuario int,
	@TotalPedido decimal(10,2),	
	@RealizadoEm datetime,	
	@NomeCliente nvarchar(max),
	@Senha nvarchar(max)
as
	BEGIN
			INSERT INTO Pedido(CodPessoa, TotalPedido, RealizadoEm, Tipo, [Status], PedidoOrigem, CodUsuario,Observacao,Senha)
			Values            (@CodPessoa, @TotalPedido, Getdate(), '2 - Balcao', 'Aberto', 'Aplicativo',@CodUsuario,@NomeCliente,@Senha);
			SET @Codigo = SCOPE_IDENTITY()
		RETURN @Codigo
	END
go
ALTER PROCEDURE [dbo].[spAlteraPedidoBalcaoApp]
	@Codigo int output,
	@NomeCliente nvarchar(max),
	@Senha	nvarchar(max)
as
	BEGIN			
		update Pedido 
		set Observacao=@NomeCliente ,
		Senha=@Senha
		where Codigo = @Codigo
		exec spAlteraTotalPedido @Codigo
	END
GO
ALTER PROCEDURE [dbo].[spObterPedidoBalcaoApp]
as
	BEGIN
		select 
			p.Codigo,
			p.CodPessoa,
			p.CodigoMesa,
			p.TotalPedido,
			p.RealizadoEm,
			p.CodUsuario,
			isnull(p.Observacao,'Cliente Balcão') as NomeCliente,
			Senha 			
		from 
			Pedido p
			inner join Pessoa pe on p.CodPessoa = pe.Codigo
		where
			p.Finalizado = 0 and P.Tipo='2 - Balcao' and Senha is not null
	END



