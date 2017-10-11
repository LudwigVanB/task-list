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
            var cmd = parser.Parse("deadline 1 2017-12-25");
            Assert.IsInstanceOf<DeadlineCommand>(cmd);
        }


    }
}
