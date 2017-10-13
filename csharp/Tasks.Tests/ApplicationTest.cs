using NUnit.Framework;
using System;
using System.IO;

namespace Tasks
{
	[TestFixture]
	public sealed class ApplicationTest
	{
		public const string PROMPT = "> ";

		private FakeConsole console;
		private System.Threading.Thread applicationThread;

		[SetUp]
		public void StartTheApplication()
		{
			this.console = new FakeConsole();
			var taskList = new TaskList(console);
			this.applicationThread = new System.Threading.Thread(() => taskList.Run());
			applicationThread.Start();
		}

		[TearDown]
		public void KillTheApplication()
		{
			if (applicationThread == null || !applicationThread.IsAlive)
			{
				return;
			}

			applicationThread.Abort();
			throw new Exception("The application is still running.");
		}

		[Test, Timeout(1000)]
		public void ItWorks()
		{
			Execute("show");

			Execute("add project secrets");
			Execute("add task secrets Eat more donuts.");
			Execute("add task secrets Destroy all humans.");

			Execute("show");
			ReadLines(
				"secrets",
				"    [ ] 1: Eat more donuts.",
				"    [ ] 2: Destroy all humans.",
				""
			);

			Execute("add project training");
			Execute("add task training Four Elements of Simple Design");
			Execute("add task training SOLID");
			Execute("add task training Coupling and Cohesion");
			Execute("add task training Primitive Obsession");
			Execute("add task training Outside-In TDD");
			Execute("add task training Interaction-Driven Design");

			Execute("check 1");
			Execute("check 3");
			Execute("check 5");
			Execute("check 6");

			Execute("show");
			ReadLines(
				"secrets",
				"    [x] 1: Eat more donuts.",
				"    [ ] 2: Destroy all humans.",
				"",
				"training",
				"    [x] 3: Four Elements of Simple Design",
				"    [ ] 4: SOLID",
				"    [x] 5: Coupling and Cohesion",
				"    [x] 6: Primitive Obsession",
				"    [ ] 7: Outside-In TDD",
				"    [ ] 8: Interaction-Driven Design",
				""
			);

			Execute("quit");
		}

        [Test, Timeout(1000)]
        public void Should_acept_deadlines()
        {
            Execute("add project secrets");
            Execute("add task secrets Eat more donuts.");
            Execute("deadline 1 2017-12-25"); 

            Execute("show");
            ReadLines(
                "secrets",
                "    [ ] 1: Eat more donuts. Due 2017-12-25",
                ""
            );

            Execute("quit");
        }

        [Test, Timeout(1000)]
        public void Should_display_today_task()
        {
            var today = DateTime.Now.ToString("yyyy-MM-dd");
            var tomorrow = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");

            Execute("add project demo");
            Execute("add task demo Task 1.");
            Execute("add task demo Task 2.");
            Execute("add task demo Task 3.");
            Execute($"deadline 1 {today}");
            Execute($"deadline 2 {tomorrow}");

            Execute("today");
            ReadLines(
                $"    [ ] 1: Task 1. Due {today}",
                ""
            );

            Execute("quit");
        }

        [Test, Timeout(1000)]
        public void Should_accept_an_arbitrary_id()
        {
            Execute("add project demo");
            Execute("add task demo id:toto Task 1.");
            Execute("add task demo id:ab^$! Task 2.");
            Execute("show");
            ReadLines(
                "demo",
                "    [ ] toto: Task 1.",
                "    [ ] ab___: Task 2.",
                ""
            );
            Execute("quit");
        }

        private void Execute(string command)
		{
			Read(PROMPT);
			Write(command);
		}

		private void Read(string expectedOutput)
		{
			var length = expectedOutput.Length;
			var actualOutput = console.RetrieveOutput(expectedOutput.Length);
			Assert.AreEqual(expectedOutput, actualOutput);
		}

		private void ReadLines(params string[] expectedOutput)
		{
			foreach (var line in expectedOutput)
			{
				Read(line + Environment.NewLine);
			}
		}

		private void Write(string input)
		{
			console.SendInput(input + Environment.NewLine);
		}
	}
}
