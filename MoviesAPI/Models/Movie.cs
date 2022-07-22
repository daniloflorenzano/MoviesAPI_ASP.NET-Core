namespace MoviesAPI.Models
{
    public class Movie
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public long DirectorId { get; set; }
        public Director Director { get; set; }

        public Movie(string title, long directorId)
        {
            Title = title;
            DirectorId = directorId;
        }
    }
}
