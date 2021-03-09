
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GrpcGreeter.Services
{
    public class CustomerService : Customer.CustomerBase
    {
        private readonly ILogger<CustomerService> logger;

        public CustomerService(ILogger<CustomerService> logger)
        {
            this.logger = logger;
        }

        public override Task<CustomerModel> GetCustomer(CustomerRequest request, ServerCallContext context)
        {
            var customer = new CustomerModel
            {
                Id = 1,
                Name = "Ray",
                CustomerType = CustomerType.FirstLevel,
                PhoneNumbers = { new CustomerModel.Types.PhoneNumber { Value = "0911xxxxxxx" } },
                ModifiedTime = new DateTime(2020, 03, 08, 17, 45, 00).ToBinary()
            };

            return Task.FromResult(customer);
        }

        public override async Task GetAllCustomers(CustomerRequest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
        {
            var customers = new List<CustomerModel>
            {
             new CustomerModel
             {
                Id = 1,
                Name = "Ray",
                CustomerType = CustomerType.FirstLevel,
                PhoneNumbers = { new CustomerModel.Types.PhoneNumber { Value = "0911xxxxxxx" } },
                ModifiedTime = new DateTime(2020, 03, 08, 17, 45, 00).ToBinary()
             },
             new CustomerModel
             {
                Id = 2,
                Name = "Ray2",
                CustomerType = CustomerType.SecondLevel,
                PhoneNumbers = { new CustomerModel.Types.PhoneNumber { Value = "0911xxxxxxx" } },
                ModifiedTime = new DateTime(2020, 03, 08, 17, 45, 00).ToBinary()
             },
             new CustomerModel
             {
                Id = 3,
                Name = "Ray3",
                CustomerType = CustomerType.LastLevel,
                PhoneNumbers = { new CustomerModel.Types.PhoneNumber { Value = "0911xxxxxxx" } },
                ModifiedTime = new DateTime(2020, 03, 08, 17, 45, 00).ToBinary()
             }
            };

            foreach (var customer in customers)
            {
                await responseStream.WriteAsync(customer);
            }
        }

        public override Task<CustomerAddedResult> AddCustomer(CustomerModel request, ServerCallContext context)
        {
            request.ModifiedTime = DateTime.Now.ToBinary();

            //Do something

            return Task.FromResult(new CustomerAddedResult { IsSuccess = true });
        }

        public override async Task<CustomerAddedResult> AddCustomers(IAsyncStreamReader<CustomerModel> requestStream, ServerCallContext context)
        {
            var customers = new List<CustomerModel>();
            while (await requestStream.MoveNext())
            {
                customers.Add(requestStream.Current);
            }

            //Do something

            return new CustomerAddedResult { IsSuccess = true };
        }
    }
}