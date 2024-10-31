using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PracticaMad.Model.ValoracionDAO
{
    public class ValoracionDAOEntityFramework : GenericDaoEntityFramework<Valoracion, Int64>, IValoracionDAO
    {


        public List<Valoracion> FindByVendor(long vendor, int startIndex, int count)
        {

            DbSet<Valoracion> vals = Context.Set<Valoracion>();

            var result =
                 (from v in vals
                  where v.Producto1.vendedor == vendor
                  orderby v.date
                  select v).Skip(startIndex).Take(count).ToList();

            return result;
        }

        public void UpdateVendorScore(long productId) { 
        }
    }
}
