using alpvisionapp.Application.Common.Interfaces;
using alpvisionapp.Application.ImportedFiles.Command;
using alpvisionapp.Application.ImportedFiles.Query;
using alpvisionapp.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace alpvisionapp.WebUI.Controllers
{
    public class FileController : ApiController
    {
        [HttpPost]
        public async Task<FileResult> Import(IFormFile FormFile)
        {
            //Use of the Mediatr design pattern
            ImportFileCommand command = new ImportFileCommand()
            {
                FormFile = FormFile
            };
            ExportDataFile fileToExport = await Mediator.Send(command);
            HttpContext.Response.ContentType = fileToExport.ContentType;
            HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
            FileResult fileContentResult = fileToExport.ExportVmToFileResult();
            return fileContentResult;
        }
        [HttpGet]
        public async Task<ActionResult<ICollection<ImportedFile>>> Get()
        {
            return Ok(await Mediator.Send(new GetFileInfoQuery()));
        }

    }
}
