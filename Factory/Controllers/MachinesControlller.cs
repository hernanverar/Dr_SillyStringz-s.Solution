using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Factory.Controllers
{
    public class MachinesController : Controller
{
    private readonly FactoryContext _db;
    public MachinesController(FactoryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
        return View(_db.Machines.ToList());
    }

    public ActionResult Details(int id)
    {
      Machine thisMachine = _db.Machines
                                .Include(machine => machine.JoinEntities)
                                .ThenInclude(join => join.Engineer)
                                .FirstOrDefault(machine => machine.MachineId == id);
      return View(thisMachine);
    }
        public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Machine machine)
    {
      if (!ModelState.IsValid)
      {
        return View();
      }
      else
      {
      _db.Machines.Add(machine);
      _db.SaveChanges();
      return RedirectToAction("Index");
      }
    }
      public ActionResult Edit(int id)
    {
      Machine thisMachine = _db.Machines.FirstOrDefault(machines => machines.MachineId == id);
      return View(thisMachine);
    }

    [HttpPost]
    public ActionResult Edit(Machine machine)
    {
      _db.Machines.Update(machine);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

  }  
}  