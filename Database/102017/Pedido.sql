alter table Pedido add idiFood nvarchar(max);
GO
update Pedido set idiFood =''
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
	@HorarioEntrega nvarchar(max),
	@Observacao nvarchar(max),
	@CodEndereco int,
    @Senha nvarchar(max),
	@PagoFidelidade bit,
	@Cupom nvarchar(max),
	@idiFood nvarchar(max)
as
        BEGIN
		 declare @CodCupom int;
		 set @CodCupom =(select Codigo from Cupom where CodCupom=@Cupom);
		 declare @DescCupom decimal;
		 set @DescCupom = (select Desconto from Cupom where CodCupom=@Cupom and cast(GETDATE() as date) between DataValidade_Inicio and DataValidade_Fim and Quantidade> 0)
		 set @CodigoMesa= (select Codigo from Mesas where Codigo = @CodigoMesa)
		 if @CodigoMesa>0
		 begin
		     exec spAlteraStatusMesa @CodigoMesa,2;
		 end
		
            INSERT INTO Pedido(CodPessoa,TotalPedido,TrocoPara,FormaPagamento,RealizadoEm,Tipo,NumeroMesa,
            [Status],PedidoOrigem,CodigoMesa,DescontoValor,CodigoPedidoWS,CodUsuario,HorarioEntrega,
			Observacao,CodEndereco,Senha,PagoFidelidade,Cupom,ImpressoSN,idiFood )
            Values(
                @CodPessoa,@TotalPedido,@TrocoPara,@FormaPagamento,@RealizadoEm,@Tipo,@NumeroMesa,
                @Status,@PedidoOrigem, @CodigoMesa ,@DescontoValor,@CodigoPedidoWS,@CodUsuario,@HorarioEntrega,
				@Observacao,@CodEndereco,@Senha,@PagoFidelidade,@Cupom,0,@idiFood
            );
            SET @Codigo = SCOPE_IDENTITY()
            RETURN @Codigo
			if @DescCupom>0
		    begin
		    exec spAdicionarPedido_Cupom @Codigo,@CodCupom
			end
        END
GO
ALTER PROCEDURE [dbo].[spAdicionarPedidoApp]
	@Codigo int output,
	@CodPessoa nvarchar(100),
	@CodUsuario int,
	@TotalPedido decimal(10,2),	
	@RealizadoEm datetime,	
	@CodigoMesa int
	
as
	BEGIN
     	declare @NumeroMesa nvarchar(20);
		set @NumeroMesa = (select NumeroMesa from Mesas where Codigo = @CodigoMesa and StatusMesa=1)
		
		if @NumeroMesa!=''
		begin	
			INSERT INTO Pedido(CodPessoa, TotalPedido, RealizadoEm, NumeroMesa, Tipo, [Status], PedidoOrigem, CodigoMesa, CodUsuario,idiFood)
			Values            (@CodPessoa, @TotalPedido, Getdate(),@NumeroMesa, '1 - Mesa', 'Aberto', 'Aplicativo', @CodigoMesa,@CodUsuario,'');
			SET @Codigo = SCOPE_IDENTITY()
			--Atualizando status da mesa
			exec spAlteraStatusMesa @CodigoMesa,2
		end	
		RETURN @Codigo
	END
GO
ALTER PROCEDURE [dbo].[spAdicionarPedidoBalcaoApp]
	@Codigo int output,
	@CodPessoa nvarchar(100),
	@CodUsuario int,
	@TotalPedido decimal(10,2),	
	@NomeCliente nvarchar(max),
	@Senha nvarchar(max),
	@idiFood nvarchar(max)
as
	BEGIN
	   declare @DataAtual datetime;
	   set @DataAtual = Getdate();
			INSERT INTO Pedido(CodPessoa, TotalPedido, RealizadoEm, Tipo, [Status], PedidoOrigem, CodUsuario,Observacao,Senha,ImpressoSN,idiFood )
			Values            (@CodPessoa, @TotalPedido, @DataAtual, '2 - Balcao', 'Aberto', 'Aplicativo',@CodUsuario,@NomeCliente,@Senha,0,@idiFood);
			SET @Codigo = SCOPE_IDENTITY()
		RETURN @Codigo
	END

GO
ALTER PROCEDURE [dbo].[spObterPedido]
as
	BEGIN
		SELECT 
		case Pe.Tipo
		when '1 - Mesa' then  'Mesa' +' - '+ Pe.NumeroMesa 
		when '2 - Balcao' then 'Cliente Balcao ' +PE.Senha +' ' + PE.Observacao
		when '0 - Entrega' then P.Nome 
		end as  'Nome Cliente',
		Pe.Codigo,Pe.Finalizado,Pe.FormaPagamento,Pe.TotalPedido,
		Pe.NumeroMesa,Pe.PedidoOrigem,Pe.Tipo,Pe.HorarioEntrega,isnull(Pe.ImpressoSN,0) as ImpressoSN,RealizadoEm,isnull(idiFood,'') as idiFoodm
		FROM Pedido Pe
		join Pessoa P on P.Codigo = Pe.CodPessoa
	    WHERE Finalizado = 0 and Pe.[status] ='Aberto'
	   ORDER BY Codigo DESC
	END
GO
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
		ISNULL(PE.HorarioEntrega,0) as HorarioEntrega,
		ISNULL(PE.Observacao,' ') as Observacao,
		ISNULL(PE.idiFood,' ') as idiFood
		FROM Pedido Pe
		join Pessoa P on P.Codigo = Pe.CodPessoa
	  WHERE Pe.Codigo = @Codigo and
	  Finalizado = 1 --and Pe.[status] ='Aberto'
	   ORDER BY Codigo DESC
	END