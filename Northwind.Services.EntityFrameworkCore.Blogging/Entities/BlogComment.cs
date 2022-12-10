using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Services.EntityFrameworkCore.Blogging.Entities
{
    /// <summary>
    /// BlogComment class.
    /// </summary>
    public class BlogComment
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the publisher identifier.
        /// </summary>
        /// <value>
        /// The publisher identifier.
        /// </value>
        public int PublisherID { get; set; }

        /// <summary>
        /// Gets or sets the article identifier.
        /// </summary>
        /// <value>
        /// The article identifier.
        /// </value>
        public int ArticleId { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        public string? Comment { get; set; }
    }
}
