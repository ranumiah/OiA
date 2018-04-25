using System.IO;
using OiA.Repository;

namespace OiA
{
    public class ReadFile
    {
        public FileDetail GetFileDetails(string file)
        {
            var fileInfo = new FileInfo(file);
            var fileDetail = new FileDetail();
            fileDetail.FileName = fileInfo.Name;
            fileDetail.FileLength = fileInfo.Length;
            fileDetail.FileFullName = fileInfo.FullName;
            fileDetail.FileExtension = fileInfo.Extension;
            fileDetail.FileCreationTimeUtc = fileInfo.CreationTimeUtc;
            fileDetail.FileLastWriteTimeUtc = fileInfo.LastWriteTimeUtc;
            fileDetail.FileLastAccessTimeUtc = fileInfo.LastAccessTimeUtc;

            return fileDetail;
        }
    }
}
