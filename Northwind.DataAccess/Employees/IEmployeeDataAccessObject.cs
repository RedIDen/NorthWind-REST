namespace Northwind.DataAccess.Employees
{
    /// <summary>
    /// Represents a DAO for Northwind employees.
    /// </summary>
    public interface IEmployeeDataAccessObject
    {
        /// <summary>
        /// Inserts the employee.
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <returns>Id of emloyee.</returns>
        Task<int> InsertEmployeeAsync(EmployeeTransferObject employee);

        /// <summary>
        /// Deletes the employee.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns>True if a employee is deleted; otherwise false.</returns>
        Task<bool> DeleteEmployeeAsync(int employeeId);

        /// <summary>
        /// Updates the employee.
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <returns>True if a employee is updated; otherwise false.</returns>
        Task<bool> UpdateEmployeeAsync(EmployeeTransferObject employee);

        /// <summary>
        /// Finds the employee.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns>A <see cref="EmployeeTransferObject"/> with specified identifier.</returns>
        Task<EmployeeTransferObject> FindEmployeeAsync(int employeeId);

        /// <summary>
        /// Selects the employee.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="limit">The limit.</param>
        /// <returns>A <see cref="List{T}"/> of <see cref="EmployeeTransferObject"/>.</returns>
        Task<IList<EmployeeTransferObject>> SelectEmployeeAsync(int offset, int limit);

        /// <summary>
        /// Selects the name of the employee by.
        /// </summary>
        /// <param name="employeeNames">The employee names.</param>
        /// <returns>A <see cref="List{T}"/> of <see cref="EmployeeTransferObject"/>.</returns>
        Task<IList<EmployeeTransferObject>> SelectEmployeeByNameAsync(ICollection<string> employeeNames);
    }
}