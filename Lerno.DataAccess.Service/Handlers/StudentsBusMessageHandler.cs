using Lerno.DataAccess.Interfaces;
using Lerno.Shared.Commands;
using Lerno.Shared.Enums;
using Lerno.Shared.DTOs;
using Newtonsoft.Json;

namespace Lerno.DataAccess.Service.Handlers
{
    public class StudentsBusMessageHandler : IBusMessageHandler
    {
        private readonly IStudentsRepository _studentsRepository;

        public StudentsBusMessageHandler(IStudentsRepository studentsRepository)
        {
            _studentsRepository = studentsRepository;
        }

        private Dictionary<string, string> ParseToDictionary(string messageContent)
        {
            var parameters = new Dictionary<string, string>();
            var parts = messageContent.Split('&');
            foreach (var part in parts)
            {
                var kvp = part.Split('=');
                var (key, value) = (kvp[0], kvp[1]);

                parameters[key] = value;
            }

            return parameters;
        }

        private BusMessage<string> DeserializeBody(string messageContent)
        {
            var parameters = ParseToDictionary(messageContent);

            return new BusMessage<string>
            {
                Action = parameters["action"],
                MessageType = (BusMessageType)int.Parse(parameters["message_type"]),
                Handler = (BusMessageHandlerType)int.Parse(parameters["handler"]),
                TypeTag = parameters["type_tag"],
                Body = parameters["body"],
            };
        }

        public async Task<object> HandleCommandAsync(string messageContent, CancellationToken cancellationToken = default)
        {
            var busCommand = DeserializeBody(messageContent);

            var body = JsonConvert.DeserializeObject<GetStudentDTO>(busCommand.Body);
            var response = await _studentsRepository.GetUserOfStudentAsync(body.UserName, body.PasswordHash, cancellationToken);
            //switch (action)
            //{
            //    case BusStudentAction.GetStudent:
                    
            //        break;
            //}

            return response;
        }

        public Task HandleEventAsync(string messageContent, CancellationToken cancellationToken = default)
        {
            var busEvent = DeserializeBody(messageContent);

            return Task.CompletedTask;
        }
    }
}
