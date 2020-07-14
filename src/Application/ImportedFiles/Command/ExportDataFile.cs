using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alpvisionapp.Application.ImportedFiles.Command
{
    public class ExportDataFile
    {
        public string FileName { get; set; }

        public string ContentType { get; set; }

        public byte[] Content { get; set; }
        public FileResult ExportVmToFileResult()
        {
            return new FileContentResult(Content, ContentType) { FileDownloadName = FileName };
        }
    }
}
