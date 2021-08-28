using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebServer.Db.Context;

namespace WebServer.Actions
{
    public sealed class EnsureFilesToUpload
    {
        public class ResultViewModel
        {
            [FromBody] public Body FromBody { get; set; }

            public class Body
            {
                public string[] FilesToUpload { get; set; }
            }
        }

        public class Request : IRequest<ResultViewModel>
        {
            public string[] FilesToEnsure { get; set; }
        }

        public class Handler : IRequestHandler<Request, ResultViewModel>
        {
            private readonly PhotosDbContext _dbContext;

            public Handler(PhotosDbContext dbContext)
            {
                _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            }
            public async Task<ResultViewModel> Handle(Request request, CancellationToken cancellationToken)
            {
                _dbContext.MediasFileInfos.AsNoTracking().Where(mf=>request.FilesToEnsure.Contains(mf.))
            }
        }
    }
}