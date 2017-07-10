
ALTER procedure [dbo].[spObterMovimentoCaixa]
@Turno nvarchar(10),
@DataI date,
@EntradaSaida nvarchar(max),
@Estado bit
as
 declare @CodCaixa int;
 set @CodCaixa = ( select Codigo from Caixa where Estado=@Estado and Turno=@Turno and Data = Cast(@DataI as date))
 select CX.Numero , 
 CXM.DATA, 
 CXM.Historico, 
 CXM.NumeroDocumento, 
 FP.Descricao ,
 CXM.Valor,
case  
CXM.Tipo
when 'E' THEN 'Entrada'
when 'S' then 'Saida'
end  
 from CaixaMovimento  CXM
LEFT JOIN FormaPagamento FP ON FP.Codigo = CXM.CodFormaPagamento 
LEFT JOIN Caixa CX ON CX.Codigo = CXM.CodCaixa    
where 
CXM.Turno =@Turno
and CXM.CodCaixa=@CodCaixa
--and CXM.Data BETWEEN @DataI AND @DataF
and CXM.Tipo in (@EntradaSaida)
