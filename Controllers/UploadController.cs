using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspnetCoreStudy.Controllers
{
    public class UploadController : Controller
    {
        private readonly IHostingEnvironment _environment;
        public UploadController(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        //Route를 사용하면 example.com/upload/ImageUpload 에서
        //example.com/api/upload 로 단순화됨
        [HttpPost, Route("api/upload")]
        public async Task<IActionResult> ImageUpload(IFormFile file)
        {
            // # 이미지나 파일을 업로드 할 떄 필요한 구성
            // 1. path(경로) - 어디에 저장할지 결정
            var path = Path.Combine(_environment.WebRootPath, @"images\upload");   //1/2/3/4 로 결정됨
            // 2. Name(파일이름) - DateTime시간으로 _ 사람이 많으면 중복값 충돌 / GUID _ 유사난수 발생
            // 3. Extension(확장자) - jpg, png... txt...
            // 3. Extension(확장자) - jpg, png... txt...
            var fileFullName = file.FileName.Split('.');
            var fileName = $"{Guid.NewGuid()}.{fileFullName[1]}";
            using(var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return Ok(new { file="/images/upload/" + fileName, success = true});
        }
    }
}
