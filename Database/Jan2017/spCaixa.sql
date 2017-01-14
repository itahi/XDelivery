alter table Caixa add HorarioAbertura time;
alter table Caixa add HorarioFechamento time;
GO
ALTER procedure [dbo].[spAbrirCaixa]
 @CodUsuario int,
 @Data date,
 @Estado bit,
 @Historico nvarchar(100),
 @ValorAbertura decimal(10,2),
 @Numero varchar(10),
 @Turno varchar(5),
 @HorarioFechamento time
 as
   begin
     insert into Caixa (CodUsuario,Data,Estado,Historico,ValorAbertura,Numero,Turno,HorarioAbertura,HorarioFechamento) 
	             values (@CodUsuario,@Data,@Estado,@Historico,@ValorAbertura,@Numero,@Turno,GetDate(),@HorarioFechamento) 
   end

