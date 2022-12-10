using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace Northwind.DataAccess.Employees
{
    /// <summary>
    /// Represents a SQL Server-tailored DAO for Northwind products.
    /// </summary>
    public sealed class EmployeeSqlServerDataAccessObject : IEmployeeDataAccessObject
    {
        private readonly SqlConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductCategorySqlServerDataAccessObject"/> class.
        /// </summary>
        /// <param name="connection">A <see cref="SqlConnection"/>.</param>
        public EmployeeSqlServerDataAccessObject(SqlConnection connection)
        {
            this.connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteEmployeeAsync(int employeeId)
        {
            if (employeeId <= 0)
            {
                throw new ArgumentException("Must be greater than zero.", nameof(employeeId));
            }

            await using var sqlCommand = new SqlCommand("DeleteEmployee", this.connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            const string empId = "@employeeID";
            sqlCommand.Parameters.Add(empId, SqlDbType.Int);
            sqlCommand.Parameters[empId].Value = employeeId;

            await this.connection.OpenAsync();
            var result = await sqlCommand.ExecuteScalarAsync();
            return ((int)result) > 0;
        }

        /// <inheritdoc/>
        public async Task<EmployeeTransferObject> FindEmployeeAsync(int employeeId)
        {
            if (employeeId <= 0)
            {
                throw new ArgumentException("Must be greater than zero.", nameof(employeeId));
            }

            await using var sqlCommand = new SqlCommand("FindEmployee", this.connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            const string empId = "@employeeID";
            sqlCommand.Parameters.Add(empId, SqlDbType.Int);
            sqlCommand.Parameters[empId].Value = employeeId;

            await this.connection.OpenAsync();
            var result = await sqlCommand.ExecuteReaderAsync();

            if (!result.HasRows)
            {
                throw new EmployeeNotFoundException(employeeId);
            }

            return CreateEmployee(result);
        }

        /// <inheritdoc/>
        public async Task<int> InsertEmployeeAsync(EmployeeTransferObject employee)
        {
            if (employee is null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            const string commandText =
@"INSERT INTO dbo.Employees (LastName, FirstName, Title, TitleOfCourtesy, BirthDate, HireDate, Address, City, Region, PostalCode, Country, HomePhone, Extension, Photo, Notes, ReportsTo, PhotoPath) OUTPUT Inserted.EmployeeID
VALUES (@lastName, @firstName, @title, @titleOfCourtesy, @birthDate, @hireDate, @address, @city, @region, @postalCode, @country, @homePhone, @extension, @photo, @notes, @reportsTo, @photoPath)";

            using var command = new SqlCommand(commandText, this.connection);
            AddSqlParameters(employee, command);

            var id = await command.ExecuteScalarAsync();
            return (int)id;
        }

        /// <inheritdoc/>
        public async Task<IList<EmployeeTransferObject>> SelectEmployeeAsync(int offset, int limit)
        {
            if (offset < 0)
            {
                throw new ArgumentException("Must be greater than zero or equals zero.", nameof(offset));
            }

            if (limit < 1)
            {
                throw new ArgumentException("Must be greater than zero.", nameof(limit));
            }

            await using var sqlCommand = new SqlCommand("SelectEmployee", this.connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            const string offsetSql = "@offset";
            const string limitSql = "@limit";

            sqlCommand.Parameters.Add(offsetSql, SqlDbType.Int);
            sqlCommand.Parameters.Add(limitSql, SqlDbType.Int);
            sqlCommand.Parameters[offsetSql].Value = offset;
            sqlCommand.Parameters[limitSql].Value = limit;

            await this.connection.OpenAsync();
            var result = await sqlCommand.ExecuteReaderAsync();

            return await this.ExecuteReaderAsync(result);
        }

        /// <inheritdoc/>
        public async Task<IList<EmployeeTransferObject>> SelectEmployeeByNameAsync(ICollection<string> employeeNames)
        {
            if (employeeNames == null)
            {
                throw new ArgumentNullException(nameof(employeeNames));
            }

            if (employeeNames.Count < 1)
            {
                throw new ArgumentException("Collection is empty.", nameof(employeeNames));
            }

            const string commandTemplate =
@"SELECT p.EmployeeID, p.LastName, p.FirstName, p.Title, p.TitleOfCourtesy, p.BirthDate, p.HireDate, p.Address, p.City, p.Region, p.PostalCode, p.Country, p.HomePhone, p.Extension, p.Photo, p.Notes, p.ReportsTo, p.PhotoPath FROM dbo.Employees as p
WHERE p.LastName in ('{0}')
ORDER BY p.EmployeeID";

            string commandText = string.Format(CultureInfo.CurrentCulture, commandTemplate, string.Join("', '", employeeNames));
            await using var sqlCommand = new SqlCommand(commandText, this.connection);

            await this.connection.OpenAsync();
            var result = await sqlCommand.ExecuteReaderAsync();

            return await this.ExecuteReaderAsync(result);
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateEmployeeAsync(EmployeeTransferObject employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            const string commandText =
@"UPDATE dbo.Employees
SET LastName = @lastName, FirstName = @firstName, Title = @title, TitleOfCourtesy = @titleOfCourtesy, BirthDate = @birthDate, HireDate = @hireDate, Address = @address, City = @city, Region = @region, PostalCode = @postalCode, Country = @country, HomePhone = @homePhone, Extension = @extension, Photo = @photo, Notes = @notes, ReportsTo = @reportsTo, PhotoPath = @photoPath
WHERE EmployeeID = @employeeID
SELECT @@ROWCOUNT";

            using var command = new SqlCommand(commandText, this.connection);
            AddSqlParameters(employee, command);

            const string productId = "@employeeID";
            command.Parameters.Add(productId, SqlDbType.Int);
            command.Parameters[productId].Value = employee.Id;

            var result = await command.ExecuteScalarAsync();
            return ((int)result) > 0;
        }

        private static void AddSqlParameters(EmployeeTransferObject employee, SqlCommand command)
        {
            const string employeeLastNameParameter = "@lastName";
            command.Parameters.Add(employeeLastNameParameter, SqlDbType.NVarChar, 20);
            command.Parameters[employeeLastNameParameter].Value = employee.LastName;

            const string employeeFirstNameParameter = "@firstName";
            command.Parameters.Add(employeeFirstNameParameter, SqlDbType.NVarChar, 10);
            command.Parameters[employeeFirstNameParameter].Value = employee.FirstName;

            const string titleParameter = "@title";
            command.Parameters.Add(titleParameter, SqlDbType.NVarChar, 30);
            command.Parameters[titleParameter].IsNullable = true;
            command.Parameters[titleParameter].Value = employee.Title != null ? employee.Title : DBNull.Value;

            const string titleOfCourtesyParameter = "@titleOfCourtesy";
            command.Parameters.Add(titleOfCourtesyParameter, SqlDbType.NVarChar, 25);
            command.Parameters[titleOfCourtesyParameter].IsNullable = true;
            command.Parameters[titleOfCourtesyParameter].Value = employee.TitleOfCourtesy != null ? employee.TitleOfCourtesy : DBNull.Value;

            const string birthDateParameter = "@birthDate";
            command.Parameters.Add(birthDateParameter, SqlDbType.DateTime, 8);
            command.Parameters[birthDateParameter].IsNullable = true;
            command.Parameters[birthDateParameter].Value = employee.BirthDate != null ? employee.BirthDate : DBNull.Value;

            const string hireDateParameter = "@hireDate";
            command.Parameters.Add(hireDateParameter, SqlDbType.DateTime, 8);
            command.Parameters[hireDateParameter].IsNullable = true;
            command.Parameters[hireDateParameter].Value = employee.HireDate != null ? employee.HireDate : DBNull.Value;

            const string addressParameter = "@address";
            command.Parameters.Add(addressParameter, SqlDbType.NVarChar, 60);
            command.Parameters[addressParameter].IsNullable = true;
            command.Parameters[addressParameter].Value = employee.Address != null ? employee.Address : DBNull.Value;

            const string cityParameter = "@city";
            command.Parameters.Add(cityParameter, SqlDbType.NVarChar, 15);
            command.Parameters[cityParameter].IsNullable = true;
            command.Parameters[cityParameter].Value = employee.City != null ? employee.City : DBNull.Value;

            const string regionParameter = "@region";
            command.Parameters.Add(regionParameter, SqlDbType.NVarChar, 15);
            command.Parameters[regionParameter].IsNullable = true;
            command.Parameters[regionParameter].Value = employee.Region != null ? employee.Region : DBNull.Value;

            const string postalCodeParameter = "@postalCode";
            command.Parameters.Add(postalCodeParameter, SqlDbType.NVarChar, 10);
            command.Parameters[postalCodeParameter].IsNullable = true;
            command.Parameters[postalCodeParameter].Value = employee.PostalCode != null ? employee.PostalCode : DBNull.Value;

            const string countryParameter = "@country";
            command.Parameters.Add(countryParameter, SqlDbType.NVarChar, 15);
            command.Parameters[countryParameter].IsNullable = true;
            command.Parameters[countryParameter].Value = employee.Country != null ? employee.Country : DBNull.Value;

            const string homePhoneParameter = "@homePhone";
            command.Parameters.Add(homePhoneParameter, SqlDbType.NVarChar, 24);
            command.Parameters[homePhoneParameter].IsNullable = true;
            command.Parameters[homePhoneParameter].Value = employee.HomePhone != null ? employee.HomePhone : DBNull.Value;

            const string extensionParameter = "@extension";
            command.Parameters.Add(extensionParameter, SqlDbType.NVarChar, 4);
            command.Parameters[extensionParameter].IsNullable = true;
            command.Parameters[extensionParameter].Value = employee.Extension != null ? employee.Extension : DBNull.Value;

            const string photoParameter = "@photo";
            command.Parameters.Add(photoParameter, SqlDbType.Image);
            command.Parameters[photoParameter].IsNullable = true;
            command.Parameters[photoParameter].Value = employee.Photo != null ? employee.Photo : DBNull.Value;

            const string notesParameter = "@notes";
            command.Parameters.Add(notesParameter, SqlDbType.NText);
            command.Parameters[notesParameter].IsNullable = true;
            command.Parameters[notesParameter].Value = employee.Notes != null ? employee.Notes : DBNull.Value;

            const string reportsToParameter = "@reportsTo";
            command.Parameters.Add(reportsToParameter, SqlDbType.Int);
            command.Parameters[reportsToParameter].IsNullable = true;
            command.Parameters[reportsToParameter].Value = employee.ReportsTo != null ? employee.ReportsTo : DBNull.Value;

            const string photoPathParameter = "@photoPath";
            command.Parameters.Add(photoPathParameter, SqlDbType.NVarChar);
            command.Parameters[photoPathParameter].IsNullable = true;
            command.Parameters[photoPathParameter].Value = employee.PhotoPath != null ? employee.PhotoPath : DBNull.Value;
        }

        private static EmployeeTransferObject CreateEmployee(SqlDataReader reader)
        {
            var id = (int)reader["EmployeeID"];
            var lastName = (string)reader["LastName"];
            var firstName = (string)reader["FirstName"];
            var title = reader["Title"] != DBNull.Value ? (string)reader["Title"] : null;
            var titleOfCourtesy = reader["TitleOfCourtesy"] != DBNull.Value ? (string)reader["TitleOfCourtesy"] : null;
            DateTime? birthDate = reader["BirthDate"] != DBNull.Value ? (DateTime)reader["BirthDate"] : null;
            DateTime? hireDate = reader["HireDate"] != DBNull.Value ? (DateTime)reader["HireDate"] : null;
            var address = reader["Address"] != DBNull.Value ? (string)reader["Address"] : null;
            var city = reader["City"] != DBNull.Value ? (string)reader["City"] : null;
            var region = reader["Region"] != DBNull.Value ? (string)reader["Region"] : null;
            var postalCode = reader["PostalCode"] != DBNull.Value ? (string)reader["PostalCode"] : null;
            var country = reader["Country"] != DBNull.Value ? (string)reader["Country"] : null;
            var homePhone = reader["HomePhone"] != DBNull.Value ? (string)reader["HomePhone"] : null;
            var extension = reader["Extension"] != DBNull.Value ? (string)reader["Extension"] : null;
            var photo = reader["Photo"] != DBNull.Value ? (byte[])reader["Photo"] : null;
            var notes = reader["Notes"] != DBNull.Value ? (string)reader["Notes"] : null;
            int? reportsTo = reader["ReportsTo"] != DBNull.Value ? (int)reader["ReportsTo"] : null;
            var photoPath = reader["PhotoPath"] != DBNull.Value ? (string)reader["PhotoPath"] : null;

            return new EmployeeTransferObject
            {
                Id = id,
                LastName = lastName,
                FirstName = firstName,
                Title = title,
                TitleOfCourtesy = titleOfCourtesy,
                BirthDate = birthDate,
                HireDate = hireDate,
                Address = address,
                City = city,
                Region = region,
                PostalCode = postalCode,
                Country = country,
                HomePhone = homePhone,
                Extension = extension,
                Photo = photo,
                Notes = notes,
                ReportsTo = reportsTo,
                PhotoPath = photoPath,
            };
        }

        private async Task<IList<EmployeeTransferObject>> ExecuteReaderAsync(SqlDataReader reader)
        {
            var products = new List<EmployeeTransferObject>();

            while (reader.Read())
            {
                products.Add(CreateEmployee(reader));
            }
            
            return products;
        }
    }
}