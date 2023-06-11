using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WorkFlowXAPI.Areas.Proyects
{
    public class ProyectController : ApiController
    {
        WorkFlowXEntities Entities = new WorkFlowXEntities();
        public IEnumerable<Proyectos> GetAll()
        {
            List<Proyectos> proyectos = Entities.Proyectos.ToList();
            return proyectos;
        }

        public Proyectos GetById(int id)
        {
            Proyectos proyecto = Entities.Proyectos.FirstOrDefault(x => x.ID == id);
            return proyecto;
        }

        public IEnumerable<Proyectos> GetByClienteId(int clienteId)
        {
            List<Proyectos> proyectos = Entities.Proyectos.Where(x => x.Cliente_ID == clienteId).ToList();
            return proyectos;
        }
    }
}
