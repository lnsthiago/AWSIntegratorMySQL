namespace AWSIntegratorMySQL.Infra.Scripts
{
    public static class FileScript
    {
        public const string GetByFileName = "SELECT * FROM fileaws.files where fileName = @fileName;";
        
        public const string Insert = @"INSERT INTO files (fileName, fileSize, lastModified) 
                                       VALUES (@fileName, @fileSize, @lastModified);";

        public const string UpdateSize = @"UPDATE files 
                                           SET fileSize = @fileSize, lastModified = @lastModified 
                                           WHERE fileName = @fileName;";
    }
}
