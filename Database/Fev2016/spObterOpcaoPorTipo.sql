create procedure spObterOpcaoPorTipo
@Codigo int
as
 begin
    select * from Opcao where Tipo=@Codigo
 end
 
 go
 create procedure spAlterarMultiploOpcao
 @Preco decimal(10,2),
 @CodOpcao int,
 @OnlineSN bit
  as
    begin
      update Produto_Opcao set
      Preco = @Preco,
      DataAlteracao = Getdate(),
      OnlineSn = @OnlineSN
      where CodOpcao = @CodOpcao
    end
 
 
 
 select * from Produto_Opcao