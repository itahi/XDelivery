alter table CaixaMovimento add Turno varchar(5);
GO
ALTER procedure [dbo].[spInserirMovimentoCaixa]
@CodCaixa int,
@Data datetime,
@Historico nvarchar(100),
@NumeroDocumento nvarchar(50),
@CodFormaPagamento int,
@Valor decimal(10,2),
@Tipo char(1),
@CodUser int,
@Turno varchar(5)
as
  begin
     insert into CaixaMovimento (CodCaixa,Data,Historico,NumeroDocumento,CodFormaPagamento,Valor,Tipo,CodUsuario,Turno)
	        values (@CodCaixa,@Data,@Historico,@NumeroDocumento,@CodFormaPagamento,@Valor,@Tipo,@CodUser,@Turno)
  end
