-- Create the stored procedure to read all employees
CREATE PROCEDURE spGetAllEmployees
AS
BEGIN
    SET NOCOUNT ON;

    SELECT EmployeeId, FirstName, LastName, DepartmentId, RoleId, Email, Phone
    FROM Employees;
END
