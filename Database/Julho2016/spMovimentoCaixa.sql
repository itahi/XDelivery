alter procedure spMovimentoCaixa
@Caixa int,
@Turno varchar(5),
@DataI date,
@DataF date
as
select 
case Tipo 
when 'E' then 'Entradas'
when 'S' then 'Saidas'
end
as 'Tipo Movimento', 
                        
Fp.Descricao ,
sum(cx.Valor) as 'Total Somado'
from CaixaMovimento CX
left join FormaPagamento FP on FP.Codigo = Cx.CodFormaPagamento
left join Caixa C on C.Codigo = CX.CodCaixa
where 
CX.CodCaixa = @Caixa AND
C.Turno = @Turno and 
CX.Data BETWEEN @DataI  AND @DataF 

group by CodCaixa,Fp.Descricao,Tipo
