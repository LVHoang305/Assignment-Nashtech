using LibraryManagement.Models.CreateDTOs;
using LibraryManagement.Repository.BaseRepository;
using LibraryManagement.Models;
using AutoMapper;
using System.Text.Json;

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

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            //var dtos = _mapper.Map<IEnumerable<UserDTO>>(entities);
            return entities;
        }

        public async Task<UserDTO> GetByIdAsync(int Id)
        {
            var entity = await _repository.GetByIdAsync(Id);
            var dto = _mapper.Map<UserDTO>(entity);
            return dto;

        }

        public async Task<User> GetByEmail(string email)
        {
            var emailObject = new { email = email };
            string jsonString = JsonSerializer.Serialize(emailObject);
            var dtos = await _repository.GetByFieldsAsync(jsonString);
            return dtos.FirstOrDefault();
        }

        public async Task<User> CreateAsync(UserCreateDTO entityDTO)
        {
            var entity = _mapper.Map<User>(entityDTO);
            entity.IsSuperUser = false;
            entity.PasswordHash = BCrypt.Net.BCrypt.HashPassword(entity.PasswordHash, BCrypt.Net.BCrypt.GenerateSalt());
            _repository.CreateAsync(entity);
            return entity;
        }

        public void UpdateAsync(int id, UserDTO entityDTO)
        {
            var entity = _mapper.Map<User>(entityDTO);
            entity.Id = id;
            _repository.UpdateAsync(entity);
        }

        public bool VerifyPassword(string password, string storedHash)
        {
            if (password == storedHash)
            {
                return true;
            }
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }
    }
}

