
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
        public IActionResult Get(
            [FromQuery(Name = "includeFiles")] bool includeFiles = false, 
            [FromQuery(Name = "path")] string path = ""
        )
        {

            string parentPath = null;
            System.Collections.Generic.List<FileInfo> elements = null;

            _logger.LogInformation("Querying file system info for {0}", path);

            if (string.IsNullOrWhiteSpace(path))
            {
                // Showing root data
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    // Return Drives (C:\ D:\ W:\ etc)
                    elements = System.IO.Directory.GetLogicalDrives()
                        .Select(dir => new FileInfo(name: dir, path: dir, isFolder: true)).ToList();
                }
                else
                {
                    // Return listing from root '/' 
                    var cwd = new System.IO.DirectoryInfo("/");
                    elements = cwd.GetDirectories()
                        .Select(dir => new FileInfo(name: dir.Name, path: dir.FullName, isFolder: true)).ToList();
                }
            }
            else
            {
                // Showing from given path
                var cwd = new System.IO.DirectoryInfo(path);
                elements = cwd.GetDirectories()
                    .Where(dir => dir.Attributes.HasFlag(System.IO.FileAttributes.Hidden) == false && dir.Attributes.HasFlag(System.IO.FileAttributes.System) == false) // Do not include hidden folders
                    .Select(dir => new FileInfo(name: dir.Name, path: dir.FullName, isFolder: true)).ToList();

                if (includeFiles) {
                    var files = cwd.GetFiles()
                        .Select(file => new FileInfo(name: file.Name, path: file.FullName, isFolder: false));

                        elements.AddRange(files);
                }

                parentPath = cwd.Parent?.FullName;
            }

            return Ok(
                new FileInfoResponse(
                    parent: parentPath,
                    elements: elements.ToArray()
                )
            );
        }
    }
}