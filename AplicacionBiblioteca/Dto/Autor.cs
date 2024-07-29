namespace AplicacionBiblioteca.Dto
{
    public class Autor
    {
        static int cont = 1;
        public Autor(string nombre)
        {
            Identificador = cont;
            Nombre = nombre;
            cont++;
        }
        public int Identificador { get; set; }
        public string Nombre { get; set; }
    }
}
