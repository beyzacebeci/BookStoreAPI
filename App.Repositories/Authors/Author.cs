namespace App.Repositories.Authors
{
    public class Author : ISoftDelete
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public bool IsDeleted { get; set; }
    }
}
