using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PracticaMad.Model.ProductoDAO
{
    public class ProductoDaoEntityFramework : GenericDaoEntityFramework<Producto, Int64>, IProductoDao
    {

        public List<Producto> FindByProductName(string productName, int startIndex, int count)
        {

            DbSet<Producto> prods = Context.Set<Producto>();

            var result =
                 (from p in prods
                  where p.nombre.Contains(productName)
                  orderby p.date
                  select p).Skip(startIndex).Take(count).ToList();
            
            return result;
        }

        public int GetNumberOfProductsByName(string productName)
        {
            DbSet<Producto> prods = Context.Set<Producto>();

            var result =
                 (from p in prods
                  where p.nombre.Contains(productName)
                  select p).Count();

            return result;
        }
    }
}
