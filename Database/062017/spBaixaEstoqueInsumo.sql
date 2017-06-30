create procedure spBaixaEstoqueInsumo
@CodInsumo int,
@Quantidade decimal(10,2),
@QuantidadeBaixar decimal(10,2)
as 
  begin
    insert into Insumo_Estoque (CodInsumo, Quantidade,DataAlteracao) values (@CodInsumo,-@QuantidadeBaixar*@Quantidade,GETDATE())
  end



