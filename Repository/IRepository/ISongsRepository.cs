using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiMusicalLibrary.Models;

namespace WebApiMusicalLibrary.Repository.IRepository
{
    public interface ISongsRepository:IRepository<Songs>
    {
        Task<Songs> Update(Songs entity);
    }
}