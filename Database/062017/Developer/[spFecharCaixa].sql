
ALTER procedure [dbo].[spFecharCaixa]
@CodUsuario int,
@Data date,
@Estado bit,
@Historico nvarchar(100),
@ValorFechamento decimal(10,2),
@Turno varchar(5)
as 
  begin
    update Caixa
	  set
	  CodUsuario = @CodUsuario,
	  Data       = @Data,
	  Estado     = @Estado,
	  Historico  = @Historico,
	  ValorFechamento = @ValorFechamento
	  where Codigo =  (select Codigo from Caixa where Turno=@Turno and Estado=0)
  end




