
ALTER procedure [dbo].[spObterDadosOpcao]
@Codigo int,
@CodOpcao int 
  as 
    begin
	  select Pr.Preco,OP.Nome,PO.Tipo from Produto_Opcao Pr
	  left join Opcao OP on OP.Codigo = Pr.CodOpcao 
	  JOIN Produto_OpcaoTipo PO ON PO.Codigo = OP.Tipo
	  where CodProduto =@Codigo
	  and CodOpcao=@CodOpcao

	end
	
