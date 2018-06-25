using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LibrameStandard.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace LibrameCore.WebMvc.Controllers
{
    public class FileController : Controller
    {
        private readonly static string _uploadsDirectory = PathUtility.AppendBasePath(@"wwwroot\uploads");

        public FileController()
        {
            Directory.CreateDirectory(_uploadsDirectory);
        }


        /// <summary>
        /// 上传重命名工厂方法。
        /// </summary>
        public Func<IFormFile, string, string> UploadRenameFactory { get; set; }
            = (file, extension) => file.FileName;

        /// <summary>
        /// 重名重新命名工厂方法。
        /// </summary>
        public Func<string, string, string> DuplicateRenameFactory { get; set; }
            = (baseName, extension) => baseName + "_" + DateTime.Now.Ticks + extension;


        /// <summary>
        /// 重名检查。
        /// </summary>
        /// <param name="dir">给定的目录。</param>
        /// <param name="name">给定的名称。</param>
        /// <returns>返回不重名的路径。</returns>
        protected virtual string DuplicateChecking(string dir, string name)
        {
            var path = Path.Combine(dir, name);

            if (System.IO.File.Exists(path))
            {
                // 取得扩展名（包含句点）
                var extension = Path.GetExtension(name);
                // 取得基础名
                var baseName = name.Substring(0, name.LastIndexOf(extension));
                // 重名重新命名
                var newName = DuplicateRenameFactory(baseName, extension);

                // 链式检查
                path = DuplicateChecking(dir, newName);
            }

            return path;
        }


        public IActionResult Index()
        {
            var entries = Directory.EnumerateFileSystemEntries(_uploadsDirectory);

            ViewBag.UploadsDirectory = _uploadsDirectory;

            return View(entries);
        }

        
        public IActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Upload(IFormCollection collection)
        {
            var files = Request.Form.Files;
            var allSize = files.Sum(f => f.Length);

            // 上传文件
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    // 取得扩展名（包含句点）
                    var extension = Path.GetExtension(file.FileName);
                    var rename = UploadRenameFactory(file, extension);

                    // 重名检查
                    var path = DuplicateChecking(_uploadsDirectory, rename);

                    // 上传文件
                    using (var fs = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(fs);
                    }
                }
            }

            return Ok(new { count = files.Count, size = allSize });
        }


        public IActionResult Download(string path)
        {
            path = Path.Combine(_uploadsDirectory, path);

            if (!System.IO.File.Exists(path))
                return NotFound(new { file = path });

            var provider = new FileExtensionContentTypeProvider();
            var extension = Path.GetExtension(path);

            // 移除句点
            var contentType = provider.Mappings[extension];

            var fs = System.IO.File.OpenRead(path);
            return File(fs, contentType, Path.GetFileName(path));
        }

    }
}