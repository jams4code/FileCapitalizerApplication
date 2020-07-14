using alpvisionapp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alpvisionapp.Infrastructure.Persistence.Configurations
{
    public class ImportedFileConfiguration: IEntityTypeConfiguration<ImportedFile>
    {
        public void Configure(EntityTypeBuilder<ImportedFile> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(u => u.Id).HasDefaultValueSql("newsequentialid()");
        }
    }
}
