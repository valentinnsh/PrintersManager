USE [master];
GO
	
CREATE TABLE printers (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(100) NOT NULL,
    connection_type INT CHECK (connection_type IN (0, 1)) NOT NULL,
    mac VARCHAR(17) NULL -- PG has macaddr datatype, didnt find anything like that for sql server, so decided to settle down with varchar
);
GO

INSERT INTO [printers] (name, connection_type, mac)
VALUES 
(N'Папирус', 0, NULL),
(N'Камень', 1, 'f0:6b:f1:ef:fa:c2'),
(N'Бумага', 0, NULL);
GO
