using PracticaProgramada.Models;
using PracticaProgramada.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PracticaProgramada.Services
{
    public class CategoriaService
    {
        private readonly IGenericRepository<Categoria> _repository;

        public CategoriaService(IGenericRepository<Categoria> repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Categoria>> GetAll() => _repository.GetAll();
        public Task<Categoria> GetById(int id) => _repository.GetById(id);
        public Task Add(Categoria categoria) => _repository.Add(categoria);
        public Task Update(Categoria categoria) => _repository.Update(categoria);
        public Task Delete(int id) => _repository.Delete(id);

    }
}
