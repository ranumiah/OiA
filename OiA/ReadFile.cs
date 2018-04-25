using System.IO;
using OiA.Repository;

namespace OiA
{
    public class ReadFile
    {
        public FileDetail GetFileDetails(string file)
        {
            var fileInfo = new FileInfo(file);
            var fileDetail = new FileDetail
            {
                Name = fileInfo.Name,
                Length = fileInfo.Length,
                FullName = fileInfo.FullName,
                Extension = fileInfo.Extension,
                FileCreationTimeUtc = fileInfo.CreationTimeUtc,
                FileLastWriteTimeUtc = fileInfo.LastWriteTimeUtc,
                FileLastAccessTimeUtc = fileInfo.LastAccessTimeUtc
            };

            return fileDetail;
        }
    }
}
