using AWSIntegratorMySQL.Domain.Entities;

namespace AWSIntegratorMySQL.Domain.Interfaces.Services
{
    public interface IUpdaterDatabase
    {
        void Update(FileEntity fileEntity);
    }
}
