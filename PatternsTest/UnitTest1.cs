using PatternsApp;

namespace PatternsTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public void IsSingelton()
        {
            var db = SingeltonDatabase.Instance;
            var db2 = SingeltonDatabase.Instance;
            Assert.That(db, Is.EqualTo(db2));
            Assert.That(SingeltonDatabase.Cout, Is.EqualTo(1));
        }
    }
}