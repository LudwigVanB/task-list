using NUnit.Framework;
using System;

namespace Tasks.commands
{
    [TestFixture]
    class DeadlineCommandTest
    {
        [Test]
        public void DeadlineCommand_should_parse_task_id()
        {
            var cmd = new DeadlineCommand("1 2017-12-25");
            Assert.AreEqual(1, cmd.TaskId);
        }

        [Test]
        public void DeadlineCommand_should_parse_deadline()
        {
            var cmd = new DeadlineCommand("1 2017-12-25");
            Assert.AreEqual( new Deadline( new DateTime(2017, 12, 25)), cmd.Deadline);
        }
    }
}
