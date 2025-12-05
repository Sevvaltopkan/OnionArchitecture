using AutoMapper;
using OnionVb02.Application.DTOInterfaces;
using OnionVb02.Application.ErrorHandling;
using OnionVb02.Application.ManagerInterfaces;
using OnionVb02.Contract.RepositoryInterfaces;
using OnionVb02.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnionVb02.InnerInfrastructure.ManagerConcretes
{
    public abstract class BaseManager<T, U> : IManager<T, U> where T : class, IDto where U : class, IEntity
    {
        private readonly IRepository<U> _repository;
        protected readonly IMapper _mapper;

        public BaseManager(IRepository<U> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateAsync(T entity)
        {
            try
            {
                U domainEntity = _mapper.Map<U>(entity);
                domainEntity.CreatedDate = DateTime.Now;
                domainEntity.Status = Domain.Enums.DataStatus.Inserted;

                await _repository.CreateAsync(domainEntity);
            }
            catch (Exception ex)
            {
                throw new BusinessException("Veri oluşturulurken hata oluştu", ex);
            }
        }

        public List<T> GetActives()
        {
            try
            {
                List<U> values = _repository.Where(x => x.Status != Domain.Enums.DataStatus.Deleted).ToList();
                return _mapper.Map<List<T>>(values);
            }
            catch (Exception ex)
            {
                throw new BusinessException("Aktif veriler getirilirken hata oluştu", ex);
            }
        }

        public async Task<List<T>> GetAllAsync()
        {
            try
            {
                List<U> values = await _repository.GetAllAsync();
                return _mapper.Map<List<T>>(values);
            }
            catch (Exception ex)
            {
                throw new BusinessException("Veriler getirilirken hata oluştu", ex);
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            try
            {
                U value = await _repository.GetByIdAsync(id);
                
                if (value == null)
                    throw new NotFoundException(typeof(U).Name, id);
                
                return _mapper.Map<T>(value);
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"ID: {id} ile veri getirilirken hata oluştu", ex);
            }
        }

        public List<T> GetPassives()
        {
            try
            {
                List<U> values = _repository.Where(x => x.Status == Domain.Enums.DataStatus.Deleted).ToList();
                return _mapper.Map<List<T>>(values);
            }
            catch (Exception ex)
            {
                throw new BusinessException("Pasif veriler getirilirken hata oluştu", ex);
            }
        }

        public List<T> GetUpdateds()
        {
            try
            {
                List<U> values = _repository.Where(x => x.Status == Domain.Enums.DataStatus.Updated).ToList();
                return _mapper.Map<List<T>>(values);
            }
            catch (Exception ex)
            {
                throw new BusinessException("Güncellenmiş veriler getirilirken hata oluştu", ex);
            }
        }

        public async Task<string> HardDeleteAsync(int id)
        {
            try
            {
                U value = await _repository.GetByIdAsync(id);
                
                if (value == null)
                    throw new NotFoundException(typeof(U).Name, id);
                    
                if (value.Status != Domain.Enums.DataStatus.Deleted) 
                    throw new BusinessException("Veri silinebilmesi için önce pasif hale getirilmelidir");
                
                await _repository.DeleteAsync(value);
                return $"{id} id'li veri kalıcı olarak silindi";
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (BusinessException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"ID: {id} silinirken hata oluştu", ex);
            }
        }

        public async Task<string> SoftDeleteAsync(int id)
        {
            try
            {
                U value = await _repository.GetByIdAsync(id);
                
                if (value == null)
                    throw new NotFoundException(typeof(U).Name, id);
                    
                if (value.Status == Domain.Enums.DataStatus.Deleted) 
                    throw new BusinessException("Veri zaten pasif durumda");
                
                value.Status = Domain.Enums.DataStatus.Deleted;
                value.DeletedDate = DateTime.Now;
                await _repository.SaveChangesAsync();
                
                return $"{id} id'li veri pasif hale getirildi";
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (BusinessException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"ID: {id} pasif hale getirilirken hata oluştu", ex);
            }
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                U originalValue = await _repository.GetByIdAsync(entity.Id);
                
                if (originalValue == null)
                    throw new NotFoundException(typeof(U).Name, entity.Id);
                
                U newValue = _mapper.Map<U>(entity);
                newValue.Status = Domain.Enums.DataStatus.Updated;
                newValue.UpdatedDate = DateTime.Now;
                
                await _repository.UpdateAsync(originalValue, newValue);
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new BusinessException("Veri güncellenirken hata oluştu", ex);
            }
        }
    }
}
