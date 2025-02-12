public class Product
{
    public int Id { get; set; } = 0;
    public string Title { get; set; } = "";
    public double Price { get; set; } = 0;
    public string Description { get; set; } = "";
    public string Category { get; set; } = "";
    public string Image { get; set; } = "";

    // Rating as a property (correct way to define it as a nested class)
    public Rating? UserRating { get; set; }

    // Nested Rating class inside Product
    public class Rating
    {
        public double Rate { get; set; }
        public int Count { get; set; }
    }
}
