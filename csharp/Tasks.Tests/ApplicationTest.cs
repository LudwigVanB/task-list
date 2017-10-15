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

		[Timeout(1000)]
        //[TestCase("show")]
        [TestCase("view by project")]
        public void ItWorks(string showCommand)
		{
			Execute(showCommand);

			Execute("add project secrets");
			Execute("add task secrets Eat more donuts.");
			Execute("add task secrets Destroy all humans.");

			Execute(showCommand);
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

			Execute(showCommand);
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

            Execute("view by project");
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
            Execute("view by project");
            ReadLines(
                "demo",
                "    [ ] toto: Task 1.",
                "    [ ] ab___: Task 2.",
                ""
            );
            Execute("quit");
        }

        [Test, Timeout(1000)]
        public void Should_delete_a_task()
        {
            Execute("add project demo");
            Execute("add task demo Task 1.");
            Execute("add task demo Task 2.");
            Execute("delete 1");
            Execute("view by project");
            ReadLines(
                "demo",
                "    [ ] 2: Task 2.",
                ""
            );
            Execute("quit");
        }

        [Timeout(1000)]
        [TestCase("view by date")]
        [TestCase("view by deadline")]
        public void Should_list_task_by_date(string command)
        {
            Execute("add project demo");
            Execute("add task demo Task 1.");
            Execute("add task demo Task 2.");
            Execute("add task demo Task 3.");
            Execute("add task demo Task 4.");
            Execute($"deadline 1 2017-12-25");
            Execute($"deadline 3 2017-11-01");
            Execute($"deadline 4 2017-12-25");
            Execute(command);
            ReadLines(
                "2017-11-01",
                "    [ ] 3: Task 3.",
                "",
                "2017-12-25",
                "    [ ] 1: Task 1.",
                "    [ ] 4: Task 4.",
                "",
                "No date",
                "    [ ] 2: Task 2.",
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
