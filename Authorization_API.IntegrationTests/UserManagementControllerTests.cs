using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authorization_API.IntegrationTests.Util;
using Authorization_Models;
using AutoFixture;
using Newtonsoft.Json;
using Xunit;

namespace Authorization_API.IntegrationTests
{
    public class UserManagementControllerTests
    {
        private readonly BaseTestFixture _fixture;

        public UserManagementControllerTests()
        {
            _fixture = new BaseTestFixture();
        }

        [Fact]
        public async Task GetIdByLogin_ReturnsId()
        {
            var lastUser = _fixture.DbContext.Users.Last();

            var id = await _fixture.Client.GetAsync("api/UserManagement/GetIdByLogin?login=" + lastUser.Login);

            Assert.Equal(lastUser.Id.ToString(), await id.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task GetAttempts_ReturnsAttempts()
        {
            var firstUser = _fixture.DbContext.Users.First();

            var response = await _fixture.Client.GetAsync("api/UserManagement/GetAttempts?id=" + firstUser.Id);

            var stringResponse = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<IEnumerable<TestAttempt>>(stringResponse);

            Assert.NotEmpty(list);
            foreach (var id in list.Select(x => x.UserId))
            {
                Assert.Equal(firstUser.Id, id);
            }
        }

        [Fact]
        public async Task AddToRole_UserRoleChanged()
        {
            var lastUser = _fixture.DbContext.Users.First();

            var response = await _fixture.Client.GetAsync("api/UserManagement/AddToRole?userId=" + lastUser.Id+"&role="+"test");

            var stringResponse = JsonConvert.DeserializeObject<IEnumerable<User>>
                (await _fixture.Client.GetStringAsync("api/UserCrud/"))
                .First(x=>x.Id==lastUser.Id);

            Assert.Equal("test",stringResponse.Role);
        }

        [Fact]
        public async Task RemoveFromRole_UserRoleBecomesUser()
        {
            var lastUser = _fixture.DbContext.Users.Last();

            var response = await _fixture.Client.GetAsync("api/UserManagement/RemoveFromRole?userId=" + lastUser.Id + "&role=" + "test");

            var stringResponse = JsonConvert.DeserializeObject<IEnumerable<User>>
                    (await _fixture.Client.GetStringAsync("api/UserCrud/"))
                .First(x => x.Id == lastUser.Id);

            Assert.Equal("user", stringResponse.Role);
        }

        [Fact]
        public async Task AddUserAttempt_AttemptAdded()
        {
            var lastUser = _fixture.DbContext.Users.Last();

            var response = await _fixture.Client.GetAsync("api/UserManagement/AddUserAttempt?userId=" + lastUser.Id + "&testId=" + lastUser.Id);

            var stringResponse = JsonConvert.DeserializeObject<IEnumerable<TestAttempt>>
                    (await _fixture.Client.GetStringAsync("api/TestAttemptCrud/"))
                .Last();

            Assert.Equal(lastUser.Id, stringResponse.UserId);
        }

        [Fact]
        public async Task ChangeUserLogin_LoginChanged()
        {
            var lastUser = _fixture.DbContext.Users.Last();

            var response = await _fixture.Client.GetAsync("api/UserManagement/ChangeUserLogin?userId=" + lastUser.Id + "&newLogin=" + "ahaha");

            var stringResponse = JsonConvert.DeserializeObject<IEnumerable<User>>
                    (await _fixture.Client.GetStringAsync("api/UserCrud/"))
                .First(x=>x.Id == lastUser.Id);

            Assert.Equal("ahaha", stringResponse.Login);
        }

        [Fact]
        public async Task ChangeUserPassword_PasswordChanged()
        {
            var lastUser = _fixture.DbContext.Users.Last();

            var response = await _fixture.Client.GetAsync("api/UserManagement/ChangeUserPassword?userId=" + lastUser.Id + "&newPassword=" + "lolo");

            var stringResponse = JsonConvert.DeserializeObject<IEnumerable<User>>
                    (await _fixture.Client.GetStringAsync("api/UserCrud/"))
                .Last();

            Assert.Equal("lolo", stringResponse.Password);
        }

        [Fact]
        public async Task ChangeUserFirstName_FirstNameChanged()
        {
            var lastUser = _fixture.DbContext.Users.Last();

            var response = await _fixture.Client.GetAsync("api/UserManagement/ChangeUserFirstName?userId=" + lastUser.Id + "&newFirstName=" + "aaaa");

            var stringResponse = JsonConvert.DeserializeObject<IEnumerable<User>>
                    (await _fixture.Client.GetStringAsync("api/UserCrud/"))
                .First(x=>x.Id == lastUser.Id);

            Assert.Equal("aaaa", stringResponse.FirstName);
        }

        [Fact]
        public async Task ChangeUserLastName_LastNameChanged()
        {
            var lastUser = _fixture.DbContext.Users.Last();

            var response = await _fixture.Client.GetAsync("api/UserManagement/ChangeUserLastName?userId=" + lastUser.Id + "&newLastName=" + "bbbb");

            var stringResponse = JsonConvert.DeserializeObject<IEnumerable<User>>
                    (await _fixture.Client.GetStringAsync("api/UserCrud/"))
                .Last();

            Assert.Equal("bbbb", stringResponse.LastName);
        }

    }
}