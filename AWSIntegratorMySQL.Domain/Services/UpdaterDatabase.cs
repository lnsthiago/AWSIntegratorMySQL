using AWSIntegratorMySQL.Domain.Entities;
using AWSIntegratorMySQL.Domain.Interfaces.Repositories;
using AWSIntegratorMySQL.Domain.Interfaces.Services;
using System;

namespace AWSIntegratorMySQL.Domain.Services
{
    public class UpdaterDatabase : IUpdaterDatabase
    {
        private IFileRepository _fileRepository;

        public UpdaterDatabase(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public void Update(FileEntity fileEntity)
        {
            var fileRegistered = _fileRepository.GetByFileName(fileEntity.FileName);

            if (fileRegistered == null) 
                _fileRepository.Insert(fileEntity);
            else
            {
                if (fileRegistered.LastModified >= fileEntity.LastModified)
                    Console.WriteLine($"Última atualização registrada no banco é mais nova que a data da notificação. Atualizaçao não será realizada");
                else
                    _fileRepository.UpdateSize(fileEntity);
            }
        }
    }
}
