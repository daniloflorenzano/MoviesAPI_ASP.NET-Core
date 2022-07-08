namespace MoviesAPI.DTOs
{
    public class DirectorOutputGetByIdDTO
    {
        public long Id { get; private set; }
        public string Name { get; private set; }

        public DirectorOutputGetByIdDTO(long id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
