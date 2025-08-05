using Azure.Core;
using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTO.Responses;
using KASHOP.DAL.Model;
using KASHOP.DAL.Repositories.Class;
using KASHOP.DAL.Repositories.Interfaces;
using KASHOP.DAL.Repositortrs;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Class
{
    public class GenericServices<TRequest, TResponse, TEntity> : IGenericServices<TRequest, TResponse, TEntity> where TEntity : BaseModel
    {
        private readonly IGenericRepository<TEntity> _genericRepository;
        public GenericServices(IGenericRepository<TEntity> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public int Create(TRequest request)
        {
            var generic = request.Adapt<TEntity>();
            return _genericRepository.Add(generic);
        }

        public int Delete(int id)
        {
            var generic = _genericRepository.GetById(id);
            if (generic is null) { return 0; }
            return _genericRepository.Remove(generic);
        }

        public IEnumerable<TResponse> GetAll(bool onlyActive=false)
        {
            var generic = _genericRepository.GetAll();
            if (onlyActive) {

                generic= generic.Where(e=>e.Status==Status.Active);
                
            }
            return generic.Adapt<IEnumerable<TResponse>>();
        }

        public TResponse? GetById(int id)
        {
            var generic = _genericRepository.GetById(id);
            return generic is null ? default : generic.Adapt<TResponse>();
        }

        public bool ToggleStatus(int id)
        {
            var generic = _genericRepository.GetById(id);
            if (generic is null) { return false; }
            generic.Status = generic.Status == Status.Active ? Status.Active : Status.Inactive;
            _genericRepository.Update(generic);
            return true;
        }

        public int Update(int id, TRequest request)
        {
            var generic = _genericRepository.GetById(id);
            if (generic is null) { return 0; }
            var updatedEntity=request.Adapt(generic);
            return _genericRepository.Update(updatedEntity);
        }
    }


}
