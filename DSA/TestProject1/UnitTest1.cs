
using DSA1;
using NUnit.Framework;

namespace TestProject1
{
    [TestFixture]
    public class Tests
    {
        private DSA1.MainWindow b;
        [SetUp]
        public void Setup()
        {
      
        }

        [Test]
        public void Test1()
        {
            //arrrage
            var main = new LoginValidator();

            //Act
            var result = main.LoggedIn("Cati","Heskey");

            //Assert 
            Assert.AreEqual(true,result);

            //Assert.Pass();
            //Assert.IsTrue(true);
        }


    }
}