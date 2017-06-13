create procedure [dbo].[spAlterarMultiploProduto]
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
go
create procedure spAlterarMultiploPessoa
@Codigo int,
@CodRegiao int,
@Nome nvarchar(max),
@Cidade nvarchar(max),
@Bairro nvarchar(max),
@Telefone nvarchar(20)
 as 
 begin
   update Pessoa 
   set CodRegiao =@CodRegiao,
       Nome =@Nome,
       Cidade=@Cidade,
       Bairro=@Bairro,
       Telefone=@Telefone
       where Codigo=@Codigo
 end