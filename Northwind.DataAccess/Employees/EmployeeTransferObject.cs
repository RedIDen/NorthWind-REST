using System.Diagnostics;

namespace Northwind.DataAccess.Employees
{
    /// <summary>
    /// Represents a TO for Northwind employees.
    /// </summary>
    [DebuggerDisplay("Id={Id}, Name={Name}")]
    public class EmployeeTransferObject
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the title of courtesy.
        /// </summary>
        /// <value>
        /// The title of courtesy.
        /// </value>
        public string? TitleOfCourtesy { get; set; }

        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        /// <value>
        /// The birth date.
        /// </value>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Gets or sets the hire date.
        /// </summary>
        /// <value>
        /// The hire date.
        /// </value>
        public DateTime? HireDate { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public string? Address { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        public string? City { get; set; }

        /// <summary>
        /// Gets or sets the region.
        /// </summary>
        /// <value>
        /// The region.
        /// </value>
        public string? Region { get; set; }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        /// <value>
        /// The postal code.
        /// </value>
        public string? PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        public string? Country { get; set; }

        /// <summary>
        /// Gets or sets the home phone.
        /// </summary>
        /// <value>
        /// The home phone.
        /// </value>
        public string? HomePhone { get; set; }

        /// <summary>
        /// Gets or sets the extension.
        /// </summary>
        /// <value>
        /// The extension.
        /// </value>
        public string? Extension { get; set; }

        /// <summary>
        /// Gets or sets the photo.
        /// </summary>
        /// <value>
        /// The photo.
        /// </value>
        public byte[]? Photo { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>
        public string? Notes { get; set; }

        /// <summary>
        /// Gets or sets the reports to.
        /// </summary>
        /// <value>
        /// The reports to.
        /// </value>
        public int? ReportsTo { get; set; }

        /// <summary>
        /// Gets or sets the photo path.
        /// </summary>
        /// <value>
        /// The photo path.
        /// </value>
        public string? PhotoPath { get; set; }
    }
}