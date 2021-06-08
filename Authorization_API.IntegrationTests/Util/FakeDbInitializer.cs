using Authorization_Data;
using Authorization_Models;
using AutoFixture;
using System.Collections.Generic;

namespace Authorization_API.IntegrationTests.Util
{
    public class FakeDbInitializer
    {
        public static void Initialize(ApplicationContext context)
        {
            var fixture = new Fixture();

            var users = new List<User>();
            var attempts = new List<TestAttempt>();

            for (int i = 0; i < 10; i++)
            {
                var user = fixture.Create<User>();
                users.Add(user);
            }

            context.Users.AddRange(users);
            context.SaveChanges();

            foreach (var user in users)
            {
                for (int i = 0; i < 5; i++)
                {
                    var attempt = fixture.Build<TestAttempt>()
                        .With(x => x.UserId, user.Id)
                        .Create();

                    attempts.Add(attempt);
                }
            }

            context.TestAttempts.AddRange(attempts);
            context.SaveChanges();
        }
    }
}