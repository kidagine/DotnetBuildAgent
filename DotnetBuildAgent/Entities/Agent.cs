namespace DotnetBuildAgent
{
	public enum AgentType { Build, Test };

	public class Agent
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Path { get; set; }
		public int TimeInterval { get; set; }
		public AgentType AgentType { get; set; }
	}
}
