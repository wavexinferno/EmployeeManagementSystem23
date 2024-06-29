CREATE DATABASE EmployeeManagement;
GO
CREATE TABLE Departments (
    DepartmentId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE Roles (
    RoleId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE Employees (
    EmployeeId INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    DepartmentId INT NOT NULL,
    RoleId INT NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(15) NULL,
    FOREIGN KEY (DepartmentId) REFERENCES Departments(DepartmentId),
    FOREIGN KEY (RoleId) REFERENCES Roles(RoleId)
);
GO

CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY,
    Username NVARCHAR(50) NOT NULL,
    Password NVARCHAR(50) NOT NULL
);

INSERT INTO Departments (Name)
VALUES ('Human Resources'), ('Engineering'), ('Marketing');
GO

INSERT INTO Roles (Name)
VALUES ('Manager'), ('Developer'), ('Designer');
GO

INSERT INTO Employees (FirstName, LastName, DepartmentId, RoleId, Email, Phone)
VALUES 
('John', 'Doe', 1, 1, 'john.doe@example.com', '123-456-7890'),
('Jane', 'Smith', 2, 2, 'jane.smith@example.com', '098-765-4321');
GO
