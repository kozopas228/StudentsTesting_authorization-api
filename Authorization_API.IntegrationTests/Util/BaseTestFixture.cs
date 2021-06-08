using System.Net.Http;
using Authorization_Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace Authorization_API.IntegrationTests.Util
{
    public class BaseTestFixture
    {
        public TestServer TestServer { get; }
        public ApplicationContext DbContext { get; }
        public HttpClient Client { get; }

        public BaseTestFixture()
        {
            var builder = new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseStartup<Startup>();

            TestServer = new TestServer(builder);
            Client = TestServer.CreateClient();
            DbContext = TestServer.Host.Services.GetService<ApplicationContext>();

            FakeDbInitializer.Initialize(DbContext);

        }

        public void Dispose()
        {
            Client.Dispose();
            TestServer.Dispose();
        }
    }
}