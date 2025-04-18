using Amazon.SQS;
using Amazon.SQS.Model;
using System.Text.Json;
using cloudass.Models;

namespace cloudass.Services
{
    public class EmailQueueSender : IEmailQueue
    {
        private readonly IAmazonSQS _sqs;
        private readonly string _queueUrl;

        public EmailQueueSender(IAmazonSQS sqs, IConfiguration cfg)
        {
            _sqs = sqs;
            _queueUrl = cfg["AWS:Sqs:EmailQueueUrl"]
                        ?? throw new InvalidOperationException("Queue URL missing");
        }

        public async Task SendEmailMessageAsync(EmailRequiredModel a, CancellationToken ct = default)
        {
            var payload = new
            {
                a.PatientName,
                PatientEmail = a.Email,
                a.AppointmentId,
                Service = a.ServiceName,
                DateTimeUtc = a.DateTimeUtc.ToString("u")
            };

            var json = JsonSerializer.Serialize(payload);

            var req = new SendMessageRequest
            {
                QueueUrl = _queueUrl,
                MessageBody = json
            };

            await _sqs.SendMessageAsync(req, ct);
        }
    }
}
