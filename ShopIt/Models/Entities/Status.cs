namespace ShopIt.Models.Entities
{
    public class Status : BaseEntity<int>
    {
        public string Title { get; set; }
        public string Icon { get; set; }
    }
}