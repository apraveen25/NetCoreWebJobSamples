//===============================================================================
// Microsoft FastTrack for Azure
// .Net Core 3 WebJob and Dependency Injection Samples
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace ConsoleAppDefaultDI
{
    public class CustomerRepository : ICustomerRepository
    {
        private IConfigurationRoot _configuration;
        private ILogger _logger;
        private readonly string _connectionString;

        public CustomerRepository(IConfigurationRoot configuration, ILogger<CustomerRepository> logger)
        {
            _configuration = configuration; // Service implementation will be supplied via dependency injection
            _logger = logger; // Service implementation will be supplied via dependency injection
            _connectionString = _configuration.GetConnectionString("SQLConn");
            _logger.LogInformation($"Retrieved connection string from configuration {_connectionString}");
        }

        public Customer Get(int customerId)
        {
            _logger.LogInformation($"Retrieving customer {customerId}");
            return new Customer()
            {
                CustomerId = customerId,
                CustomerName = $"Customer{customerId}"
            };
        }

        public List<Customer> List()
        {
            _logger.LogInformation("Retreiving all customers");
            List<Customer> customers = new List<Customer>();
            for (int i = 1; i < 6; i++)
            {
                customers.Add(new Customer() { CustomerId = i, CustomerName = $"Customer{i}" });
            }
            return customers;
        }
    }
}