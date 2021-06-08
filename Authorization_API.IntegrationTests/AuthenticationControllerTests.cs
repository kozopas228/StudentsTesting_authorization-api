using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Authorization_API.IntegrationTests.Util;
using Authorization_Models;
using AutoFixture;
using Newtonsoft.Json;
using Xunit;

namespace Authorization_API.IntegrationTests
{
    public class AuthenticationControllerTests
    {
        private readonly BaseTestFixture _fixture;
        public AuthenticationControllerTests()
        {
            _fixture = new BaseTestFixture();
        }

        [Fact]
        public async Task Login_WrongCredentials_ReturnsUnathorized()
        {
            var lastUser = _fixture.DbContext.Users.Last();

            var response = await _fixture.Client.PostAsync("api/Authentication/Login?login=" + lastUser.Login + "&password=" + "1234", new StringContent(""));

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Register_UserRegistered()
        {
            var fixture = new Fixture();

            var user = fixture.Create<User>();

            var response = await _fixture.Client.PostAsync("api/Authentication/Register" +
                                                           "?login=" + user.Login + 
                                                           "&password=" + user.Password+
                                                           "&firstname="+user.FirstName+
                                                           "&lastname="+user.LastName, new StringContent(""));

            var stringResponse = JsonConvert.DeserializeObject<IEnumerable<User>>
                    (await _fixture.Client.GetStringAsync("api/UserCrud/"))
                .First(x=>x.Login == user.Login);

            Assert.Equal(user.Login, stringResponse.Login);
        }

        [Fact]
        public async Task Register_WrongCredentials_BadRequest()
        {
            var fixture = new Fixture();

            var response = await _fixture.Client.PostAsync("api/Authentication/Register" +
                                                           "?login=" + "1" +
                                                           "&password=" + "2" +
                                                           "&firstname=" + "3" +
                                                           "&lastname=" + "4", new StringContent(""));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}