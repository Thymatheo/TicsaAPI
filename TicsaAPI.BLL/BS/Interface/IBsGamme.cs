﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.BS.Interface
{
    public interface IBsGamme
    {
        Task<IEnumerable<Gammes>> GetAllGamme();
        Task<IEnumerable<Gammes>> GetGammesByIdType(int idType);
        Task<Gammes> GetGammeById(int idGamme);
        Task<Gammes> UpdateGamme(Gammes gamme);
        Task<Gammes> RemoveGamme(Gammes gamme);
        Task<Gammes> AddGamme(Gammes gamme);
    }
}
