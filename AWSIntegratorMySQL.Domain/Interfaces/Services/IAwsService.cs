using Amazon.SQS.Model;
using System.Threading.Tasks;

namespace AWSIntegratorMySQL.Domain.Interfaces.Services
{
    public interface IAwsService
    {
        Task<ReceiveMessageResponse> GetMessageAsync();

        Task DeleteMessage(Message message);
    }
}
