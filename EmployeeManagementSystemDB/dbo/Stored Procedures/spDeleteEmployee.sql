-- Create the stored procedure
CREATE PROCEDURE spDeleteEmployee
    @EmployeeId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    DELETE FROM Employees
    WHERE EmployeeId = @EmployeeId;
END
