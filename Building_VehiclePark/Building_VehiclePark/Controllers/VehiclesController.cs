using Building_VehiclePark.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Building_VehiclePark.Controllers
{
    public class VehiclesController : Controller
    {
        // GET: Vehicles
        public ActionResult AllVihecles()
        {
            return View(DAL.GetVehicles());
        }
        

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Vehicle model)
        {
            if (ModelState.IsValid & IsValidVehicles(model))
            {
                if(DAL.Add(model))
                    ViewBag.Message = "Добавлено";
                else
                    ViewBag.Message = "Введите корректные данные";
            }
            else
            {
                ViewBag.Message = "Введите корректные данные";
            }
            return View(model);
        }

        public ActionResult Edit(Vehicle model)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Vehicle model, string save)
        {
            if (ModelState.IsValid & IsValidVehicles(model))
            {
                if(DAL.Edit(model))
                    ViewBag.Message = "Изменено";
                else
                    ViewBag.Message = "Не Изменено";
            }
            else
            {
                ViewBag.Message = "Не Изменено";
            }
            return View(model);
        }

        public ActionResult Details(Vehicle model)
        {
            return View(model);
        }
        
        public ActionResult Delete(Vehicle model)
        {
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Vehicle model, string del)
        {
            if (DAL.Delete(model.VehicleId))
            {
                return RedirectToAction("AllVihecles");
            }
            ViewBag.Message = "Не удалено";
            return View(model);
        }

        private bool IsValidVehicles(Vehicle vehicle)
        {
            if(vehicle.InGarage & !vehicle.InRepair & !vehicle.IsWork ||
                !vehicle.InGarage & vehicle.InRepair & !vehicle.IsWork ||
                !vehicle.InGarage & !vehicle.InRepair & vehicle.IsWork)
            {
                return true;
            }
            return false;
        }

        [HttpGet]
        public ActionResult Autorize()
        {            
            return View();
        }

        [HttpPost]
        public ActionResult Autorize(string login, string password)
        {
            if(login == "Moderator" & password == "gfhjkm")
            {
                HttpCookie cookie = new HttpCookie("Role", "Moder");
                cookie.Expires = DateTime.Now.AddMinutes(10);

                Response.Cookies.Add(cookie);

                return RedirectToAction("AllVihecles");
            }

            ViewBag.Message = "Not correct Login or Password";

            return View();
        }
    }
}