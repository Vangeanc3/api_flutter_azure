namespace api_flutter.Models
{
    public class Tarefa
    {
        public string Titulo { get; set; } = null!;
        public string UrlFoto { get; set; } = null!;
        public int? Dificuldade { get; set; }
    }
}