using NUnit.Framework;

namespace DotnetBuildAgent.Tests
{
	public class SuccessExampleTest
	{
		[Test]
		public void SuccessTest()
		{
			bool expected = true;
			bool actual = true;
			Assert.AreEqual(expected, actual);
		}
	}
}