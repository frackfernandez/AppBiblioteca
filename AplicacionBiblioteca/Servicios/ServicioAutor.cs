using AplicacionBiblioteca.Dto;

namespace AplicacionBiblioteca.Servicios
{
    public static class ServicioAutor
    {
        public static void AgregarAutor(List<Autor> autores, Autor autor)
        {
            autores.Add(autor);
            Console.WriteLine("Autor agregado correctamente.");
        }
        public static void EliminarAutor(List<Autor> autores, int id)
        {
            Autor elementoAEliminar = autores.Find(x => x.Identificador == id);
            if (elementoAEliminar != null)
            {
                autores.Remove(elementoAEliminar);
                Console.WriteLine("Autor eliminado correctamente.");
            }
            else
            {
                Console.WriteLine("Autor no encontrado.");
            }
        }
        public static void ModificarAutor(List<Autor> autores, int id, string nuevoNombre)
        {
            Autor elementoAEditar = autores.Find(x => x.Identificador == id);
            if (elementoAEditar != null)
            {
                elementoAEditar.Nombre = nuevoNombre;
                Console.WriteLine("Autor modificado correctamente.");
            }
            else
            {
                Console.WriteLine("Autor no encontrado");
            }
        }
        public static void MostrarAutores(List<Autor> autores)
        {
            Console.WriteLine("Lista de autores:");
            foreach (var item in autores)
            {
                Console.WriteLine($"Id: {item.Identificador}, Nombre: {item.Nombre}");
            }
        }
    }
}
