using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Services.EntityFrameworkCore.Blogging.Entities
{
    /// <summary>
    /// BlogArticle class.
    /// </summary>
    [Index(nameof(Title), Name = "Title")]
    public class BlogArticle
    {
        /// <summary>
        /// Gets or sets the blog article identifier.
        /// </summary>
        /// <value>
        /// The blog article identifier.
        /// </value>
        [Key]
        [Column("BlogArticleID")]
        public int BlogArticleID { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [StringLength(40)]
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string? Text { get; set; }

        /// <summary>
        /// Gets or sets the date published.
        /// </summary>
        /// <value>
        /// The date published.
        /// </value>
        public DateTime? DatePublished { get; set; }

        /// <summary>
        /// Gets or sets the publisher identifier.
        /// </summary>
        /// <value>
        /// The publisher identifier.
        /// </value>
        public int PublisherId { get; set; }
    }
}
