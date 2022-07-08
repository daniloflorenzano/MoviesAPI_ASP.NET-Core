namespace MoviesAPI.DTOs.Movie
{
    public class MovieOutputPostDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }

        public MovieOutputPostDTO(long id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
