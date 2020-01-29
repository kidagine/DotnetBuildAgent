using System;

namespace DotnetBuildAgent
{
	public class UserInput
	{
		private const string Path = @"C:\Users\Kiddo\Desktop\Code\AgentDev\DotnetBuildAgent\";


		public void Start()
		{
			do
			{
				Console.WriteLine("------------------------");
				Console.WriteLine();
				Console.WriteLine("1. Create an agent");
				Console.WriteLine("2. Delete an agent");
				Console.WriteLine("3. Add agent to queue");
				Console.WriteLine("4. Delete agent from queue");
				Console.WriteLine("5. Start an agent");
				Console.WriteLine("6. Start agent queue");
				Console.WriteLine("7. List all agents");
				Console.WriteLine("8. List all queued agents");
				Console.WriteLine();
				Console.Write("Choose a numbered option: ");

				bool isNumber = int.TryParse(Console.ReadLine(), out int optionNumber);
				if (isNumber && optionNumber > 0 && optionNumber < 9)
				{
					StartOption(optionNumber);
				}
				else if (!isNumber)
				{
					Console.WriteLine("The value you inserted was not a number");
				}
				else
				{
					Console.WriteLine("The value you inserted was not one of the numbered options");
				}
			} while (true);
		}

		public void StartOption(int numberOption)
		{
			Console.WriteLine();
			switch (numberOption)
			{
				case 1:
					CreateAgent();
					break;
				case 2:
					DeleteAgent();
					break;
				case 3:
					AddAgentToQueue();
					break;
				case 4:
					DeleteAgentFromQueue();
					break;
				case 5:
					StartAgent();
					break;
				case 6:
					StartAgentQueue();
					break;
				case 7:
					ListAgents();
					break;
				case 8:
					ListQueuedAgents();
					break;
			}
		}

		public void CreateAgent()
		{
			Console.WriteLine("-Create Agent-");
			Console.Write("Insert name: ");
			string name = Console.ReadLine();

			bool hasSetTimeInterval = default;
			int timeInterval = default;
			do
			{
				Console.Write("Insert interval time: ");
				bool isNumber = int.TryParse(Console.ReadLine(), out int number);
				if (isNumber && number >= 1000)
				{
					hasSetTimeInterval = true;
					timeInterval = number;
				}
				else if (!isNumber)
				{
					Console.WriteLine("The value you inserted was not a number.");
				}
				else
				{
					Console.WriteLine("The time interval you inserted was too low.");
				}
			} while (!hasSetTimeInterval);


			Agent createdAgent = BuildManager.Instance.CreateAgent(name, Path, timeInterval);
			if (createdAgent != null)
			{
				BuildManager.Instance.AddAgent(createdAgent);

				Console.WriteLine();
				Console.WriteLine($"Created agent: {createdAgent.Name}");
			}
		}

		public void DeleteAgent()
		{
			Console.WriteLine("-Insert the name of the agent you wish to delete-");
			Console.Write("Insert agent name: ");
			string agentName = Console.ReadLine();

			Agent deleteAgent = BuildManager.Instance.GetAgent(agentName);
			if (deleteAgent != null)
			{
				BuildManager.Instance.DeleteAgent(deleteAgent);

				Console.WriteLine();
				Console.WriteLine($"Deleted agent: {deleteAgent.Name}");
			}
		}

		public void AddAgentToQueue()
		{
			Console.WriteLine("-Insert the name of the agent you wish to add to the queue-");
			Console.Write("Insert agent name: ");
			string agentName = Console.ReadLine();

			Agent addAgentQueue = BuildManager.Instance.GetAgent(agentName);
			if (addAgentQueue != null)
			{
				BuildManager.Instance.AddAgentToQueue(addAgentQueue);

				Console.WriteLine();
				Console.WriteLine($"Added agent: {addAgentQueue.Name} to queue");
			}
		}

		public void DeleteAgentFromQueue()
		{
			Console.WriteLine("-Insert the name of the agent you wish to delete from the queue-");
			Console.Write("Insert agent name: ");
			string agentName = Console.ReadLine();

			Agent deleteAgentQueue = BuildManager.Instance.GetAgent(agentName);
			if (deleteAgentQueue != null)
			{
				BuildManager.Instance.DeleteAgentFromQueue(deleteAgentQueue);

				Console.WriteLine();
				Console.WriteLine($"Deleted agent: {deleteAgentQueue.Name} from queue");
			}
		}

		public void StartAgent()
		{
			Console.WriteLine("-Insert the name of the agent you wish to start-");
			Console.Write("Insert agent name: ");
			string agentName = Console.ReadLine();

			Console.WriteLine();
			Console.WriteLine($"Starting agent: {agentName}");

			Agent startAgent = BuildManager.Instance.GetAgent(agentName);
			if (startAgent != null)
			{
				BuildManager.Instance.StartAgent(startAgent);
			}
		}

		public void StartAgentQueue()
		{
			Console.WriteLine();
			Console.WriteLine("Starting agent queue...");
			BuildManager.Instance.StartAgentQueue();
		}

		public void ListAgents()
		{
			Console.WriteLine("-List of all agents-");
			BuildManager.Instance.ListAllAgents();
		}

		public void ListQueuedAgents()
		{
			Console.WriteLine("-List of all queued agents-");
			BuildManager.Instance.ListAllQueuedAgents();
		}
	}
}
