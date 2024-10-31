using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;
using PracticaMad.Model.ProductServiceNS;
using PracticaMad.Model.UserServiceNS;
using PracticaMad.Model.ValoracionDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaMad.Model.ValoracionServiceNS
{
    public class ValoracionService : IValoracionService
    {

        public ValoracionService() { }

        [Inject]
        public IValoracionDAO ValoracionDao { private get; set; }

        [Inject]
        public IProductService ProductService { private get; set; }

        [Inject]
        public IUserService UserService { private get; set; }

        [Transactional]
        public Valoracion AddValoracion(long comprador, long product, double score, String comentario=null)
        {
            Valoracion valoracion = new Valoracion();
            valoracion.comprador = comprador;
            valoracion.producto = product;
            valoracion.puntuacion = score;
            valoracion.comentario = comentario;
            DateTime myDateTime = DateTime.Now;
            valoracion.date = myDateTime;
            ValoracionDao.Create(valoracion);
            ProductoDTO.ProductoDTO prod = ProductService.Find(product);
            UserService.UpdateUserScore(prod.vendedor, score);
            return valoracion;
        }

        [Transactional]
        public Valoracion FindValoracionByValId(long valId)
        {
            Valoracion val = ValoracionDao.Find(valId);

            return val;
        }

        [Transactional]
        public ValoracionBlock FindValoracionByVendor(long vendor, int startIndex, int count)
        {
            List<Valoracion> vals =
                ValoracionDao.FindByVendor(vendor, startIndex, count + 1);

            bool existMoreVals = (vals.Count == count + 1);

            if (existMoreVals)
                vals.RemoveAt(count);
            List<ValoracionDTO.ValoracionDTO> final_vals = new List<ValoracionDTO.ValoracionDTO>();
            foreach (Valoracion val in vals)
            {
                final_vals.Add(new ValoracionDTO.ValoracionDTO(val, UserService.Find(val.comprador).username));
            }
            return new ValoracionBlock(final_vals, existMoreVals);
        }
    }
}
