create procedure spObterDadosCaixa
@Data date,
@Numero varchar(10)
 as 
   begin
     select
	   ISNULL(Codigo,0) as Codigo,
	   ISNULL(Numero,1) as Numero,
	   ISNULL(Data,getdate()) as Data,
	   ISNULL(CodUsuario,0) as CodUsuario,
	   ISNULL(Historico,'') as Historico,
	   ISNULL(ValorAbertura,0) as ValorAbertura,
	   ISNULL(ValorFechamento,0) as ValorFechamento,
	   ISNULL(Estado,0) as Estado
   from 
   Caixa
  where Data = @Data
    and Numero = @Numero 
   end
go
create procedure spAbrirCaixa
 @CodUsuario int,
 @Data date,
 @Estado bit,
 @Historico nvarchar(100),
 @ValorAbertura decimal(10,2),
 @Numero varchar(10)
 as
   begin
     insert into Caixa (CodUsuario,Data,Estado,Historico,ValorAbertura,Numero) 
	             values (@CodUsuario,@Data,@Estado,@Historico,@ValorAbertura,@Numero) 
   end
go

create procedure spFecharCaixa
@CodUsuario int,
@Data date,
@Estado bit,
@Historico nvarchar(100),
@ValorFechamento decimal(10,2),
@Numero varchar(10)
as 
  begin
    update Caixa
	  set
	  CodUsuario = @CodUsuario,
	  Data       = @Data,
	  Estado     = @Estado,
	  Historico  = @Historico,
	  ValorFechamento = @ValorFechamento

	  where Data = @Data 
	  and Numero = @Numero
  end