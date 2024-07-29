namespace AplicacionBiblioteca.Dto
{
    public class Libro
    {
        static int cont = 1;
        public Libro(int autor, string titulo, string categoria, int credito, int stock)
        {
            Identificador = cont;
            Autor = autor;
            Titulo = titulo;
            Categoria = categoria;
            Credito = credito;
            Stock = stock;
            cont++;
        }
        public int Identificador { get; set; }
        public int Autor { get; set; }
        public string Titulo { get; set; }
        public string Categoria { get; set; }
        public int Credito { get; set; }
        public int Stock { get; set; }
    }
}
