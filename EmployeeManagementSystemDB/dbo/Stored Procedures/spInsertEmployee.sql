-- Create the insert stored procedure
CREATE PROCEDURE spInsertEmployee
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @DepartmentId INT,
    @RoleId INT,
    @Email NVARCHAR(100),
    @Phone NVARCHAR(15)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Employees (FirstName, LastName, DepartmentId, RoleId, Email, Phone)
    VALUES (@FirstName, @LastName, @DepartmentId, @RoleId, @Email, @Phone);
END
