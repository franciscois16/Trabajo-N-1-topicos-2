create procedure [dbo].[MantenedorPaquetes]
(
	@paqId varchar(15)='',
	@paqNombre varchar(100)='',
	@paqCantObjetos int='',
	@paqFecCreacion date='',
	@xml varchar(max)='',
	@modo char(1)=''
)
as
/*** MODO PARA CREAR UN PAQUETE CON SU DETALLE ***/
IF @modo='C'
BEGIN
	insert into Paquete
	select @paqId,@paqNombre,@paqCantObjetos,@paqFecCreacion
	create table #tmp(xmlData xml)
	insert into #tmp
	select @xml
	insert into PaqueteDetalle
	SELECT ROW_NUMBER() OVER(order by @paqId),@paqId,
		c.value('(obId/text())[1]', 'nvarchar(15)') AS EMAIL,
		isnull(c.value('(paqDCantObjeto/text())[1]', 'int'),'') AS ID_SIGNATURE
	FROM #tmp CROSS APPLY xmlData.nodes('ArrayOfPaqueteDetalle/PaqueteDetalle') AS t(c)
	drop table #tmp
END
/*** MODO PARA ELIMINAR UN PAQUETE CON SU DETALLE ***/
IF @modo='E'
BEGIN
	delete from Paquete where paqId=@paqId
	delete from PaqueteDetalle where paqId=@paqId
END
/*** MODO PARA LISTAR UN PAQUETE ***/
IF @modo='P'
BEGIN
	select paqId,paqNombre,paqCantObjetos,paqFecCreacion from Paquete
END
/*** MODO PARA LISTAR EL DETALLE DE UN PAQUETE ***/
IF @modo='D'
BEGIN
	select paqDId,obId,paqDCantObjeto from PaqueteDetalle where paqId=@paqId
END
/*** MODO PARA OBTENER UN PAQUETE ***/
IF @modo='O'
BEGIN
	select p.paqNombre,p.paqCantObjetos,p.paqFecCreacion from Paquete p where p.paqId=@paqId
END