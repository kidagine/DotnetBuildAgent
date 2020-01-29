using System;
using System.Threading;
using System.Diagnostics;

namespace DotnetBuildAgent
{
	public class BuildAgent
	{
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
							FileName = "dotnet",
							Arguments = "build",
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
			});
			thread.Start();
		}
	}
}
