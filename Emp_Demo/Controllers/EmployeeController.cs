using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Emp_Demo.Models;
using Emp_Demo.Enums;
using System.Data.Entity;


namespace Emp_Demo.Controllers
{

    public class EmployeeController : BaseController
    {
    
        Demo_EmployeeManagementEntities DbContext = new Demo_EmployeeManagementEntities();

        [HttpGet]
        public ActionResult EmployeeDashboard()
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"] as string;
            }
            else
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;
            }

            int userId = GetLoggedInUserId();
            var employee = DbContext.Employeeinfoes.FirstOrDefault(e => e.Userlogin.UserId == userId);
            if (employee != null)
            {
                var attendances = DbContext.Attendances.Where(a => a.EmployeeId == employee.EmployeeId).ToList();
                var lastAttendance = attendances.OrderByDescending(a => a.AttendanceDate).FirstOrDefault();
                ViewBag.EmployeeName = employee.EmployeeName;

                //Attendances = attendances
                var model = new AttendanceViewModel
                {
                    //EmployeeId = employee.EmployeeId,
                    ////AttendanceDate = DateTime.Today,
                    ////Timestamp = DateTime.Now,

                    Attendances = attendances

                };


                return View(model);
            }
            else
            {
                return HttpNotFound();
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PunchInOut(AttendanceViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    int userId = GetLoggedInUserId();
                    var employee = DbContext.Employeeinfoes.FirstOrDefault(e => e.Userlogin.UserId == userId);

                    if (employee != null)
                    {

                        var todayPunches = DbContext.Attendances
                            .Where(a => a.EmployeeId == employee.EmployeeId && DbFunctions.TruncateTime(a.AttendanceDate) == DbFunctions.TruncateTime(DateTime.Today))
                            .OrderBy(a => a.Timestamp)
                            .ToList();


                        var lastPunch = todayPunches.LastOrDefault();

                        if (model.EntryType == EntryTypeEnum.PunchIn)
                        {

                            bool hasPunchInToday = todayPunches.Any(a => a.EntryType == EntryTypeEnum.PunchIn.ToString());

                            if (!hasPunchInToday)
                            {

                                var punchIn = new Attendance
                                {
                                    EmployeeId = employee.EmployeeId,
                                    AttendanceDate = DateTime.Today,
                                    EntryType = EntryTypeEnum.PunchIn.ToString(),
                                    Timestamp = DateTime.Now
                                };
                                DbContext.Attendances.Add(punchIn);
                                DbContext.SaveChanges();
                                TempData["SuccessMessage"] = "Punched in successfully.";
                            }
                            else
                            {
                                TempData["ErrorMessage"] = "You have already punched in today.";
                            }
                        }
                        else if (model.EntryType == EntryTypeEnum.PunchOut)
                        {
                            bool hasPunchOutToday = todayPunches.Any(a => a.EntryType == EntryTypeEnum.PunchOut.ToString());

                            if (lastPunch != null && lastPunch.EntryType == EntryTypeEnum.PunchIn.ToString() && !hasPunchOutToday)
                            {

                                var punchOut = new Attendance
                                {
                                    EmployeeId = employee.EmployeeId,
                                    AttendanceDate = DateTime.Today,
                                    EntryType = EntryTypeEnum.PunchOut.ToString(),
                                    Timestamp = DateTime.Now
                                };
                                DbContext.Attendances.Add(punchOut);
                                DbContext.SaveChanges();
                                TempData["SuccessMessage"] = "Punched out successfully.";
                            }
                            else if (lastPunch == null || lastPunch.EntryType == EntryTypeEnum.PunchOut.ToString())
                            {
                                TempData["ErrorMessage"] = "You haven't punched in today or you have already punched out.";
                            }
                        }

                        return RedirectToAction("EmployeeDashboard");
                    }

                    TempData["ErrorMessage"] = "User not found.";
                }
                catch (Exception)
                {
                    TempData["ErrorMessage"] = "An error occurred while processing your request.";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Invalid model state.";
            }

            return RedirectToAction("EmployeeDashboard");

        }


        [HttpGet]

        public ActionResult AttendanceReport( )
        {
            int userId = GetLoggedInUserId();
            var employee = DbContext.Employeeinfoes.FirstOrDefault(e => e.Userlogin.UserId == userId);
            var attendances = DbContext.Attendances.Where(a => a.EmployeeId == employee.EmployeeId).ToList();
            var lastAttendance = attendances.OrderByDescending(a => a.Timestamp).FirstOrDefault();
            ViewBag.EmployeeName = employee.EmployeeName;
            return View(attendances);
            //int userId = GetLoggedInUserId();
           
            //var attendances = DbContext.Attendances.Where(a => a.EmployeeId == ).ToList();
            //return View(attendances);
        }


    }
}
