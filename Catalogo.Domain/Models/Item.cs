namespace Catalogo.Domain.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Generacion { get; set; }
        public string Tipo { get; set; } 
        public string Rutas { get; set; }
        public string Descripcion { get; set; }
        public string ImagenUrl { get; set; } 
    }
}
