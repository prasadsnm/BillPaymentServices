﻿using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePaymentServices.Tests.FunctionalTests
{
    public class BaseControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;

        public BaseControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        public HttpClient GetNewClient()
        {
            var newClient = _factory.WithWebHostBuilder(builder =>
            {
                _factory.CustomConfigureServices(builder);
            }).CreateClient();

            return newClient;
        }
    }
}
