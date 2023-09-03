namespace My_Store_API.Models
{
	public class BuyItemResponse
	{
		public int itemId { set; get; }
		public string itemName { set; get; }
		public double itemPrice { set; get; }
		public int itemQuantity { set; get; }
		public double totalPrice { set; get; }
		public string message { set; get; }
		public bool status { set; get; }
	}
}
