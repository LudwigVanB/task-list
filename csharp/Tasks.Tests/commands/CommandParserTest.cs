using NUnit.Framework;

namespace Tasks.commands
{
    [TestFixture]
    class CommandParserTest
    {
        [Test]
        public void CommandParser_should_recognize_deadline_command()
        {
            var parser = new CommandParser();
            var cmd = parser.Parse("deadline 1 2017-12-25", null);
            Assert.IsInstanceOf<DeadlineCommand>(cmd);
        }

        [Test]
        public void CommandParser_should_recognize_add_task_command()
        {
            var parser = new CommandParser();
            var cmd = parser.Parse("add task secrets Eat more donuts.", null);
            Assert.IsInstanceOf<AddTaskCommand>(cmd);
        }
    }
}
