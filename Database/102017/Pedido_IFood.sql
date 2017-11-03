
DROP TABLE [dbo].[Pedido_iFood]
GO
CREATE TABLE [dbo].[Pedido_iFood](
	[codigo] [int] IDENTITY(1,1) NOT NULL,
	[idPedido] [nvarchar](max) NULL,
	[status] [nvarchar](max) NULL,
	[Data] [datetime] NULL,
	[Cliente] nvarchar(max),
	Total decimal(10,2)
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
create procedure [dbo].[spAdicionarPedido_iFood]
@idPedido nvarchar(max),
@status nvarchar(max),
@Data datetime,
@Cliente nvarchar(max),
@Total decimal(10,2)
as
 begin
  insert into Pedido_iFood (idPedido,[status],Data,Cliente,Total) values (@idPedido,@status,@Data,@Cliente,@Total)
 end


