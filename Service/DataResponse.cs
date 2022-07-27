using Domain;
using System;
using System.Collections.Generic;

namespace Service
{
    public class DataResponse<T> : Response
    {
        public List<T> Data { get; set; }

        public static implicit operator DataResponse<T>(SingleResponse<Anuncio> v)
        {
            throw new NotImplementedException();
        }
    }
}
