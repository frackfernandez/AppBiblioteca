using AplicacionBiblioteca.Dto;

namespace AplicacionBiblioteca.Servicios
{
    public static class ServicioLibro
    {
        public static void AgregarLibro(List<Libro> libros, Libro libro)
        {
            libros.Add(libro);
            Console.WriteLine("Libro agregado correctamente.");
        }
        public static void EliminarLibro(List<Libro> libros, int id)
        {
            Libro elementoAEliminar = libros.Find(x => x.Identificador == id);
            if (elementoAEliminar != null)
            {
                libros.Remove(elementoAEliminar);
                Console.WriteLine("Libro eliminado correctamente");
            }
            else
            {
                Console.WriteLine("Libro no encontrado.");
            }
        }
        public static void ModificarLibroCredito(List<Libro> libros, int id, int nuevoCredito)
        {
            Libro elementoAEditar = libros.Find(x => x.Identificador == id);
            if (elementoAEditar != null)
            {
                elementoAEditar.Credito = nuevoCredito;
                Console.WriteLine("Se modifico el credito correctamente.");
            }
            else
            {
                Console.WriteLine("Libro no encontrado.");
            }
        }
        public static void ModificarLibroStock(List<Libro> libros, int id, int nuevoStock)
        {
            Libro elementoAEditar = libros.Find(x => x.Identificador == id);
            if (elementoAEditar != null)
            {
                elementoAEditar.Stock = nuevoStock;
                Console.WriteLine("Se modifico el stock correctamente.");
            }
            else
            {
                Console.WriteLine("Libro no encontrado.");
                Console.WriteLine();
            }
        }
        public static void MostrarLibros(List<Libro> libros, List<Autor> autores)
        {
            int idautor;
            string autorlistar;

            Console.WriteLine("Lista de libros:");

            foreach (var item in libros)
            {
                Autor elementoencontrado = autores.Find(x => x.Identificador == item.Autor);
                if (elementoencontrado == null)
                {
                    Console.WriteLine($"Id: {item.Identificador}, Titulo: {item.Titulo}, Autor: {item.Autor}. NO EXISTE, Categoria: {item.Categoria}, Credito: {item.Credito}, Stock: {item.Stock}");
                }
                else
                {
                    Console.WriteLine($"Id: {item.Identificador}, Titulo: {item.Titulo}, Autor: {elementoencontrado.Nombre}, Categoria: {item.Categoria}, Credito: {item.Credito}, Stock: {item.Stock}");
                }
            }
        }
    }
}
