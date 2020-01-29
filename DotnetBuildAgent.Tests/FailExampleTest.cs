using NUnit.Framework;

namespace DotnetBuildAgent.Tests
{
	public class FailExampleTest
	{
		[Test]
		public void FailTest()
		{
			bool expected = true;
			bool actual = false;
			Assert.AreEqual(expected, actual);
		}
	}
}
