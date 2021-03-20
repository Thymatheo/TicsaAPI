using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicsaAPI
{
    public class Response<T>
    {
        /// <summary>
        /// Define if the query was a succes or a failure
        /// </summary>
        public bool Succes { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Data of the response query
        /// </summary>
        public T Data { get; set; }
    }
}
