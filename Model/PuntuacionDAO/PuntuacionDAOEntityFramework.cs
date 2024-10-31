using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaMad.Model.PuntuacionDAO
{
    public class PuntuacionDAOEntityFramework : GenericDaoEntityFramework<Puntuacion, Int64>, IPuntuacionDAO
    {
    }
}
