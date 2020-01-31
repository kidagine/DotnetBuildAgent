using System;
using System.Threading;
using System.Diagnostics;

namespace DotnetBuildAgent
{
	public class BuildAgent
	{
		private readonly string _fileName = "dotnet";

		public void Build(Agent agent)
		{
			Thread thread = new Thread(() =>
			{
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
					if (log.Contains("Error"))
					{
						//Console.WriteLine("Test Failed");
						//BuildManager.Instance.Stop();
						//Console.WriteLine();
						//Console.WriteLine("Press a key to exit build");
					}
					else
					{
						Thread.Sleep(agent.TimeInterval);
					}
				}
			});
			thread.Start();
		}
	}
}
