using Es.Udc.DotNet.ModelUtil.Transactions;
using PracticaMad.Model.ValoracionDAO;
using System;

namespace PracticaMad.Model.ValoracionServiceNS
{
    public interface IValoracionService
    {

        IValoracionDAO ValoracionDao { set; }

        [Transactional]
        Valoracion AddValoracion(long comprador, long product, double score, String comentario = null);

        [Transactional]
        Valoracion FindValoracionByValId(long valId);

        [Transactional]
        ValoracionBlock FindValoracionByVendor(long loginVendor, int startIndex, int count);
    }
}
