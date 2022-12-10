using Northwind.Services.Models;

namespace Northwind.Services
{
    /// <summary>
    /// IEmployeeManagementService interface.
    /// </summary>
    public interface IEmployeeManagementService
    {
        /// <summary>
        /// Creates the employee.
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <returns>An identifier of a created employee.</returns>
        Task<int> CreateEmployeeAsync(Employee employee);

        /// <summary>
        /// Shows the employees.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="limit">The limit.</param>
        /// <returns>A <see cref="IList{T}"/> of <see cref="Employee"/>.</returns>
        Task<IList<Employee>> ShowEmployeesAsync(int offset, int limit);

        /// <summary>
        /// Tries the show employee.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="employee">The employee.</param>
        /// <returns>Returns true if a employee is returned; otherwise false.</returns>
        bool TryShowEmployee(int employeeId, out Employee employee);

        /// <summary>
        /// Destroys the employee.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns>True if a employee is destroyed; otherwise false.</returns>
        Task<bool> DestroyEmployeeAsync(int employeeId);

        /// <summary>
        /// Updates the employee.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="employee">The employee.</param>
        /// <returns>True if a employee is updated; otherwise false.</returns>
        Task<bool> UpdateEmployeeAsync(int employeeId, Employee employee);
    }
}
