create procedure spInserirMovimentoCaixa
@CodCaixa int,
@Data datetime,
@Historico nvarchar(100),
@NumeroDocumento nvarchar(50),
@CodFormaPagamento int,
@Valor decimal(10,2),
@Tipo char(1)
as
  begin
     insert into CaixaMovimento (CodCaixa,Data,Historico,NumeroDocumento,CodFormaPagamento,Valor,Tipo)
	        values (@CodCaixa,@Data,@Historico,@NumeroDocumento,@CodFormaPagamento,@Valor,@Tipo)
  end

go
create procedure spExcluirMovimentoCaixa
@Data date,
@CodCaixa int
as 
  begin
    delete from CaixaMovimento 
	 where 
	  Data =@Data
	   and
	   CodCaixa = @CodCaixa
  end