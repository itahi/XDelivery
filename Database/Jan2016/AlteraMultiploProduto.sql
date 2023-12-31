
ALTER procedure [dbo].[spAlterarMultiploProduto]
@Codigo int,
@Nome nvarchar(max),
@DataAlteracao datetime,
@Preco decimal(10,2),
@DataInicioPromocao date ,
@DataFimPromocao date,
@PrecoDesconto decimal(10,2),
@DiasDesconto nvarchar(max)
as
begin
  update 
  Produto 
  set
   PrecoProduto = @Preco,
   NomeProduto = @Nome,
   DataAlteracao = @DataAlteracao,
   DataInicioPromocao = @DataInicioPromocao,
   DataFimPromocao = @DataFimPromocao,
   PrecoDesconto = @PrecoDesconto
   where Codigo = @Codigo 
end
