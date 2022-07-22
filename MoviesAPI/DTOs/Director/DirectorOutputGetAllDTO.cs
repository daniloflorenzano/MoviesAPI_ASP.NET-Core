namespace MoviesAPI.DTOs
{
    public class DirectorListOutputGetAllDTO
    {
        public int CurrentPage { get; init; }
        public int TotalItems { get; init; }
        public int TotalPages { get; init; }
        public List<DirectorOutputGetAllDTO> Items { get; init; }
    }
    
    public class DirectorOutputGetAllDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public DirectorOutputGetAllDTO(long id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
