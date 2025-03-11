using AutoMapper;
using FluentValidation;
using LibrariesWeb.Application.Dtos;
using LibrariesWeb.Application.Exceptions;
using LibrariesWeb.Application.Request;
using LibrariesWeb.Application.Services.Interfaces;
using LibrariesWeb.Domain.Entities;
using LibrariesWeb.Domain.Repositories.Interfaces;
using LibrariesWeb.Shared.RequestFeatures;

namespace LibrariesWeb.Application.Services.Implementations
{
    public class AuthorServices : IAuthorServices
    {
        private readonly IMapper _mapper;
        private readonly IAuthorRepository _repository;
        private readonly IValidator<AuthorRequest> _validator;
        public AuthorServices(IMapper mapper, IAuthorRepository repository, IValidator<AuthorRequest> validator)
        {
            _mapper = mapper;
            _repository = repository;
            _validator = validator;
        }

        public async Task<AuthorDtos> AddAuthorAsync(AuthorRequest authorRequest)
        {
            var validatorAuth = await _validator.ValidateAsync(authorRequest);
            if (!validatorAuth.IsValid)
            {
                throw new ValidationException(validatorAuth.Errors);
            }
            var auth = _mapper.Map<Author>(authorRequest);
            await _repository.AddAuthorAsync(auth);
            return _mapper.Map<AuthorDtos>(auth);

        }

        public async Task<AuthorDtos?> DeleteAuthorAsync(Guid authorId)
        {
            var auth = await CheckIfAuthorExists(authorId);
            await _repository.DeleteAuthorAsync(auth);
            return _mapper.Map<AuthorDtos>(auth);
        }

        public async Task<AuthorsResponse> GetAllAuthorAsync(AuthorParameters parameters)
        {
            var auth = await _repository.GetAllAuthors(parameters);
            return _mapper.Map<AuthorsResponse>(auth);
        }

        public async Task<AuthorDtos> GetAuthorByIdAsync(Guid AuthorId)
        {
            var auth = await CheckIfAuthorExists(AuthorId);
            return _mapper.Map<AuthorDtos>(auth);

        }

        public async Task<AuthorDtos> UpdateAuthorAsync(Guid AuthorId, AuthorRequest authorRequest)
        {
            var auth = await CheckIfAuthorExists(AuthorId);
            _mapper.Map(authorRequest, auth);
            await _repository.UpdateAuthorAsync(auth);
            return _mapper.Map<AuthorDtos>(auth);
        }

        private async Task<Author> CheckIfAuthorExists(Guid authorId)
        {
            var auth = await _repository.GetAuthorById(authorId);

            if (auth is null)
            {
                throw new NotFoundException("($\"There is no product with the provided id : {authorId}\");");
            }

            return auth;
        }
    }
}
