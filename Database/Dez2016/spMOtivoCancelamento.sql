
ALTER procedure [dbo].[spAlteraMotivoCancelamento]
@Nome nvarchar(50),
@DataCadastro date,
@Codigo int
as
  begin
    Update MOtivoCancelamento 
	set 
	Nome=@Nome
	where 
	Codigo = @Codigo 
  end

