USE [master];
GO

CREATE TABLE printers (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(100) NOT NULL,
    connection_type INT CHECK (connection_type IN (0, 1)) NOT NULL,
    mac VARCHAR(17) NULL
);
GO

CREATE TABLE branches (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(100) NOT NULL,
    location NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE installations (
    id INT IDENTITY(1,1) PRIMARY KEY,
    external_id UNIQUEIDENTIFIER NOT NULL,
    local_name NVARCHAR(100) NOT NULL,
    local_number TINYINT NOT NULL,
    is_default BIT NOT NULL,
    printer_id INT NOT NULL,
    branch_id INT NOT NULL,
    
    CONSTRAINT FK_Printer_Installation FOREIGN KEY (printer_id) REFERENCES printers(id),
    CONSTRAINT FK_Branch_Installation FOREIGN KEY (branch_id) REFERENCES branches(id),
);
GO

SET QUOTED_IDENTIFIER ON;
GO

CREATE UNIQUE INDEX UQ_Default_Installation ON installations(branch_id) WHERE is_default = 1;
GO

CREATE TABLE employees (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(100) NOT NULL,
    branch_id INT NOT NULL,
    
    CONSTRAINT FK_Branch_Employees FOREIGN KEY (branch_id) REFERENCES branches(id)
);
GO

CREATE TABLE sessions (
    id BIGINT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(100) NOT NULL,
    employee_id INT NOT NULL,
    installation_id INT NOT NULL,
    pages SMALLINT NOT NULL,
    status TINYINT NOT NULL,
    
    CONSTRAINT FK_Employee_Session FOREIGN KEY (employee_id) REFERENCES employees(id),
    CONSTRAINT FK_Installation_Session FOREIGN KEY (installation_id) REFERENCES installations(id)
);
GO

INSERT INTO [printers] (name, connection_type, mac)
VALUES
    (N'Папирус', 0, NULL),
    (N'Камень', 1, 'f0:6b:f1:ef:fa:c2'),
    (N'Бумага', 0, NULL);
GO

INSERT INTO [branches] (name, location)
VALUES
    (N'Тридевятое царство', N'За семью морями, за семью горами'),
    (N'Дремучий лес', N'Сибирь'),
    (N'Луна', N'Околоземная орбита');
GO

INSERT INTO [employees] (name, branch_id)
VALUES
    (N'Царь', 1),
    (N'Добрыня', 1),
    (N'Яга', 2),
    (N'Кощей', 2),
    (N'Копатыч', 3),
    (N'Копатыч', 3);
GO

INSERT INTO [installations] (local_name, local_number, is_default, printer_id, branch_id, external_id)
VALUES
    (N'Дворец', 1, 1, 1, 1, NEWID()),
    (N'Конюшни', 2, 0, 3, 1, NEWID()),
    (N'Оружейная', 3, 0, 3, 1, NEWID()),
    (N'Кратер', 1, 1, 2, 3, NEWID()),
    (N'Избушка', 3, 0, 3, 2, NEWID()),
    (N'Топи', 2, 1, 1, 2, NEWID());
GO
