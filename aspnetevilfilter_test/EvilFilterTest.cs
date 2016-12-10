using Xunit;
using System.Net.Http;
using aspnetevilfilter;
using Microsoft.AspNetCore.Builder;  
using Microsoft.AspNetCore.Hosting;  
using Microsoft.AspNetCore.Http;  
using Microsoft.AspNetCore.TestHost;  
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aspnetevilfilter_test
{
    public class EviltFilterTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public EviltFilterTest()
        {
          _server = new TestServer(new WebHostBuilder()
            .UseStartup<Startup>());
         _client = _server.CreateClient();
        }

        [Fact]
        public async Task Should_add_evil_header_to_get_request()
        {
            HttpResponseMessage response = await _client.GetAsync("/api/Values");
            var headers = response.Headers;
            IEnumerable<string> values;
            string evilHeader = null;
            if (headers.TryGetValues("Goyello", out values))
            {
                evilHeader = values.First();
            }
            Assert.NotNull(evilHeader);
            Assert.Equal(evilHeader,"Not funny easter egg");            
        }

        [Fact]
        public async Task Should_response_with_internal_server_error_code()
        {
            HttpResponseMessage response = await _client.GetAsync("/api/Values");
            System.Net.HttpStatusCode statusCode = response.StatusCode;
            Assert.Equal(System.Net.HttpStatusCode.InternalServerError,statusCode);
        }

        [Fact]
        public async Task Should_response_with_419_status_code()
        {
            var content = new StringContent("{}", Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync("/api/Values",content);
            int statusCode = (int) response.StatusCode;
            Assert.Equal(statusCode,419);
        }

        [Fact]
        public async Task Should_response_with_418_status_code()
        {
            HttpResponseMessage response = await _client.GetAsync("/api/Values/6");
            int statusCode = (int) response.StatusCode;
            Assert.Equal(statusCode,418);
        }





    }
}
