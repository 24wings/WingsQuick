using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Wings.Examples.UseCase.Server.Controllers.Admin.Common
{
    public class UploadDto
    {
        public IFormFile Avatar { get; set; }
    }

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CommonController:ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public CommonController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task<string> Upload([FromForm] UploadDto dto)
        {
            var fileInfo = new FileInfo(Path.Combine(_hostingEnvironment.ContentRootPath, "1.png"));

            var fileStream = fileInfo.Open(FileMode.Create, FileAccess.ReadWrite);
            await dto.Avatar.CopyToAsync(fileStream);

            fileStream.Close();
            return "aa";

        }
    }
}
