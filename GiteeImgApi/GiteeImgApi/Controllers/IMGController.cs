using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

namespace GiteeImgApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IMGController : ControllerBase
    {


        private readonly ILogger<IMGController> _logger;

        public IMGController(ILogger<IMGController> logger)
        {
            _logger = logger;
        }


        [HttpPost]
        public IActionResult Post( UserFile file)
        {
            var s = file;
            //if (file == null || !file.IsValid)
                //return new JsonResult(new { code = 500, message = "不允许上传的文件类型" });

            string newFile = string.Empty;
            //if (file != null)
             //   newFile = await file.SaveAs("/data/files/images");

            return new JsonResult(new { code = 0, message = "成功", url = newFile });
        }


        [HttpGet]
        public IActionResult Get([FromFile] UserFile file)
        {
            var s = file;
            //if (file == null || !file.IsValid)
            //return new JsonResult(new { code = 500, message = "不允许上传的文件类型" });

            string newFile = string.Empty;
            //if (file != null)
            //   newFile = await file.SaveAs("/data/files/images");

            return new JsonResult(new { code = 0, message = "Get成功", url = newFile });
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete()
        {
            int i = 5;
            string strUrl = $"https://gitee.com/api/v5/repos/imgrep001/m{i}?access_token=c1e9eba495a3db846846e7078a345493";

            HttpHelper.HttpDel(strUrl);


            return null;
        }




    }
    public class FromFileAttribute : Attribute, IBindingSourceMetadata
    {
        public BindingSource BindingSource => BindingSource.FormFile;
    }

    public class UserFile1
    {
        public string FileName { get; set; }
        public string File { get; set; }
    }

    public class UserFile
    {
        public string FileName { get; set; }
        public long Length { get; set; }
        public string Extension { get; set; }
        public string FileType { get; set; }

        private readonly static string[] Filters = { ".jpg", ".png", ".bmp" };
        public bool IsValid => !string.IsNullOrEmpty(this.Extension) && Filters.Contains(this.Extension);

        private IFormFile file;
        public IFormFile File
        {
            get { return file; }
            set
            {
                if (value != null)
                {
                    this.file = value;

                    this.FileType = this.file.ContentType;
                    this.Length = this.file.Length;
                    this.Extension = this.file.FileName.Substring(file.FileName.LastIndexOf('.'));
                    if (string.IsNullOrEmpty(this.FileName))
                        this.FileName = this.FileName;
                }
            }
        }

        public async Task<string> SaveAs(string destinationDir = null)
        {
            if (this.file == null)
                throw new ArgumentNullException("没有需要保存的文件");

            if (destinationDir != null)
                Directory.CreateDirectory(destinationDir);

            var newName = DateTime.Now.Ticks;
            var newFile = Path.Combine(destinationDir ?? "", $"{newName}{this.Extension}");
            using (FileStream fs = new FileStream(newFile, FileMode.CreateNew))
            {
                await this.file.CopyToAsync(fs);
                fs.Flush();
            }

            return newFile;
        }
    }
}
