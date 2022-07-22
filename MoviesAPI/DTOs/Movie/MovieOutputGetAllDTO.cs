namespace MoviesAPI.DTOs.Movie
{
    public class MovieListOutputGetAllDTO
    {
        public int CurrentPage { get; init; }
        public int TotalItems { get; init; }
        public int TotalPages { get; init; }
        public List<MovieOutputGetAllDTO> Items { get; init; }
    }
    
    public class MovieOutputGetAllDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }

        public MovieOutputGetAllDTO(long id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
