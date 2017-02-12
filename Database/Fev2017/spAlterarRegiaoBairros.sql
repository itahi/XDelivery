
ALTER procedure [dbo].[spAlterarBairrosRegiao]
@CodRegiao int,
@Nome  nvarchar(100),
@CEP  nvarchar(10),
@DataCadastro datetime,
@AtivoSN bit,
@OnlineSN bit
as 
  begin
  update RegiaoEntrega_Bairros 
  set 
  CodRegiao = @CodRegiao,
  Nome = @Nome,
  CEP = @CEP,
  DataCadastro = @DataCadastro,
  AtivoSN = @AtivoSN,
  OnlineSN = @OnlineSN

  where 
  --CodRegiao = @CodRegiao
  Cep = @CEp
  end


