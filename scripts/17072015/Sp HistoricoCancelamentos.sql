
create procedure [dbo].[spAdicionaHistoricoCancelamento]
@CodPessoa int,
@Motivo nvarchar(100),
@CodMotivo int,
@Data date
as
  begin
  insert into HistoricoCancelamentos (CodPessoa,Motivo,CodMotivo,Data)
         values (@CodPessoa,@Motivo,@CodMotivo,@Data)

  end


