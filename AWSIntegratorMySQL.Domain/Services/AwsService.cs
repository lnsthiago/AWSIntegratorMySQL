using AWSIntegratorMySQL.Domain.Interfaces.Services;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Configuration;

namespace AWSIntegratorMySQL.Domain.Services
{
    public class AwsService : IAwsService
    {
        private IAmazonSQS _amazonSQSClient;

        private string _urlSQS;
        private const int MaxMessages = 1;
        private const int WaitTime = 2;

        public AwsService(IConfiguration configuration)
        {
            _amazonSQSClient = new AmazonSQSClient();
            _urlSQS = configuration.GetSection("AmazonSetup:SqsUrl").Value;
        }

        public async Task<ReceiveMessageResponse> GetMessageAsync()
        {
            return await _amazonSQSClient.ReceiveMessageAsync(new ReceiveMessageRequest
            {
                QueueUrl = _urlSQS,
                MaxNumberOfMessages = MaxMessages,
                WaitTimeSeconds = WaitTime
            });
        }

        public async Task DeleteMessage(Message message)
        {
            await _amazonSQSClient.DeleteMessageAsync(_urlSQS, message.ReceiptHandle);
        }
    }
}
