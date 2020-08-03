
using System.Linq;
using System.Runtime.InteropServices;
using Celeste.Models.Instance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Celeste.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileSystemController : ControllerBase
    {
        private readonly ILogger<FileSystemController> _logger;

        public FileSystemController(ILogger<FileSystemController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get([FromQuery(Name = "includeFiles")] bool includeFiles = false, [FromQuery(Name = "path")] string path = "")
        {

            string parentPath = null;
            FileInfo[] directories = null;
            FileInfo[] files = null;

            _logger.LogInformation("Querying file system info for {0}", path);

            if (string.IsNullOrWhiteSpace(path))
            {
                // Showing root data
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    // Return Drives (C:\ D:\ W:\ etc)
                    directories = System.IO.Directory.GetLogicalDrives()
                        .Select(dir => new FileInfo(name: dir, path: dir, isFolder: true)).ToArray();
                }
                else
                {
                    // Return listing from root '/' 
                    var cwd = new System.IO.DirectoryInfo("/");
                    directories = cwd.GetDirectories()
                        .Select(dir => new FileInfo(name: dir.Name, path: dir.FullName, isFolder: true)).ToArray();
                }
            }
            else
            {
                // Showing from given path
                var cwd = new System.IO.DirectoryInfo(path);
                directories = cwd.GetDirectories()
                    .Select(dir => new FileInfo(name: dir.Name, path: dir.FullName, isFolder: true)).ToArray();

                files = includeFiles ? cwd.GetFiles()
                    .Select(file => new FileInfo(name: file.Name, path: file.FullName, isFolder: false)).ToArray() : null;

                parentPath = cwd.Parent.FullName;
            }

            return Ok(
                new FileInfoResponse(
                    parent: parentPath,
                    directories: directories,
                    files: files
                )
            );
        }
    }
}