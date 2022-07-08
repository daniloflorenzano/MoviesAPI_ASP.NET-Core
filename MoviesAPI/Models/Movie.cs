namespace MoviesAPI.Models
{
    public class Movie
    {
        public Movie(string titulo)
        {
            Titulo = titulo;
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Ano { get; set; }
        public string Genre { get; set; }
        public long DirectorId { get; set; }
        public Director Director { get; set; }

    }
}
