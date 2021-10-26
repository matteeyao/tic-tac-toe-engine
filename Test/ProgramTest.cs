using App;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    public class ProgramTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void RunsInstanceOfGame()
        {
            Assert.Pass();
        }
    }
}
