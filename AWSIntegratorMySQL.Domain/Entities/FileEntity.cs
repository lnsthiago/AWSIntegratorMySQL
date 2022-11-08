using System;

namespace AWSIntegratorMySQL.Domain.Entities
{
    public class FileEntity
    {
        public FileEntity() { }

        public FileEntity(string fileName, long fileSize, DateTime lastModified)
        {
            FileName = fileName;
            FileSize = fileSize;
            LastModified = lastModified;
        }

        public string FileName { get; set; }

        public long FileSize { get; set; }

        public DateTime LastModified { get; set; }
    }
}