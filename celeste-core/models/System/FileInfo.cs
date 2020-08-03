
namespace Celeste.Models.Instance
{
    public class FileInfo
    {
        public bool IsFolder { get; private set; }

        public string Name { get; private set; }

        public string Path {get;private set;}

        public FileInfo(string name, string path, bool isFolder) {
            Name = name;
            Path = path;
            IsFolder = isFolder;
        }
    }
}