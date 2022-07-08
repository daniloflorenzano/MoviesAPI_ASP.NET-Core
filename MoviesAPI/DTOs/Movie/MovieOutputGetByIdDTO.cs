namespace MoviesAPI.DTOs.Movie
{
    public class MovieOutputGetByIdDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string DirectorName { get; set; }

        public MovieOutputGetByIdDTO(long id, string title, string directorName)
        {
            Id = id;
            Title = title;
            DirectorName = directorName;
        }
    }
}
