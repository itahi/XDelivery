
ALTER procedure [dbo].[spObterOpcaoProdutoCodigo]
@Codigo int
  as 
    begin
	  select
	  CodProduto,
	  CodOpcao,
	  ISNULL(Preco,0) as Preco,
	  ISNULL(PO.DataAlteracao,GETDATE()) as DataAlteracao,
	  ISNULL(PO.DataSincronismo,GETDATE()+1) as DataSincronismo,
	  ISNULL(PO.OnlineSN,0) as OnlineSN,
	  ISNULL(PrecoProcomocao,0) as PrecoProcomocao,
	  ISNULL(P.DataFimPromocao,GETDATE()) as DataFimPromocao
	  from Produto_Opcao PO 
	  join Produto P on P.Codigo = PO.CodProduto
	 -- join Opcao  O on O.Tipo = Po.CodOpcao
	  where CodProduto =@Codigo 
	  
	  
	end
	
	

