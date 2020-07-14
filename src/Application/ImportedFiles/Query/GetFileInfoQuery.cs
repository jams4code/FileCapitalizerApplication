using alpvisionapp.Application.Common.Interfaces;
using alpvisionapp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace alpvisionapp.Application.ImportedFiles.Query
{
    public class GetFileInfoQuery : IRequest<ICollection<ImportedFile>>
    {

        public class GetFileInfoQueryHandler : IRequestHandler<GetFileInfoQuery, ICollection<ImportedFile>>
        {
            private readonly IApplicationDbContext _context;

            public GetFileInfoQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<ICollection<ImportedFile>> Handle(GetFileInfoQuery request, CancellationToken cancellationToken)
            {
                return await _context.ImportedFiles.ToListAsync();
            }
        }
    }
}
