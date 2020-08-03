
namespace Celeste.Models.Instance
{
    public class FileInfoResponse
    {
        public string Parent { get; private set; }

        public FileInfo[] Elements { get; private set; }

        public FileInfoResponse(string parent, FileInfo[] elements) {
            Parent = parent;
            Elements = elements;
        }
    }
}