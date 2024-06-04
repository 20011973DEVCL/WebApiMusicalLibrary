using AutoMapper;
using WebApiMusicalLibrary.Models;
using WebApiMusicalLibrary.Models.Login;

namespace WebApiMusicalLibrary
{
    public class MappingConfig:Profile
    {
        public MappingConfig() {
            CreateMap<Albun,AlbunDto>().ReverseMap();
            CreateMap<Albun,AlbunCreateDto>().ReverseMap();
            CreateMap<Albun,AlbunUpdateDto>().ReverseMap();

            CreateMap<Singer,SingerDto>().ReverseMap();
            CreateMap<Singer,SingerCreateDto>().ReverseMap();
            CreateMap<Singer,SingerUpdateDto>().ReverseMap();

            CreateMap<Country,CountryDto>().ReverseMap();
            CreateMap<Country,CountryCreateDto>().ReverseMap();
            CreateMap<Country,CountryUpdateDto>().ReverseMap();

            CreateMap<MusicGenre,MusicGenreDto>().ReverseMap();
            CreateMap<MusicGenre,MusicGenreCreateDto>().ReverseMap();
            CreateMap<MusicGenre,MusicGenreUpdateDto>().ReverseMap();

            CreateMap<Songs,SongsDto>().ReverseMap();
            CreateMap<Songs,SongsCreateDto>().ReverseMap();
            CreateMap<Songs,SongsUpdateDto>().ReverseMap();

            CreateMap<MenuOptions, MenuOptionsDto>().ReverseMap();
            CreateMap<MenuOptions, MenuOptionsCreateDto>().ReverseMap();
            CreateMap<MenuOptions, MenuOptionsUpdateDto>().ReverseMap();
        }
    }
}