using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;

namespace LibrameSample.Mvc.Models
{
    public class SampleDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // 博客
            builder.Entity<Blog>(b =>
            {
                b.ForRelational().Table("Blogs");

                b.Property(p => p.Id).GenerateValueOnAdd();
                b.Property(p => p.Title).Required().MaxLength(50);
                b.Property(p => p.Description);
                b.Property(p => p.Pubdate).Required();

                b.Key(k => k.Id);
            });

            base.OnModelCreating(builder);
        }
    }
}