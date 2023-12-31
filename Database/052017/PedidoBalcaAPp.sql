
ALTER PROCEDURE [dbo].[spAdicionarPedidoBalcaoApp]
	@Codigo int output,
	@CodPessoa nvarchar(100),
	@CodUsuario int,
	@TotalPedido decimal(10,2),	
	@NomeCliente nvarchar(max),
	@Senha nvarchar(max)
as
	BEGIN
	   declare @DataAtual datetime;
	   set @DataAtual = Getdate();
			INSERT INTO Pedido(CodPessoa, TotalPedido, RealizadoEm, Tipo, [Status], PedidoOrigem, CodUsuario,Observacao,Senha)
			Values            (@CodPessoa, @TotalPedido, @DataAtual, '2 - Balcao', 'Aberto', 'Aplicativo',@CodUsuario,@NomeCliente,@Senha);
			SET @Codigo = SCOPE_IDENTITY()
		RETURN @Codigo
	END
go
create PROCEDURE [dbo].[spAlteraPedidoBalcaoApp]
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
create PROCEDURE [dbo].[spObterPedidoBalcaoApp]
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



GO
CREATE PROCEDURE [dbo].[spAdicionarItemAoPedidoBalcaoApp]
	@CodProduto int,
	@Senha nvarchar(max),
	@CodUsuario int,
	@NomeProduto nvarchar(max),
	@Quantidade decimal,
	@PrecoUnitario decimal(10,2),
	@PrecoTotal decimal(10,2),
	@Item nvarchar(max)
	
	as
	BEGIN
		declare @CodPedido int
		--Busco pedidos em aberto
		set @CodPedido = (select top 1 Codigo from Pedido where Senha =@Senha and Finalizado = 0 and [status] = 'Aberto' order by Codigo desc )
		--se achou o pedido
		if (@CodPedido > 0)
		begin
			INSERT INTO ItemsPedido(CodPedido, CodProduto, CodUsuario, NomeProduto, Quantidade, PrecoItem, PrecoTotalItem, Item,ImpressoSN)
			VALUES                 (@CodPedido, @CodProduto, @CodUsuario, @NomeProduto, @Quantidade, @PrecoUnitario, @PrecoTotal, @Item,0)

			exec spAlterarTotalPedidoApp @CodPedido
		end
	END

go
CREATE PROCEDURE [dbo].[spObterItemsPedidoBalcaoApp]
	@Codigo int	
as
	BEGIN
		SELECT 
			i.Codigo,
			i.CodPedido,
			i.CodProduto,
			isnull(i.CodUsuario,1) as CodUsuario,
			i.NomeProduto,
			i.PrecoItem,
			i.PrecoTotalItem,
			Quantidade ,
			i.Item
		FROM 
			ItemsPedido i
			join Pedido p on i.CodPedido = p.Codigo
		WHERE 
			CodPedido = @Codigo and Senha is not null and P.Finalizado=0
		ORDER BY Codigo
	END
