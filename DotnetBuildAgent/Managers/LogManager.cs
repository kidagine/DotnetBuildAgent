using System;
using System.IO;

namespace DotnetBuildAgent
{
	public class LogManager
	{
		private readonly string _logPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DotnetBuildAgentLog.txt");
		private static LogManager _instance = null;

		public string LogText { get; set; }


		public static LogManager Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new LogManager();
				}
				return _instance;
			}
		}

		public void LogBuild()
		{
			using (StreamWriter streamWriter = new StreamWriter(_logPath, true))
			{
				streamWriter.WriteLine($"Build Log Date: {DateTime.Now}");
				streamWriter.WriteLine("---------------------------------------------------------------------------------");
				streamWriter.WriteLine(LogText);
				streamWriter.WriteLine("---------------------------------------------------------------------------------");
			}
		}
	}
}
