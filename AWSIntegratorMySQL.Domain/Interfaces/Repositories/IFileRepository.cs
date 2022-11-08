using AWSIntegratorMySQL.Domain.Entities;

namespace AWSIntegratorMySQL.Domain.Interfaces.Repositories
{
    public interface IFileRepository
    {
        FileEntity GetByFileName(string fileName);

        void Insert(FileEntity fileEntity);

        void UpdateSize(FileEntity fileEntity);
    }
}
