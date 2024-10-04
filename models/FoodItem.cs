using System.Collections.Generic;

namespace Models
{
    public class FoodItem
    {
        public string Codigo { get; set; } = string.Empty; 
        public string Nome { get; set; } = string.Empty; 
        public string NomeCientifico { get; set; } = string.Empty;  
        public string Grupo { get; set; } = string.Empty; 
         public string DetalheUrl { get; set; } = string.Empty; 
        public List<string[]> DetailData { get; set; } = new List<string[]>();
    }
}