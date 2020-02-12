using System;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DotnetBuildAgent
{
	public class BuildAgent
	{
		private readonly string _fileName = "dotnet";

		public void Build(Agent agent)
		{
			CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
			CancellationToken cancellationToken = cancellationTokenSource.Token;
			Thread thread = new Thread(() =>
			{
				CheckStop(cancellationTokenSource);
			});
			thread.Start();


			if (cancellationToken.IsCancellationRequested)
			{
				Console.WriteLine("Task was cancelled before it got started.");
				cancellationToken.ThrowIfCancellationRequested();
			}

			while (BuildManager.Instance.IsRunningProcesses)
			{
				using Process process = new Process
				{
					StartInfo =
					{
						FileName = _fileName,
						Arguments = agent.AgentType.ToString().ToLower(),
						UseShellExecute = false,
						RedirectStandardOutput = true,
						WorkingDirectory = agent.Path
					}
				};
				process.Start();
				string log = process.StandardOutput.ReadToEnd();
				LogManager.Instance.LogText += log;
				Console.WriteLine(log);
				Thread.Sleep(agent.TimeInterval);
			}
		}

		private void CheckStop(CancellationTokenSource cancellationTokenSource)
		{
			ConsoleKeyInfo consoleKeyInfo;
			do
			{
				consoleKeyInfo = Console.ReadKey(true);
				if (consoleKeyInfo.Key == ConsoleKey.F)
				{
					cancellationTokenSource.Cancel();
					BuildManager.Instance.IsRunningProcesses = false;
					Console.WriteLine("Stopped agent queue");
				}
			} while (BuildManager.Instance.IsRunningProcesses);
			LogManager.Instance.LogBuild();
			Console.WriteLine("Logged build");
		}
	}
}
