create procedure spContaCancelamentos
@CodPessoa int
as
  begin
     select COUNT(CodPessoa)  as Quantidade from HistoricoCancelamentos
	 where CodPessoa = @CodPessoa
  end