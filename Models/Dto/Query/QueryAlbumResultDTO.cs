using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiMusicalLibrary.Models.Dto.Query
{
    public class QueryAlbumResultDTO
    {
        public int IdSinger { get; set; }
        public string Singer { get; set; }
        public DateTime? FechaInicioSinger { get; set; }
        public int IdAlbum { get; set; }
        public string Album { get; set; }
        public int AñoAlbum { get; set; }
        public string NotasAlbum { get; set; }
        public string IdPais { get; set; }
        public string Pais { get; set; }
        public int IdGenero { get; set; }
        public string Genero { get; set; }
    }
}