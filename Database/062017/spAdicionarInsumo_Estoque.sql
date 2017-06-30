


create procedure spAdicionarInsumo_Estoque
@CodInsumo int,
@Quantidade decimal(10,2),
@Preco decimal(10,2)
 as
 begin
    insert into Insumo_Estoque  (CodInsumo,Quantidade,DataAlteracao)
	                      values(@CodInsumo,@Quantidade,GETDATE())
   update Insumo 
   set Preco=@Preco,
       DataAlteracao=Getdate()
            where Codigo=@CodInsumo
 end