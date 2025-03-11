using AutoMapper;
using FluentValidation;
using LibrariesWeb.Application.Dtos;
using LibrariesWeb.Application.Exceptions;
using LibrariesWeb.Application.Request;
using LibrariesWeb.Application.Services.Interfaces;
using LibrariesWeb.Domain.Entities;
using LibrariesWeb.Domain.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;

namespace LibrariesWeb.Application.Services.Implementations
{
    public class BookServices : IBookServices
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _repository;
        private readonly IValidator<BookRequest> _validator;

        public BookServices(IMapper mapper, IBookRepository repository, IValidator<BookRequest> validator)
        {
            _mapper = mapper;
            _repository = repository;
            _validator = validator;
        }
        public async Task<BookDtos> AddBookAsync(BookRequest bookRequest)
        {
            var validatorAuth = await _validator.ValidateAsync(bookRequest);
            if (!validatorAuth.IsValid)
            {
                throw new ValidationException(validatorAuth.Errors);
            }

           var author = await _repository.GetAuthorIdAsync(bookRequest.AuthorId);
           if (author == null) 
           {
                throw new Exception("Author not found");
           }
            var book = _mapper.Map<Book>(bookRequest);
            await _repository.AddBookAsync(book);
            return _mapper.Map<BookDtos>(book);
        }

        public async Task AddImageToBookAsync(Guid bookId, IFormFile ImageFile)
        {
            if (ImageFile.Length > 0)
            {
                var imagePath = Path.Combine("wwwroot/images", Guid.NewGuid() + Path.GetExtension(ImageFile.FileName));

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                await _repository.AddImageToBooks(bookId, "/images/" + Path.GetFileName(imagePath));

            }
        }

        public async Task<BookDtos> DeleteBookAsync(Guid bookId)
        {
            var book = await CheckIfBookExists(bookId);
            await _repository.DeleteBookAsync(book);
            return _mapper.Map<BookDtos>(book);
        }
        public async Task<List<BookDtos>> GetAllBookAsync()
        {
           var book = await _repository.GetAllBooks();
            return _mapper.Map<List<BookDtos>>(book);
        }
        public async Task<BookDtos> GetBookByIdAsync(Guid bookId)
        {
            var book = await CheckIfBookExists(bookId);
            return _mapper.Map<BookDtos>(book);
        }

        public async Task<BookDtos> GetBookByISBNAsync(string ISBN)
        {
            var book = await _repository.GetBookByISBNAsync(ISBN);
            if (book == null)
            {
                throw new NotFoundException($"No book found with ISBN: {ISBN}");
            }
            return _mapper.Map<BookDtos>(book); ;
        }

        public async Task<BookDtos> UpdateBookAsync(Guid bookId, BookRequest bookRequest)
        {
            var book = await CheckIfBookExists(bookId);
            _mapper.Map(bookRequest, book);
            await _repository.UpdateBookAsync(book);
            return _mapper.Map<BookDtos>(book);
        }

        private async Task<Book> CheckIfBookExists(Guid bookId)
        {
            var book = await _repository.GetBookByIdAsync(bookId);

            if (book is null)
            {
                throw new NotFoundException("($\"There is no product with the provided id : {bookId}\");");
            }

            return book;
        }
    }
}
