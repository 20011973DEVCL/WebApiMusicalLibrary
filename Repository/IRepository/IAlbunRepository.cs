using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiMusicalLibrary.Models;

namespace WebApiMusicalLibrary.Repository.IRepository
{
    public interface IAlbunRepository:IRepository<Albun>
    {
        Task<Albun> Update(Albun entity);
    }
}