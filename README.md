# Online-Auction-system
 I picked an online auction system on multiple items such as cars, exotic pieces and other valuables. Ill include user authenticated items listings, bidding system, auction timer, real time updates , payment processing and admin dashboard. 
press the code tab to see the design card 
  +--------------------------------+
|         User                   |
+--------------------------------+
| - Username : string            |
| - Password : string            |
+--------------------------------+
| + Login(username, password) : bool |
+--------------------------------+

             ▲
             |
             |
+--------------------------------+
|         Admin (inherits User)  |
+--------------------------------+
| + Login(username, password) : bool (override) |
+--------------------------------+

+--------------------------------+
|         AuctionItem            |
+--------------------------------+
| - ItemId : int                 |
| - Title : string               |
| - Description : string         |
| - StartingPrice : decimal      |
| - CurrentHighestBid : decimal  |
| - HighestBidder : User          |
| - IsSold : bool                |
+--------------------------------+
| + PlaceBid(bidder, amount) : bool |
| + MarkAsSold() : void          |
| + ToString() : string          |
+--------------------------------+

+--------------------------------+
|         AuctionService         |
+--------------------------------+
| - auctionItems : List<AuctionItem> |
+--------------------------------+
| + AddAuctionItem(title, description, price) : void |
| + ListAuctionItems() : void |
| + PlaceBid(itemId, bidder, amount) : bool |
| + CloseAuction(itemId) : void |
| + GetAuctionItemById(itemId) : AuctionItem |
| + GetAllItems() : List<AuctionItem> |
| + CheckAndCloseExpiredAuctions() : void |
+--------------------------------+

+--------------------------------+
|      NotificationService       |
+--------------------------------+
| + NotifyUser(user, message) : void |
+--------------------------------+

+--------------------------------+
|        PaymentService          |
+--------------------------------+
| + ProcessPayment(user, amount) : bool |
+--------------------------------+
