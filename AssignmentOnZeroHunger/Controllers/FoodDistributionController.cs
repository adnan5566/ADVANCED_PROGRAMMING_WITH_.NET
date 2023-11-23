////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Web;
////using System.Web.Mvc;
////using System.Data.Entity;
////using AssignmentOnZeroHunger.DB;

////namespace AssignmentOnZeroHunger.Controllers
////{
////    public class FoodDistributionController : Controller
////    {
////        // GET: FoodDistribution
////        public ActionResult Index()
////        {
////            using (var db = new ZeroHunEntities())
////            {
////                var Food = (from f in db.FoodDistributions
////                            from e in db.Employees
////                            where e.ID == f.EmployeeID
////                            select f).ToList();

////                var employees = db.Employees.ToList();
////                var collection = db.CollectRequests.ToList();

////                ViewBag.Employees = employees;
////                ViewBag.Collections = collection;

////                return View(Food);
////            }
////        }

////        [HttpPost]
////        public ActionResult Index(FoodDistribution food, int EmployeeID, int CollectRequestID)
////        {
////            using (var db = new ZeroHunEntities())
////            {
////                food.EmployeeID = EmployeeID;
////                food.CollectRequestID = CollectRequestID;
////                food.RequestType = "Pending";
////                db.FoodDistributions.Add(food);
////                db.SaveChanges();
////                ViewBag.Msg = "Food Distribution Added Successfully";
////                return RedirectToAction("Index");
////            }
////        }

////        public ActionResult Complete(int id)
////        {
////            using (var db = new ZeroHunEntities())
////            {
////                var request = db.CollectRequests.Find(id);
////                var food = (from f in db.FoodDistributions where f.CollectRequestID == id select f).SingleOrDefault();

////                if (food != null)
////                {
////                    food.RequestType = "Complete";
////                    db.Entry(food).State = EntityState.Modified;
////                    db.SaveChanges();
////                    ViewBag.Msg = "Food Distribution marked as Completed successfully!";
////                }
////                else
////                {
////                    ViewBag.Error = "Invalid request!";
////                }

////                if (request != null)
////                {
////                    request.Status = "Complete";
////                    db.Entry(request).State = EntityState.Modified;
////                    db.SaveChanges();
////                    ViewBag.Msg = "Request marked as Completed successfully!";
////                }
////                else
////                {
////                    ViewBag.Error = "Invalid request!";
////                }
////            }

////            return RedirectToAction("Index");
////        }
////    }
////}


//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using AssignmentOnZeroHunger.DB;

//namespace AssignmentOnZeroHunger.Controllers
//{
//    public class FoodDistributionController : Controller
//    {
//        public ActionResult Index()
//        {
//            using (var db = new ZeroHunEntities())
//            {
//                var food = (from f in db.FoodDistributions
//                            from e in db.Employees
//                            where e.ID == f.EmployeeID
//                            select f).ToList();

//                var employees = db.Employees.ToList();
//                var collections = db.CollectRequests
//                    .Where(c => c.Status == "Accept" && c.EmployeeId != null)
//                    .ToList();

//                ViewBag.Employees = employees;
//                ViewBag.Collections = collections;

//                return View(food);
//            }
//        }

//        [HttpPost]
//        public ActionResult Index(FoodDistribution food, int EmployeeID, int CollectRequestID)
//        {
//            using (var db = new ZeroHunEntities())
//            {
//                food.EmployeeID = EmployeeID;
//                food.CollectRequestID = CollectRequestID;
//                food.RequestType = "Pending";
//                db.FoodDistributions.Add(food);
//                db.SaveChanges();
//                TempData["msg"] = "Food Distribution Added Successfully";
//                return RedirectToAction("Index");
//            }
//        }

//        public ActionResult Complete(int id)
//        {
//            using (var db = new ZeroHunEntities())
//            {
//                var request = db.CollectRequests.Find(id);
//                var food = db.FoodDistributions.SingleOrDefault(f => f.CollectRequestID == id);

//                if (food != null)
//                {
//                    food.RequestType = "Complete";
//                    db.Entry(food).State = EntityState.Modified;
//                    db.SaveChanges();
//                    TempData["msg"] = "Food Distribution marked as Completed successfully!";
//                }
//                else
//                {
//                    TempData["error"] = "Invalid food distribution request!";
//                }

//                if (request != null)
//                {
//                    request.Status = "Complete";
//                    db.Entry(request).State = EntityState.Modified;
//                    db.SaveChanges();
//                    TempData["msg"] = "Request marked as Completed successfully!";
//                }
//                else
//                {
//                    TempData["error"] = "Invalid collect request!";
//                }
//            }

//            return RedirectToAction("Index");
//        }
//    }
//}



using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using AssignmentOnZeroHunger.DB;

namespace AssignmentOnZeroHunger.Controllers
{
    public class FoodDistributionController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new ZeroHunEntities())
            {
                var food = (from f in db.FoodDistributions
                            from e in db.Employees
                            where e.ID == f.EmployeeID
                            select f).ToList();

                var employees = db.Employees.ToList();
                var collections = db.CollectRequests
                    .Where(c => c.Status == "Accept" && c.EmployeeId != null)
                    .ToList();

                ViewBag.Employees = employees;
                ViewBag.Collections = collections.Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.Restaurant.Name
                }).ToList();

                return View(food);
            }
        }

        [HttpPost]
        public ActionResult Index(FoodDistribution food, int EmployeeID, int CollectRequestID)
        {
            using (var db = new ZeroHunEntities())
            {
                food.EmployeeID = EmployeeID;
                food.CollectRequestID = CollectRequestID;
                food.RequestType = "Pending";
                db.FoodDistributions.Add(food);
                db.SaveChanges();
                TempData["msg"] = "Food Distribution Added Successfully";
                return RedirectToAction("Index");
            }
        }

        public ActionResult Complete(int id)
        {
            using (var db = new ZeroHunEntities())
            {
                var request = db.CollectRequests.Find(id);
                var food = db.FoodDistributions.SingleOrDefault(f => f.CollectRequestID == id);

                if (food != null)
                {
                    food.RequestType = "Complete";
                    db.Entry(food).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["msg"] = "Completed successfully!";
                }
                else
                {
                    TempData["error"] = "Invalid food distribution request!";
                }

                if (request != null)
                {
                    request.Status = "Complete";
                    db.Entry(request).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["msg"] = "Successful!";
                }
                else
                {
                    TempData["error"] = "Invalid collect request!";
                }
            }

            return RedirectToAction("Index");
        }
    }
}

