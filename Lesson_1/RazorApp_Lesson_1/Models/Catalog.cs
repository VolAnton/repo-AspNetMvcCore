namespace RazorApp_Lesson_1.Models
{
    public class Catalog
    {
        //public List<Category> Categories { get; set; } = new();

        public List<Category> Categories { get; set; } = new List<Category>
        {
            new Category { Id = 1, Name = "Яблоко" },
            new Category { Id = 2, Name = "Апельсин" },
            new Category { Id = 3, Name = "Банан" },
            new Category { Id = 4, Name = "Груша" },
            new Category { Id = 5, Name = "Киви" },
        };
    }

    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
