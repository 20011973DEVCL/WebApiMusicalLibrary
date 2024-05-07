using AutoMapper;
using WebApiMusicalLibrary.Models;

namespace WebApiMusicalLibrary
{
    public class MappingConfig:Profile
    {
        public MappingConfig() {
            CreateMap<Albun,AlbunDto>().ReverseMap();
            CreateMap<Albun,AlbunCreateDto>().ReverseMap();
            CreateMap<Albun,AlbunUpdateDto>().ReverseMap();

            CreateMap<BandSinger,BandSingerDto>().ReverseMap();
            CreateMap<BandSinger,BandSingerCreateDto>().ReverseMap();
            CreateMap<BandSinger,BandSingerUpdateDto>().ReverseMap();

            CreateMap<Country,CountryDto>().ReverseMap();
            CreateMap<Country,CountryCreateDto>().ReverseMap();
            CreateMap<Country,CountryUpdateDto>().ReverseMap();

            CreateMap<Genre,GenreDto>().ReverseMap();
            CreateMap<Genre,GenreCreateDto>().ReverseMap();
            CreateMap<Genre,GenreUpdateDto>().ReverseMap();

            CreateMap<Songs,SongsDto>().ReverseMap();
            CreateMap<Songs,SongsCreateDto>().ReverseMap();
            CreateMap<Songs,SongsUpdateDto>().ReverseMap();
        }
    }
}