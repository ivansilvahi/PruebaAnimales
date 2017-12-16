using PruebaAnimales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Autos.Controllers
{

    public class HomeController : Controller
    {
        ANIMALESEntities cnx;
       

        public HomeController()
        {
            cnx = new ANIMALESEntities();
        }
        public ActionResult Formulario()
        {
            return View();
        }
        public ActionResult Guardar(string nombre, string sexo, string raza, string edad, string tipo, string rebaño)
        {

            PruebaAnimales.Models.Vaca vaca = new PruebaAnimales.Models.Vaca
            {
                Nombre = nombre,
                Sexo = sexo,
                Raza = raza,
                Edad = edad,
                Tipo = tipo,
                Rebaño = rebaño
                
            };

            cnx.Vaca.Add(vaca);
            cnx.SaveChanges();

            return View("Listado", ListadoVacas());
        }
        public ActionResult Listado()
        {

            return View("Listado", ListadoVacas());
        }
        public ActionResult Ficha(string nombre)
        {
    
            return View( BuscarVaca(nombre));
        }

        private Vaca BuscarVaca(string nombre)
        {
            Vaca nueva = new Vaca();
            foreach (Vaca vaca in cnx.Vaca.ToList())
            {
                if ( vaca.Nombre.Equals(nombre))
                {
                    nueva = vaca;
                }
            }
            return nueva;
        }
        public ActionResult Visualizar(string nombre)
        {
            Vaca nueva = BuscarVaca(nombre);

            if (nueva != null)
            {
                return View("Ficha", nueva);
            }
            return View("Listado", cnx.Vaca.ToList());
        }


        private List<PruebaAnimales.Models.Vaca> ListadoVacas()
        {

            cnx.Database.Connection.Open();


            List<PruebaAnimales.Models.Vaca> auto = cnx.Vaca.ToList();

            cnx.Database.Connection.Close();

            return auto;
        }

    }
}