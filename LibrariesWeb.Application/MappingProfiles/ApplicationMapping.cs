using AutoMapper;
using LibrariesWeb.Application.Dtos;
using LibrariesWeb.Application.Request;
using LibrariesWeb.Domain.Entities;
using LibrariesWeb.Shared.RequestFeatures;
using System.Numerics;

namespace LibrariesWeb.Application.MappingProfiles
{
    public class ApplicationMapping:Profile
    {
        public ApplicationMapping()
        {
            CreateMap<UserRegisterRequest, AppUser>()
                .ReverseMap();

            CreateMap<AuthorRequest, Author>()
                .ReverseMap();

            CreateMap<Author, AuthorDtos>();
            CreateMap<PagedList<Author>, AuthorsResponse>()
                .ForMember(dest => dest.MetaData, opt => opt.MapFrom(src => src.MetaData))
                .ConstructUsing((src, context) => new AuthorsResponse(
                src.Select(author => context.Mapper.Map<AuthorDtos>(author)).ToList(),
                src.MetaData));

            CreateMap<BookRequest, Book>()
                .ReverseMap();

            CreateMap<Book, BookDtos>()
                .ReverseMap();

        }
    }
}
