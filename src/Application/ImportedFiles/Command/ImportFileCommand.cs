using alpvisionapp.Application.Common.Interfaces;
using alpvisionapp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace alpvisionapp.Application.ImportedFiles.Command
{
    public class ImportFileCommand : IRequest<ExportDataFile>
    {
        public IFormFile FormFile { get; set; }
        public class ImportFileCommandHandler : IRequestHandler<ImportFileCommand, ExportDataFile>
        {
            private readonly IApplicationDbContext _context;
            public ImportFileCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<ExportDataFile> Handle(ImportFileCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    IFormFile file = request.FormFile;
                    string folderName = Path.Combine("Ressources", "test");
                    string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                    if(file.Length > 0)
                    {
                        //To Save the file in the server
                        var fileName = file.FileName.Replace(".txt", DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".txt");
                        var fullPath = Path.Combine(pathToSave, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream, cancellationToken); //Use of multitheading to allow concurrent use of the app.
                            
                        }
                        //Creation of a new file with all characters in uppercase
                        //We read the first file line by line asynchrously to allow multithreading and to not surcharge the memory
                        //Then we write them in the same time in another file
                        string dbPath = Path.Combine(folderName, fileName);
                        string newFileName = fileName.Insert(0, "Capital");
                        string newFileDbPath = Path.Combine(folderName, newFileName);
                        StreamReader reader = File.OpenText(dbPath); 
                        StreamWriter writer = File.CreateText(newFileDbPath);
                        string line;
                        do
                        {
                            line = await reader.ReadLineAsync();
                            if (line != null)
                            {
                                writer.WriteLine(line.ToUpper());
                            }
                        } while (line != null);
                        reader.Close();
                        writer.Close();


                        ExportDataFile fileToExport = new ExportDataFile()
                        {
                            FileName = newFileName,
                            ContentType = "text/plain",
                            //Writting all the content of the new file in the file object asynchronously
                            Content = await File.ReadAllBytesAsync(newFileDbPath, cancellationToken)
                        };
                        ImportedFile myNewFile = new ImportedFile() { Id = Guid.NewGuid(), Created = DateTime.Now, CreatedBy = "AlpVisionTeam", FileSize = Convert.ToInt32(file.Length), InputFileName = file.FileName };
                        await _context.ImportedFiles.AddAsync(myNewFile, cancellationToken);
                        await _context.SaveChangesAsync(cancellationToken);
                        return fileToExport;
                    }
                    return null;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                    throw ex;
                }
            }
        }
    }
}
