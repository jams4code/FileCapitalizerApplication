using alpvisionapp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alpvisionapp.Domain.Entities
{
    public class ImportedFile : AuditableEntity
    {
        public Guid Id { get; set; }
        public string InputFileName { get; set; }
        public int FileSize { get; set; }
    }
}
