create table Objeto
(
	obId varchar(15),
	obNombre varchar(100),
	catId varchar(15),
	obCantidad int,
	obFecCreacion date,
	obVigencia char(1), -- S / N
	primary key(obId),
)
go
create table Categoria
(
	catId varchar(15),
	catNombre varchar(100),
	primary key(catId),
)
go
create table Paquete
(
	paqId varchar(15),
	paqNombre varchar(100),
	paqCantObjetos int,
	paqFecCreacion date,
	primary key(paqId),
)
go
create table PaqueteDetalle
(
	paqDId int,
	paqId varchar(15),
	obId varchar(15),
	paqDCantObjeto int,
	primary key(paqDId,paqId),
)
go

