-- Create the update stored procedure
CREATE PROCEDURE spUpdateEmployee
    @EmployeeId INT,
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @DepartmentId INT,
    @RoleId INT,
    @Email NVARCHAR(100),
    @Phone NVARCHAR(15)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Employees
    SET FirstName = @FirstName,
        LastName = @LastName,
        DepartmentId = @DepartmentId,
        RoleId = @RoleId,
        Email = @Email,
        Phone = @Phone
    WHERE EmployeeId = @EmployeeId;
END
