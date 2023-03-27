using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public interface IFunctional
    {
        Task InitAppData();

        Task CreateDefaultSuperAdmin();

       

        Task<string> UploadFile(List<IFormFile> files, Microsoft.AspNetCore.Hosting.IHostingEnvironment env, string uploadFolder);

    }
}
