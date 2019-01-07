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
        [HttpGet]
        public ActionResult AllVihecles()
        {
            return View(DataAccesLayer.GetVehicles());
        }

        [HttpPost]
        public ActionResult AllVihecles(string filter, string filtType, string isInGarage, string isInRepair, string isIsWork)
        {
            IEnumerable<Vehicle> vehicles = DataAccesLayer.GetVehicles();
            bool[] state = new bool[3];

            if (filtType == "Filt by TypeId")
            {
                vehicles = vehicles.Where(x => x.TypeId.ToLower().Contains(filter.ToLower()));
            }
            else if (filtType == "Filt by Name")
            {
                vehicles = vehicles.Where(x => x.Name.ToLower().Contains(filter.ToLower()));
            }

            if (isInGarage != "" & isInGarage != null)
                state[0] = true;
            else
                state[0] = false;

            if (isInRepair != "" & isInRepair != null)
                state[1] = true;
            else
                state[1] = false;

            if (isIsWork != "" & isIsWork != null)
                state[2] = true;
            else
                state[2] = false;

            vehicles = vehicles.Where(x => x.InGarage & state[0] || x.InRepair & state[1] || x.IsWork & state[2]);

            return View(vehicles);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Vehicle model)
        {
            bool isHiden = true;
            if (Request.Cookies["Role"] != null)
            {
                if (Request.Cookies["Role"].Value == "Moder")
                {
                    isHiden = false;
                }
                else
                {
                    isHiden = true;
                    ViewBag.Message = "У вас недостаточно прав";
                }
            }

            if (!isHiden)
            {
                if (ModelState.IsValid & IsValidVehicles(model))
                {
                    if (DataAccesLayer.Add(model))
                        ViewBag.Message = "Добавлено";
                    else
                        ViewBag.Message = "Введите корректные данные";
                }
                else
                {
                    ViewBag.Message = "Введите корректные данные";
                }
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
            bool isHiden = true;
            if (Request.Cookies["Role"] != null)
            {
                if (Request.Cookies["Role"].Value == "Moder")
                {
                    isHiden = false;
                }
                else
                {
                    isHiden = true;
                    ViewBag.Message = "У вас недостаточно прав";
                }
            }
            if (!isHiden)
            {
                if (ModelState.IsValid & IsValidVehicles(model))
                {
                    if (DataAccesLayer.Edit(model))
                        ViewBag.Message = "Изменено";
                    else
                        ViewBag.Message = "Не Изменено";
                }
                else
                {
                    ViewBag.Message = "Не Изменено";
                }
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
            bool isHiden = true;
            if (Request.Cookies["Role"] != null)
            {
                if (Request.Cookies["Role"].Value == "Moder")
                {
                    isHiden = false;
                }
                else
                {
                    isHiden = true;
                    ViewBag.Message = "У вас недостаточно прав";
                }
            }
            if (!isHiden)
            {
                if (DataAccesLayer.Delete(model.VehicleId))
                {
                    return RedirectToAction("AllVihecles");
                }
                ViewBag.Message = "Не удалено";
            }
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
            else if(DataAccesLayer.LogIn(login, password))
            {
                HttpCookie cookie = new HttpCookie("Role", "Moder");
                cookie.Expires = DateTime.Now.AddMinutes(10);

                Response.Cookies.Add(cookie);

                return RedirectToAction("AllVihecles");
            }

            ViewBag.Message = "Не верный логин или пароль";

            return View();
        }
        [HttpGet]
        public ActionResult AddModer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddModer(User user)
        {
            bool isHiden = true;
            if (Request.Cookies["Role"] != null)
            {
                if (Request.Cookies["Role"].Value == "Moder")
                {
                    isHiden = false;
                }
                else
                {
                    isHiden = true;
                    ViewBag.Message = "У вас недостаточно прав";
                }
            }

            if (!isHiden)
            {
                if (ModelState.IsValid)
                {
                    if (DataAccesLayer.AddUser(user))
                        ViewBag.Message = "Добавлено";
                    else
                        ViewBag.Message = "Введите корректные данные";
                }
                else
                {
                    ViewBag.Message = "Введите корректные данные";
                }
            }
            return View(user);
        }

    }
}