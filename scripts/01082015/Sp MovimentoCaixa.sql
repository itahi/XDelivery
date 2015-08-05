create procedure spObterCaixaMovimetoFiltro
@DataInicio date,
@DataFim date,
@Tipo char(1),
@CodFormaPagamento int,
@CodCaixa nvarchar(10)
as
  begin
    select 
	CX.Numero as 'Numero Caixa',
	CXM.DATA,
	CXM.Historico,
	CXM.NumeroDocumento,
	FP.Descricao AS 'FORMA PAGAMENTO',
	CXM.Valor,
	case  CXM.Tipo
	when 'E' THEN 'Entrada'
	when 'S' then 'Saida'
	end 
	as 
	 'Tipo Movimento'
	 
	 from CaixaMovimento  CXM
	 LEFT JOIN FormaPagamento FP ON FP.Codigo = CXM.CodFormaPagamento
	 LEFT JOIN Caixa          CX ON CX.Codigo = CXM.CodCaixa

where 
  CXM.CodCaixa          = @CodCaixa and
  CXM.CodFormaPagamento = @CodFormaPagamento and
  CXM.Data BETWEEN @DataInicio AND @DataFim AND
  CXM.Tipo = @Tipo
  end