using AWSIntegratorMySQL.Domain.Entities;
using AWSIntegratorMySQL.Domain.Interfaces.Repositories;
using AWSIntegratorMySQL.Infra.Scripts;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace AWSIntegratorMySQL.Infra.Repositories
{
    public class FileRepository : IFileRepository
    {
        private IConfiguration _configuration;
        private string _connectionString;

        public FileRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public FileEntity GetByFileName(string fileName)
        {
            try
            {
                FileEntity fileEntity = null;
                using (var connection = new MySqlConnection(_connectionString))
                {
                    using (var command = new MySqlCommand(FileScript.GetByFileName, connection))
                    {
                        command.Parameters.AddWithValue("@fileName", fileName);

                        connection.Open();

                        var dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);

                        while (dataReader.Read())
                        {
                            fileEntity = new FileEntity
                            {
                                FileName = dataReader["fileName"].ToString(),
                                FileSize = Convert.ToInt64(dataReader["fileSize"]),
                                LastModified = Convert.ToDateTime(dataReader["lastModified"])
                            };
                        }
                    }
                }

                return fileEntity;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro ao buscar registro no banco. Message: {ex.Message}", ConsoleColor.Red);
                throw;
            }
        }

        public void Insert(FileEntity fileEntity)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand(FileScript.Insert, connection))
                    {
                        command.Parameters.AddWithValue("@fileName", fileEntity.FileName);
                        command.Parameters.AddWithValue("@fileSize", fileEntity.FileSize);
                        command.Parameters.AddWithValue("@lastModified", fileEntity.LastModified);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro ao inserir registro no banco. Message: {ex.Message}", ConsoleColor.Red);
                throw;
            }
        }

        public void UpdateSize(FileEntity fileEntity)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand(FileScript.UpdateSize, connection))
                    {
                        command.Parameters.AddWithValue("@fileName", fileEntity.FileName);
                        command.Parameters.AddWithValue("@fileSize", fileEntity.FileSize);
                        command.Parameters.AddWithValue("@lastModified", fileEntity.LastModified);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro ao atualizar registro no banco. Message: {ex.Message}", ConsoleColor.Red);
                throw;
            }
        }
    }
}
