using Microsoft.EntityFrameworkCore;
using Northwind.Services.EntityFrameworkCore.Blogging.Entities;

namespace Northwind.Services.EntityFrameworkCore.Blogging.Context
{
    public partial class BloggingContext : DbContext
    {
        public BloggingContext()
        {
        }

        public BloggingContext(DbContextOptions<BloggingContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the blog articles.
        /// </summary>
        /// <value>
        /// The blog articles.
        /// </value>
        public virtual DbSet<BlogArticle> BlogArticles { get; set; } = null!;

        /// <summary>
        /// Gets or sets the blog comments.
        /// </summary>
        /// <value>
        /// The blog comments.
        /// </value>
        public virtual DbSet<BlogComment> BlogComments { get; set; } = null!;

        /// <summary>
        /// Gets or sets the blog article products.
        /// </summary>
        /// <value>
        /// The blog article products.
        /// </value>
        public virtual DbSet<BlogArticleProduct> BlogArticleProducts { get; set; } = null!;

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        /// <remarks>
        /// <para>
        /// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        /// then this method will not be run.
        /// </para>
        /// <para>
        /// See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see> for more information.
        /// </para>
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogArticle>(entity =>
            {
                entity.Property(e => e.Text).HasDefaultValueSql("((0))");

                entity.Property(e => e.Title).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<BlogArticleProduct>();

            modelBuilder.Entity<BlogComment>();

            this.OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
