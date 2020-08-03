
namespace Celeste.Models.Instance
{
    public class FileInfoResponse
    {
        public string Parent { get; private set; }

        public FileInfo[] Directories { get; private set; }
        public FileInfo[] Files { get; private set; }

        public FileInfoResponse(string parent, FileInfo[] directories, FileInfo[] files) {
            Parent = parent;
            Directories = directories;
            Files = files;
        }
    }
}