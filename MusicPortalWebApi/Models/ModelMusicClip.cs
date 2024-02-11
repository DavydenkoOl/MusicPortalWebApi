namespace MusicPortalWebApi.Models
{
    public class ModelMusicClip
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? ReleaseDate { get; set; }

        public string? Artist { get; set; }

        public string[]? Genre { get; set; }

        public IFormFile? attachment { get; set; }

        public int? Id_user { get; set; }
    }
}
