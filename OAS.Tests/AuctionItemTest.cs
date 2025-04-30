using NUnit.Framework;
using OAS; 

namespace OAS.Tests
{
    [TestFixture] //makrs a class that contains unit tests
    public class AuctionItemTests
    {
        [Test]
        public void AuctionItem_InitialValues_ShouldBeCorrect()
        {
            var item = new AuctionItem("Test Item", "A simple description", 50m);

            Assert.AreEqual("Test Item", item.Title);
            Assert.AreEqual(50m, item.StartingPrice);
            Assert.IsFalse(item.IsSold);
        }
    }
}
