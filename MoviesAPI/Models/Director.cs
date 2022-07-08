namespace MoviesAPI.Models
{
    public class Director
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<Movie> Movies { get; set; }

        public Director(string name)
        {
            Name = name;
            Movies = new List<Movie>();
        }
    }
}
