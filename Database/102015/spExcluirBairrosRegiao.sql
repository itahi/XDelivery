create procedure spExcluirBairroRegiao
@CodRegiao int,
@Cep       nvarchar(10)
as
  begin
     delete from RegiaoEntrega_Bairros 
     where CodRegiao=@CodRegiao and CEP=@Cep
  end