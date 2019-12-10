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
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Text;

namespace WebJobDefaultDI
{
    public class JobFunctions
    {
        private readonly ISimpleService _simpleService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="simpleService"><see cref="WebJobDefaultDI.ISimpleService"/>ISimpleService implementation</param>
        public JobFunctions(ISimpleService simpleService)
        {
            _simpleService = simpleService; // Service implementation will be supplied via dependency injection
        }

        /// <summary>
        /// Process a message from an Azure Service Bus topic
        /// </summary>
        /// <param name="message"><see cref="Microsoft.Azure.ServiceBus.Message"/>Service Bus message</param>
        /// <param name="log"><see cref="Microsoft.Extensions.Logging.ILogger"/>Logging implementation</param>
        public void ProcessMessage(
            [ServiceBusTrigger("demotopic1", "WebJobSubscriber", Connection = "ServiceBusConnection")] Message message,
            ILogger log)
        {
            string body = Encoding.UTF8.GetString(message.Body);
            log.LogInformation($"WebJobs function ProcessMessage received the following message. {body}");
            log.LogInformation($"ISimpleService implementation SayHello response: {_simpleService.SayHello()}");
            log.LogInformation("WebJobs function ProcessMessage completed successfully.");
        }
    }
}