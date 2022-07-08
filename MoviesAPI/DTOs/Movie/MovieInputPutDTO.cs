namespace MoviesAPI.DTOs.Movie
{
    public class MovieInputPutDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public long DirectorId { get; set; }
        public MovieInputPutDTO(long id, string title, long directorId)
        {
            Id = id;
            Title = title;
            DirectorId = directorId;
        }
    }
}
