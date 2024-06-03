using LibraryManagement.Models.CreateDTOs;
using LibraryManagement.Repository.BaseRepository;
using LibraryManagement.Models;
using AutoMapper;

namespace LibraryManagement.Services
{
    public class UserService: IUserService<UserDTO, UserCreateDTO>
    {
        private readonly IBaseRepository<User> _repository;
        private readonly IMapper _mapper;

        public UserService(IBaseRepository<User> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void DeleteAsync(int Id)
        {
            _repository.DeleteAsync(Id);
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<UserDTO>>(entities);
            return dtos;
        }

        public async Task<UserDTO> GetByIdAsync(int Id)
        {
            var entity = await _repository.GetByIdAsync(Id);
            var dto = _mapper.Map<UserDTO>(entity);
            return dto;

        }

        public void CreateAsync(UserCreateDTO entityDTO)
        {
            var entity = _mapper.Map<User>(entityDTO);
            _repository.CreateAsync(entity);
        }

        public void UpdateAsync(int id, UserDTO entityDTO)
        {
            var entity = _mapper.Map<User>(entityDTO);
            _repository.UpdateAsync(entity);
        }
    }
}

