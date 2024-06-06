using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiMusicalLibrary.Models.Dto.Query
{
    public class FilterParametersAlbum
    {
        public int? IdSinger { get; set; }
        public int? IdAlbum { get; set; }
        public string? IdCountry { get; set; }
        public int? IdMusicGenre { get; set; }
    }
}