namespace OnlineShop.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public int ProductCost { get; set; }

        public string ProductOwner { get; set; }

        public string OwnerEmail { get; set; }
    }
}
