using System;
using System.Collections.Generic;
using System.Text;
using TicsaAPI.BLL.BS.Interface;
using TicsaAPI.DAL.DataProvider;
using TicsaAPI.DAL.DataProvider.Interface;

namespace TicsaAPI.BLL.BS
{
    public class BsGammeType : IBsGammeType
    {
        private IDpGammeType _dpGammeType { get; set; }
        public BsGammeType(IDpGammeType dpGammeType)
        {
            _dpGammeType = dpGammeType ;
        }
    }
}
