create procedure spObterOpcaoProdutoPedido
@Codigo  int
as
select 
PEO.CodPedido,
PO.Nome as 'Tipo',PEO.Observacao
 ,PEO.Quantidade,PEO.Preco
 from Pedido_Opcao PEO
left join Opcao O on O.Codigo = PEO.CodOpcao
left join Produto_OpcaoTipo PO on PO.Codigo=O.Tipo
where  PEO.CodPedido=@Codigo

