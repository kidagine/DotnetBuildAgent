using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetBuildAgent
{
	class Program
	{
		private const string Path = @"C:\Users\Kiddo\Desktop\Code\AgentDev\DotnetBuildAgent\";

		static void Main(string[] args)
		{
			//TaskTest();
			UserInput userInput = new UserInput();
			userInput.Start();
		}

		private static void TaskTest()
		{
			Agent agent = new Agent { Id = 0, Name = "agent", AgentType = AgentType.Build, TimeInterval = 2000, Path = Path };
			while (true)
			{
				Task task = Task.Run(() => ExecuteBuild(agent));
				Thread.Sleep(2000);
				task.Wait();
				Console.WriteLine("Done");
			}

		}

		static void ExecuteBuild(Agent agent)
		{
			using Process process = new Process
			{
				StartInfo =
				{
					FileName = "dotnet",
					Arguments = agent.AgentType.ToString().ToLower(),
					UseShellExecute = false,
					RedirectStandardOutput = true,
					WorkingDirectory = agent.Path
				}
			};
			process.Start();
		}
	}
}
