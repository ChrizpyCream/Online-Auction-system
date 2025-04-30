using NUnit.Framework;
using OAS; 

namespace OAS.Tests
{
    [TestFixture]
    public class AuctionServiceTests
    {
        private AuctionService _auctionService;

        [SetUp]
        public void Setup()
        {
            _auctionService = new AuctionService();
        }

        [Test]
        public void AddAuctionItem_ShouldIncreaseItemCount()
        {
            // Arrange
            int initialCount = _auctionService.GetAllItems().Count;

            // Act
            _auctionService.AddAuctionItem("Test Item", "Test Description", 100m);
            int afterAddCount = _auctionService.GetAllItems().Count;

            // Assert
            Assert.AreEqual(initialCount + 1, afterAddCount);
        }

        [Test]
        public void PlaceBid_ShouldUpdateCurrentHighestBid()
        {
            // Arrange
            _auctionService.AddAuctionItem("Vintage Watch", "1960s watch", 100m);
            var item = _auctionService.GetAllItems()[0];
            var bidder = new User("testuser", "password");

            // Act
            bool bidResult = _auctionService.PlaceBid(item.ItemId, bidder, 150m);

            // Assert
            Assert.IsTrue(bidResult);
            Assert.AreEqual(150m, item.CurrentHighestBid);
            Assert.AreEqual(bidder, item.HighestBidder);
        }

        [Test]
        public void CloseAuction_ShouldMarkItemAsSold()
        {
            // Arrange
            _auctionService.AddAuctionItem("Antique Clock", "Old clock", 200m);
            var item = _auctionService.GetAllItems()[0];

            // Act
            _auctionService.CloseAuction(item.ItemId);

            // Assert
            Assert.IsTrue(item.IsSold);
        }
    }
}
