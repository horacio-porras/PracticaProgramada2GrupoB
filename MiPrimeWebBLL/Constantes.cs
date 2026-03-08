using System;
using System.Collections.Generic;
using System.Text;

namespace PracticaProgramada2BLL
{
    public static class Constantes
    {

        
        public static class Categoria
        {
            public const string Null = "El objeto categoría no puede ser nulo.";
            public const string InvalidId = "El id de la categoría no puede ser 0.";
            public const string ErrorActualizar = "Error al actualizar la categoría en la base de datos.";
            public const string ErrorGuardar = "Error al guardar la categoría en la base de datos.";
            public const string ErrorEliminar = "Error al eliminar la categoría en la base de datos.";
            public const string NotFound = "La categoría no existe.";
        }

        public static class Producto
        {
            public const string Null = "El objeto producto no puede ser nulo.";
            public const string InvalidId = "El id del producto no puede ser 0.";
            public const string ErrorActualizar = "Error al actualizar el producto en la base de datos.";
            public const string ErrorGuardar = "Error al guardar el producto en la base de datos.";
            public const string ErrorEliminar = "Error al eliminar el producto en la base de datos.";
            public const string NotFound = "El producto no existe.";
        }
    }
}
