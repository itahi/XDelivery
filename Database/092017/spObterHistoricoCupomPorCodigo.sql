create procedure spObterHistoricoCupomPorCodigo
@Codigo int
as
select 
PE.Nome as 'Nome Cliente',
cast( PC.DataCadastro as date) as 'Data do uso'
from 
Pedido_Cupom PC
join Pedido P on P.Codigo=PC.CodPedido
join Pessoa PE on PE.Codigo=P.CodPessoa
where PC.CodCupom=@Codigo
