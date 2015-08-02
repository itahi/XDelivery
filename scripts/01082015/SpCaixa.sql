create procedure spObterDadosCaixa
@Data date
 as 
   begin
     select
	   ISNULL(Codigo,0) as Codigo,
	   ISNULL(Data,getdate()) as Data,
	   ISNULL(CodUsuario,0) as CodUsuario,
	   ISNULL(Historico,'') as Historico,
	   ISNULL(ValorAbertura,0) as ValorAbertura,
	   ISNULL(ValorFechamento,0) as ValorFechamento,
	   ISNULL(Estado,0) as Estado
   from 
   Caixa
  where Data = @Data

   end
go
create procedure spAbrirCaixa
 @CodUsuario int,
 @Data date,
 @Estado bit,
 @Historico nvarchar(100),
 @ValorAbertura decimal(10,2)
 as
   begin
     insert into Caixa (CodUsuario,Data,Estado,Historico,ValorAbertura) 
	             values (@CodUsuario,@Data,@Estado,@Historico,@ValorAbertura) 
   end
go

create procedure spFecharCaixa
@CodUsuario int,
@Data date,
@Estado bit,
@Historico nvarchar(100),
@ValorFechamento decimal(10,2)
as 
  begin
    update Caixa
	  set
	  CodUsuario = @CodUsuario,
	  Data       = @Data,
	  Estado     = @Estado,
	  Historico  = @Historico,
	  ValorFechamento = @ValorFechamento
  end