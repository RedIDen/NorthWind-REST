using Northwind.DataAccess;
using Northwind.DataAccess.Employees;
using Northwind.Services.Models;

namespace Northwind.Services.DataAccess
{
    /// <summary>
    /// EmployeeManagementDataAccessService class.
    /// </summary>
    /// <seealso cref="Northwind.Services.IEmployeeManagementService" />
    public class EmployeeManagementDataAccessService : IEmployeeManagementService
    {
        private readonly NorthwindDataAccessFactory? _factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeManagementDataAccessService"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public EmployeeManagementDataAccessService(NorthwindDataAccessFactory factory)
            => _factory = factory;

        /// <inheritdoc/>
        public async Task<int> CreateEmployeeAsync(Employee employee)
        {
            _ = employee is null ? throw new ArgumentNullException($"{nameof(employee)} is null") : employee;

            var emp = GetEmployeeTransferObject(employee);

            return await _factory.GetEmployeeDataAccessObject().InsertEmployeeAsync(emp);
        }

        /// <inheritdoc/>
        public async Task<bool> DestroyEmployeeAsync(int employeeId)
        {
            var access = _factory.GetEmployeeDataAccessObject();

            try
            {
                var emplouee = await access.FindEmployeeAsync(employeeId);

                if (emplouee is null)
                {
                    return false;
                }

                await access.DeleteEmployeeAsync(employeeId);

                return true;
            }
            catch (EmployeeNotFoundException)
            {
                return false;
            }
        }

        /// <inheritdoc/>
        public async Task<IList<Employee>> ShowEmployeesAsync(int offset, int limit)
        {
            var list = await _factory.GetEmployeeDataAccessObject().SelectEmployeeAsync(offset, limit);

            return list.Select(employee =>
            {
                return GetEmloyee(employee);
            }).ToList();
        }

        /// <inheritdoc/>
        public bool TryShowEmployee(int employeeId, out Employee employee)
        {
            try
            {
                var emp = _factory.GetEmployeeDataAccessObject().FindEmployeeAsync(employeeId);
                emp.Wait();
                var em = emp.Result;

                if (em is null)
                {
                    employee = null;

                    return false;
                }

                employee = GetEmloyee(em);

                return true;
            }
            catch (EmployeeNotFoundException)
            {
                employee = null;
                return false;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateEmployeeAsync(int employeeId, Employee employee)
        {
            var emp = GetEmployeeTransferObject(employee);

            return await _factory.GetEmployeeDataAccessObject().UpdateEmployeeAsync(emp);
        }

        private static EmployeeTransferObject GetEmployeeTransferObject(Employee employee)
            => new EmployeeTransferObject
            {
                Id = employee.Id,
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                Title = employee.Title,
                TitleOfCourtesy = employee.TitleOfCourtesy,
                BirthDate = employee.BirthDate,
                HireDate = employee.HireDate,
                Address = employee.Address,
                City = employee.City,
                Region = employee.Region,
                PostalCode = employee.PostalCode,
                Country = employee.Country,
                HomePhone = employee.HomePhone,
                Extension = employee.Extension,
                Photo = employee.Photo,
                Notes = employee.Notes,
                ReportsTo = employee.ReportsTo,
                PhotoPath = employee.PhotoPath,
            };

        private static Employee GetEmloyee(EmployeeTransferObject employee)
            => new Employee
            {
                Id = employee.Id,
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                Title = employee.Title,
                TitleOfCourtesy = employee.TitleOfCourtesy,
                BirthDate = employee.BirthDate,
                HireDate = employee.HireDate,
                Address = employee.Address,
                City = employee.City,
                Region = employee.Region,
                PostalCode = employee.PostalCode,
                Country = employee.Country,
                HomePhone = employee.HomePhone,
                Extension = employee.Extension,
                Photo = employee.Photo,
                Notes = employee.Notes,
                ReportsTo = employee.ReportsTo,
                PhotoPath = employee.PhotoPath,
            };
    }
}
