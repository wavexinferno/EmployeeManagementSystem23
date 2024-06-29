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