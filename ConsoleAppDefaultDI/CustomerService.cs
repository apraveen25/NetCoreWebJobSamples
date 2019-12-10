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
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace ConsoleAppDefaultDI
{
    public class CustomerService : ICustomerService
    {
        private ILogger _logger;
        private ICustomerRepository _repository;

        public CustomerService(ILogger<CustomerService> logger, ICustomerRepository repository)
        {
            _logger = logger; // Service implementation will be supplied via dependency injection
            _repository = repository; // Service implementation will be supplied via dependency injection
        }

        public Customer GetCustomer(int customerId)
        {
            Customer customer = _repository.Get(customerId);
            _logger.LogInformation($"Computing total sales for customer {customerId}");
            Random random = new Random();
            customer.TotalSales = random.Next(1000, 10000);
            return customer;
        }

        public List<Customer> ListCustomers()
        {
            List<Customer> customers = _repository.List();
            Random random = new Random();
            foreach (Customer customer in customers)
            {
                _logger.LogInformation($"Computing total sales for customer {customer.CustomerId}");
                customer.TotalSales = random.Next(1000, 10000);
            }
            _logger.LogInformation($"Returning {customers.Count} customers");
            return customers;
        }
    }
}