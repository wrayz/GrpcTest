using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Google.Protobuf;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;

namespace GrpcTMRobotServiceClient
{
    public class TMRobotGrpcClient
    {
        private readonly ILogger<TMRobotGrpcClient> _logger;
        private readonly TMRobot.TMRobotClient client;

        public TMRobotGrpcClient(ILogger<TMRobotGrpcClient> logger)
        {
            _logger = logger;
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("https://localhost:9831/", new GrpcChannelOptions { HttpHandler = httpHandler });
            client = new TMRobot.TMRobotClient(channel);
        }

        public async Task GetTMRobotAsync()
        {
            var reply = await client.GetTMRobotsAsync(new GetTMRobotRequest());
            Console.WriteLine("GetTMRobot|" + JsonSerializer.Serialize(reply, new JsonSerializerOptions { WriteIndented = true }));
            _logger.LogInformation("GetTMRobot|" + JsonSerializer.Serialize(reply, new JsonSerializerOptions { WriteIndented = true }));
        }

        public async Task GetTMRobotsByIPAsync(params string[] ips)
        {
            var request = new GetTMRobotByIPRequest();
            request.TMRobotIP.Add(ips);

            var reply = await client.GetTMRobotsByIPAsync(request);
            Console.WriteLine("GetTMRobotsByIP|" + JsonSerializer.Serialize(reply, new JsonSerializerOptions { WriteIndented = true }));
            _logger.LogInformation("GetTMRobotsByIP|" + JsonSerializer.Serialize(reply, new JsonSerializerOptions { WriteIndented = true }));
        }

        public async Task GetTMRobotDetailsAsync(params string[] names)
        {
            var request = new GetTMRobotDetailRequest();
            request.TMRobotName.Add(names);

            var reply = await client.GetTMRobotDetailsAsync(request);
            Console.WriteLine("GetTMRobotDetails|" + JsonSerializer.Serialize(reply, new JsonSerializerOptions { WriteIndented = true }));
            _logger.LogInformation("GetTMRobotDetails|" + JsonSerializer.Serialize(reply, new JsonSerializerOptions { WriteIndented = true }));
        }
        public async Task GetTMRobotDetailsByIPAsync(params string[] ips)
        {
            var request = new GetTMRobotDetailByIPRequest();
            request.TMRobotIP.Add(ips);

            var reply = await client.GetTMRobotDetailsByIPAsync(request);
            Console.WriteLine("GetTMRobotDetailsByIP|" + JsonSerializer.Serialize(reply, new JsonSerializerOptions { WriteIndented = true }));
            _logger.LogInformation("GetTMRobotDetailsByIP|" + JsonSerializer.Serialize(reply, new JsonSerializerOptions { WriteIndented = true }));
        }
        public async Task GetTMRobotVariablesAsync(string name, string projectName, params string[] varNames)
        {
            var request = new GetTMRobotVariablesRequest
            {
                TMRobotName = name,
                ProjectName = projectName,
            };
            request.VariableNames.Add(varNames);

            var reply = await client.GetTMRobotVariablesAsync(request);
            Console.WriteLine("GetTMRobotVariables|" + JsonSerializer.Serialize(reply, new JsonSerializerOptions { WriteIndented = true }));
            _logger.LogInformation("GetTMRobotVariables|" + JsonSerializer.Serialize(reply, new JsonSerializerOptions { WriteIndented = true }));
        }
        public async Task GetTMRobotVariableValueAsync(string name, string projectName, params string[] varNames)
        {
            var request = new GetTMRobotVariableValueRequest
            {
                TMRobotName = name,
                ProjectName = projectName,
            };
            request.VariableNames.Add(varNames);

            var reply = await client.GetTMRobotVariableValueAsync(request);
            Console.WriteLine("GetTMRobotVariableValue|" + JsonSerializer.Serialize(reply, new JsonSerializerOptions { WriteIndented = true }));
            _logger.LogInformation("GetTMRobotVariableValue|" + JsonSerializer.Serialize(reply, new JsonSerializerOptions { WriteIndented = true }));
        }

        public async Task GetTMRobotControlAsync(params string[] names)
        {
            var request = new GetTMRobotControlRequest();
            request.TMRobotNames.Add(names);

            var reply = await client.GetTMRobotControlAsync(request);
            Console.WriteLine("GetTMRobotControl|" + JsonSerializer.Serialize(reply, new JsonSerializerOptions { WriteIndented = true }));
            _logger.LogInformation("GetTMRobotControl|" + JsonSerializer.Serialize(reply, new JsonSerializerOptions { WriteIndented = true }));
        }

        public async Task ReleaseTMRobotControlAsync(params string[] names)
        {
            var request = new ReleaseTMRobotControlRequest();
            request.TMRobotNames.Add(names);

            var reply = await client.ReleaseTMRobotControlAsync(request);
            Console.WriteLine("ReleaseTMRobotControl|" + JsonSerializer.Serialize(reply, new JsonSerializerOptions { WriteIndented = true }));
            _logger.LogInformation("ReleaseTMRobotControl|" + JsonSerializer.Serialize(reply, new JsonSerializerOptions { WriteIndented = true }));
        }
        public async Task ChangeRobotDefaultProjectAsync(Func<Dictionary<string, string>> func)
        {
            var request = new ChangeRobotDefaultProjectRequest();
            request.RobotProjectMap.Add(func());

            var reply = await client.ChangeRobotDefaultProjectAsync(request);
            Console.WriteLine("ChangeRobotDefaultProject|" + JsonSerializer.Serialize(reply, new JsonSerializerOptions { WriteIndented = true }));
            _logger.LogInformation("ChangeRobotDefaultProject|" + JsonSerializer.Serialize(reply, new JsonSerializerOptions { WriteIndented = true }));
        }
        public async Task ChangeRobotExecutionAsync(Func<Dictionary<string, TMRobotExcution>> func)
        {
            var request = new ChangeRobotExecutionRequest();
            request.RobotExcutionMap.Add(func());

            var reply = await client.ChangeRobotExecutionAsync(request);
            Console.WriteLine("ChangeRobotExecution|" + JsonSerializer.Serialize(reply, new JsonSerializerOptions { WriteIndented = true }));
            _logger.LogInformation("ChangeRobotExecution|" + JsonSerializer.Serialize(reply, new JsonSerializerOptions { WriteIndented = true }));
        }
        public void SetRobotVariable(string robotName, string projectName, string varName, string value)
        {
            var request = new SetRobotVariableRequest
            {
                TMRobotName = robotName,
                ProjectName = projectName,
                VariableName = varName,
                Value = value
            };

            var reply = client.SetRobotVariable(request);
            Console.WriteLine("SetRobotVariable|" + JsonSerializer.Serialize(reply, new JsonSerializerOptions { WriteIndented = true }));
            _logger.LogInformation("SetRobotVariable|" + JsonSerializer.Serialize(reply, new JsonSerializerOptions { WriteIndented = true }));
        }
        public async Task PullProjectAsync(string robotName, string projectName)
        {
            var request = new PullProjectRequest
            {
                TMRobotName = robotName,
                ProjectName = projectName,
            };

            var reply = await client.PullProjectAsync(request);
            Console.WriteLine("PullProject|" + JsonSerializer.Serialize(reply, new JsonSerializerOptions { WriteIndented = true }));
            _logger.LogInformation("PullProject|" + JsonSerializer.Serialize(reply, new JsonSerializerOptions { WriteIndented = true }));
        }
        public void DownloadProject(string robotName, string projectName)
        {
            var request = new DownloadProjectRequest
            {
                TMRobotName = robotName,
                ProjectName = projectName,
            };

            var reply = client.DownloadProject(request);
            Console.WriteLine("DownloadProject|" + JsonSerializer.Serialize(reply, new JsonSerializerOptions { WriteIndented = true }));
            _logger.LogInformation("DownloadProject|" + JsonSerializer.Serialize(reply, new JsonSerializerOptions { WriteIndented = true }));
        }
        public async Task PushProjectAsync(string robotName, string projectName)
        {
            var request = new PushProjectRequest
            {
                TMRobotName = robotName,
                ProjectName = projectName,
            };

            var reply = await client.PushProjectAsync(request);
            Console.WriteLine("PushProject|" + JsonSerializer.Serialize(reply, new JsonSerializerOptions { WriteIndented = true }));
            _logger.LogInformation("PushProject|" + JsonSerializer.Serialize(reply, new JsonSerializerOptions { WriteIndented = true }));
        }
        public void UploadProject(string fileName, int size, int sendCount, string encode, ByteString content, string chacksum)
        {
            //TODO: proto 帶request用 stream, 所以要使用 Add方法，目前還沒找到
            // var request = new UploadProjectRequest
            // {
            //     FileInfo = new FileInfo
            //     {
            //         FileName = fileName,
            //         FileSize = size,
            //         SendCount = sendCount,
            //         Encode = encode,
            //         Content = content,
            //         Chacksum = chacksum
            //     }
            // };

            // Console.WriteLine("UploadProject|" + JsonSerializer.Serialize(reply, new JsonSerializerOptions { WriteIndented = true }));
            // _logger.LogInformation("UploadProject|" + JsonSerializer.Serialize(reply, new JsonSerializerOptions { WriteIndented = true }));
        }
    }
}