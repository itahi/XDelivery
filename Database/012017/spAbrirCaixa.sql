alter table Caixa alter column HorarioFechamento datetime
go
ALTER procedure [dbo].[spAbrirCaixa]
 @CodUsuario int,
 @Data date,
 @Estado bit,
 @Historico nvarchar(100),
 @ValorAbertura decimal(10,2),
 @Numero varchar(10),
 @Turno varchar(5),
 @HorarioFechamento datetime
 as
   begin
     insert into Caixa (CodUsuario,Data,Estado,Historico,ValorAbertura,Numero,Turno,HorarioAbertura,HorarioFechamento) 
	             values (@CodUsuario,@Data,@Estado,@Historico,@ValorAbertura,@Numero,@Turno,GetDate(),@HorarioFechamento) 
   end


