using System;

namespace Authorization_Models
{
    public class TestAttempt
    {
        public Guid Id { get; set; }
        public Guid TestId { get; set; }
        public TestState TestState { get; set; }
        public int TestGrade { get; set; }
        public Guid UserId { get; set; }
        //public User User { get; set; }
    }
}