using InvoicePaymentServices.Core.Models;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePaymentServices.Tests.FunctionalTests
{
    public class PaymentControllerTests : BaseControllerTests
    {
        public PaymentControllerTests(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async Task GetInvoicesByAccountId_InvoiceExists_ReturnCorrectIncoice()
        {
            var accountId = Guid.Parse("8d8ce279-b746-4e73-a1f8-40696fc4e632");
            var client = this.GetNewClient();
            var response = await client.GetAsync($"/api/v1/Invoices/{accountId}");
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<Invoice>>(stringResponse);
            Assert.NotNull(result);
            Assert.Single<Invoice>(result);

            var statusCode = response.StatusCode.ToString();
            Assert.Equal("OK", statusCode);

            Assert.True(result.First().Id == 1);
            Assert.Equal("Description 2", result.First().Description);
        }
    }
}
