using System;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace PracticaMad.Model.ProductoDAO
{
    public interface IProductoDao : IGenericDao<Producto, Int64>
    {

        List<Producto> FindByProductName(string productName, int startIndex, int count);

        int GetNumberOfProductsByName(string productName);
    }
}
