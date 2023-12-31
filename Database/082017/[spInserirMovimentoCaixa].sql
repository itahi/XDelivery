
ALTER procedure [dbo].[spInserirMovimentoCaixa]
@Historico nvarchar(100),
@NumeroDocumento nvarchar(50),
@CodFormaPagamento int,
@Valor decimal(10,2),
@Tipo char(1),
@CodUser int,
@Turno varchar(5)
as
  begin
     declare @CodCaixa int;
	 declare @Data datetime;
	 set @CodCaixa = ( select Codigo from Caixa where Estado=0 and Turno=@Turno)
	 set @Data = ( select Data from Caixa where Estado=0 and Turno=@Turno)
     insert into CaixaMovimento (CodCaixa,Data,Historico,NumeroDocumento,CodFormaPagamento,Valor,Tipo,CodUsuario,Turno)
	        values (@CodCaixa,@Data,@Historico,@NumeroDocumento,@CodFormaPagamento,@Valor,@Tipo,@CodUser,@Turno)
  end
