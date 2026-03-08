using System;
using System.Collections.Generic;
using System.Text;

namespace PracticaProgramada2DAL.Repositorios.Generico
{
    public interface IRepositorioGenerico<T> where T : class 
    {
        Task<T> ObtenerPorIdAsync(int id);
        Task<List<T>> ObtenerTodosAsync();
        void AgregarAsync(T entidad);
        void ActualizarAsync(T entidad);
        void EliminarAsync(int id);

        //Confirmación
        Task<bool> GuardarCambiosAsync(); 


    }
}
