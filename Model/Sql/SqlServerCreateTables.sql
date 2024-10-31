use [MaD] 


/* Drop tables.
 * NOTE: before dropping a table (when re-executing the script), the tables
 * having columns acting as foreign keys of the table to be dropped must be
 * dropped first (otherwise, the corresponding checks on those tables could
 * not be done).
 */

/* Drop Table AccountOp if already exists */
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Producto]') 
AND type in ('U')) DROP TABLE [Producto]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Etiqueta]') 
AND type in ('U')) DROP TABLE [Etiqueta]
GO

/* Drop Table Account if already exists */
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Usuario]') 
AND type in ('U')) DROP TABLE [Usuario]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Comentario]') 
AND type in ('U')) DROP TABLE [Comentario]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Valoracion]') 
AND type in ('U')) DROP TABLE [Valoracion]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[TagCom]') 
AND type in ('U')) DROP TABLE [TagCom]
GO
/*
 * Create tables.
 * Account and AccountOp tables are created. Indexes required for the 
 * most common operations are also defined.
 */


/*
 * Create tables.
 * Account and AccountOp tables are created. Indexes required for the 
 * most common operations are also defined.
 */

/* Usuario */

CREATE TABLE Usuario(
    userID bigint IDENTITY(1,1),
	username VARCHAR(64) NOT NULL UNIQUE,
	pwd VARCHAR(64) NOT NULL,
    nombre VARCHAR(64) NOT NULL,
    apellidos VARCHAR(64) NOT NULL,
    email VARCHAR(64) NOT NULL UNIQUE,
	idioma VARCHAR(64),
	nacionalidad VARCHAR(64),

    CONSTRAINT [PK_Usuario] PRIMARY KEY (userID),
) 


PRINT N'Table Usuario created.'
GO

/* Producto */

CREATE TABLE Producto (
    productID bigint IDENTITY (1,1),
	nombre VARCHAR(64) NOT NULL,
    categoria VARCHAR(64) NOT NULL,
    date datetime2 NOT NULL,
    vendedor bigint NOT NULL,
	imagen VARBINARY,
	descripcion VARCHAR(512),
    
    CONSTRAINT [PK_Producto] PRIMARY KEY (productID),
    
    CONSTRAINT [FK_ProductoVendedor] FOREIGN KEY(vendedor)
        REFERENCES Usuario (userID),
)

PRINT N'Table Producto created.'
GO

/* Comentario */

CREATE TABLE Comentario(
    comID bigint IDENTITY(1,1),
	usuario bigint NOT NULL,
	producto bigint NOT NULL,
    date datetime2 NOT NULL,
    text VARCHAR(512) NOT NULL,

    CONSTRAINT [PK_Comentario] PRIMARY KEY (comID),

	CONSTRAINT [FK_ComentarioUsuario] FOREIGN KEY(usuario)
        REFERENCES Usuario (userID),
		
	CONSTRAINT [FK_ComentarioProducto] FOREIGN KEY(producto)
        REFERENCES Producto (productID),
) 


PRINT N'Table Comentario created.'
GO

CREATE TABLE Valoracion(
	valID bigint IDENTITY(1,1),
	comprador bigint NOT NULL,
	producto bigint NOT NULL,
	puntuacion float NOT NULL,
	comentario VARCHAR(512),
	date datetime2 NOT NULL,

	CONSTRAINT [PK_Valoracion] PRIMARY KEY (valID),

	CONSTRAINT [FK_ValoracionComprador] FOREIGN KEY(comprador)
		REFERENCES Usuario(userID),

	CONSTRAINT [FK_ValoracionProducto] FOREIGN KEY(producto)
		REFERENCES Producto(productID),

	CONSTRAINT validPuntuacion CHECK ( puntuacion >= 0 AND puntuacion <=5 ),
)
PRINT N'Table Valoracion created.'
GO

CREATE TABLE Etiqueta(
	tagID BIGINT IDENTITY(1,1),
	nombre VARCHAR(64) NOT NULL UNIQUE,
	numUsos INT DEFAULT 0,

	CONSTRAINT [PK_Etiqueta] PRIMARY KEY (tagID),
)
PRINT N'Table Etiqueta created.'
GO

CREATE TABLE TagCom(
	tag bigint NOT NULL,
	comentario bigint NOT NULL,

	CONSTRAINT [PK_TagComentario] PRIMARY KEY (tag, comentario),

	CONSTRAINT [FK_TagComentarioTag] FOREIGN KEY (tag)
		REFERENCES Etiqueta(tagID),

	CONSTRAINT [FK_TagComentarioComentario] FOREIGN KEY (comentario)
		REFERENCES Comentario(comID),
)
PRINT N'Table TagComentario created.'

CREATE TABLE Puntuacion(
	userId bigint NOT NULL,
	puntuacion float DEFAULT 0 NOT NULL,
	numValoraciones int DEFAULT 0 NOT NULL,

	 CONSTRAINT [PK_Puntuacion] PRIMARY KEY (userId),
	 CONSTRAINT [FK_PuntuacionUsuario] FOREIGN KEY (userId)
		REFERENCES Usuario(userID),
)
PRINT N'Table Puntuacion created.'

PRINT N'Done'

/* INSERT some default values */
INSERT INTO Usuario (username, pwd, nombre, apellidos, email) VALUES ('dsineiro','contraseña','david','sineiro','davisineba@gmail.com');
INSERT INTO Usuario (username, pwd, nombre, apellidos, email) VALUES ('diegogarcia','contraseñaa','diego','garcía','diegogarcialopez26@gmail.com');

INSERT INTO Producto (nombre, categoria, date, vendedor) VALUES ('mesa','mueble', '20120618 10:34:09 AM', 1);
INSERT INTO Producto (nombre, categoria, date, vendedor) VALUES ('silla','mueble', '20120619 10:34:09 AM', 2);
INSERT INTO Producto (nombre, categoria, date, vendedor) VALUES ('mesita de noche','mueble', '20120618 10:35:09 AM', 1);
INSERT INTO Producto (nombre, categoria, date, vendedor) VALUES ('cómoda','mueble', '20120619 10:36:09 AM', 1);
INSERT INTO Producto (nombre, categoria, date, vendedor) VALUES ('armario','mueble', '20120618 10:37:09 AM', 1);
INSERT INTO Producto (nombre, categoria, date, vendedor) VALUES ('vitrina','mueble', '20120619 10:38:09 AM', 1);
INSERT INTO Producto (nombre, categoria, date, vendedor) VALUES ('pierta de roble','mueble', '20120618 10:39:09 AM', 2);
INSERT INTO Producto (nombre, categoria, date, vendedor) VALUES ('silla de oficina','mueble', '20120619 10:31:09 AM', 2);
INSERT INTO Producto (nombre, categoria, date, vendedor) VALUES ('taburete','mueble', '20120618 10:34:15 AM', 2);
INSERT INTO Producto (nombre, categoria, date, vendedor) VALUES ('cheslón','mueble', '20120619 10:34:09 PM', 2);

INSERT INTO Comentario (usuario, producto, date, text) VALUES (1,2, '20120620 10:34:09 AM', 'Asquerosa, se rompio hoy.');
INSERT INTO Comentario (usuario, producto, date, text) VALUES (2,1, '20120621 10:34:09 AM', 'Me encanta! mi favorita');
INSERT INTO Comentario (usuario, producto, date, text) VALUES (3,1, '20120621 10:34:09 PM', 'No está mal');
INSERT INTO Comentario (usuario, producto, date, text) VALUES (3,2, '20120621 10:37:09 AM', 'Disfrutable');
INSERT INTO Comentario (usuario, producto, date, text) VALUES (1,2, '20120620 10:34:09 PM', 'La he arreglado y me vuelve a gusta, ¡increible!');
INSERT INTO Comentario (usuario, producto, date, text) VALUES (1,2, '20120621 10:34:09 PM', 'Olvidadlo se volvió a romper.');

INSERT INTO Valoracion (comprador,producto, puntuacion, comentario, date) VALUES (1,2,4,'muy majo', '20120619 10:34:09 PM');
INSERT INTO Valoracion (comprador,producto, puntuacion, comentario, date) VALUES (2,1,1,'muy malo', '20120619 10:35:09 PM');

INSERT INTO Etiqueta (nombre) VALUES ('guay');
INSERT INTO Etiqueta (nombre) VALUES ('noGuay');

INSERT INTO Puntuacion (userId, puntuacion, numValoraciones) VALUES (1,1,1);
INSERT INTO Puntuacion (userId, puntuacion, numValoraciones) VALUES (2,4,1);

GO