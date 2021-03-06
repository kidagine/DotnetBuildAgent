﻿using System;
using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DotnetBuildAgent
{
	public class BuildManager
	{
		private readonly List<Agent> _storedAgents = new List<Agent>();
		private readonly List<Agent> _executedAgents = new List<Agent>();
		private readonly List<Task> _agentTasks = new List<Task>();
		private int _currentAgentId = default;
		private static BuildManager _instance = null;

		public bool IsRunningProcesses { get; set; } = true;
		public CancellationTokenSource cancellationTokenSource { get; set; }


		public static BuildManager Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new BuildManager();
				}
				return _instance;
			}
		}

		public Agent CreateAgent(string name, string path, int timeInterval, AgentType agentType) 
		{
			Agent agent = new Agent { Id = _currentAgentId, Name = name, Path = path, TimeInterval = timeInterval, AgentType = agentType };
			_currentAgentId++;
			return agent;
		}

		public Agent GetAgent(string agentName)
		{
			foreach (Agent agent in _storedAgents)
			{
				if (agent.Name.Equals(agentName))
				{
					Console.WriteLine($"Found agent: ID:{agent.Id}, {agent.Name}");
					return agent;
				}
			}
			Console.WriteLine($"Agent with the name: {agentName} was not found");
			return null;
		}


		public Agent GetQueuedAgent(string agentName)
		{
			foreach (Agent agent in _executedAgents)
			{
				if (agent.Name.Equals(agentName))
				{
					Console.WriteLine($"Found agent: ID:{agent.Id}, {agent.Name}");
					return agent;
				}
			}
			Console.WriteLine($"Agent with the name: {agentName} was not found");
			return null;
		}

		public void AddAgent(Agent agent)
		{
			_storedAgents.Add(agent);
		}

		public void AddAgentToQueue(Agent agent)
		{
			_executedAgents.Add(agent);
		}

		public void DeleteAgent(Agent agent)
		{
			_storedAgents.Remove(agent);
		}

		public void DeleteAgentFromQueue(Agent agent)
		{
			_executedAgents.Remove(agent);
		}

		public void StartAgent(Agent agent)
		{
			Console.WriteLine();
			Console.WriteLine("Type F to exit the automated build");
			Console.WriteLine();
			Thread.Sleep(1000);

			BuildAgent buildAgent = new BuildAgent();
			buildAgent.Build(agent);
		}

		public void StartAgentQueueAsync()	
		{
			if (_executedAgents.Count > 0)
			{
				IsRunningProcesses = true;
				Console.WriteLine();
				Console.WriteLine("Type F to exit the automated build");
				Console.WriteLine();
				Thread.Sleep(1000);

				BuildAgent buildAgent = new BuildAgent();
				foreach (Agent agent in _executedAgents)
				{
					Task task = Task.Run(() => buildAgent.Build(agent));
					_agentTasks.Add(task);
				}
				Task.WaitAll(_agentTasks.ToArray());
			}
			else
			{
				Console.WriteLine();
				Console.WriteLine("No agent in queue.");
			}
		}

		public void ListAllAgents()
		{
			foreach (Agent agent in _storedAgents)
			{
				Console.WriteLine($"ID: {agent.Id}| Name:{agent.Name}| TimeInterval:{agent.TimeInterval}| AgentType:{agent.AgentType}| Path:{agent.Path}");
			}
		}

		public void ListAllQueuedAgents()
		{
			foreach (Agent agent in _executedAgents)
			{
				Console.WriteLine($"ID: {agent.Id}| Name:{agent.Name}| TimeInterval:{agent.TimeInterval}| AgentType:{agent.AgentType}| Path:{agent.Path}");
			}
		}
	}
}
