using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;

namespace PracticaMad.Model.ValoracionDAO
{
    public interface IValoracionDAO : IGenericDao<Valoracion, Int64>
    {

        List<Valoracion> FindByVendor(long loginVendor, int startIndex, int count);
    }
}
