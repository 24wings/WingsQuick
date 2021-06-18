using Aliyun.OSS;
using Aliyun.OSS.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wings.Examples.UseCase.Server.Controllers.Admin.Common
{
    public class UploadDto
    {
        public IFormFile Avatar { get; set; }
    }

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CommonController : ControllerBase
    {
        static OssClient client = new OssClient("oss-cn-beijing.aliyuncs.com", "LTAI4GKc00000h5w2H9GrATRYn665", "B2RE000000L70UhtzEqWd0sAk83U4dKN8SUA");


        public CommonController()
        {

        }
        [HttpPost]
        public object Upload([FromForm] UploadDto dto)
        {
            var bucketName = "basewe-manage";

            var now = DateTime.Now;
            var dir = now.Year + "-" + now.Month.ToString().PadLeft(2, '0') + "-" + now.Date.ToString().PadLeft(2, '0');
            var fileName = Guid.NewGuid().ToString() + ".png";
            var key = dir + "/" + fileName;
            PutObjectFromString(bucketName, key, dto.Avatar.OpenReadStream());

            return new  {Url= "http://basewe-manage.oss-cn-beijing.aliyuncs.com/" + key };

        }


        public static void PutObjectFromString(string bucketName, string fileName, Stream stream)
        {

            try
            {

                client.PutObject(bucketName, fileName, stream);
                Console.WriteLine("Put object:{0} succeeded", fileName);
            }
            catch (OssException ex)
            {
                Console.WriteLine("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}",
                    ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed with error info: {0}", ex.Message);
            }
        }

    }
}
