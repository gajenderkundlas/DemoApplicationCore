using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Demoapplication.Model;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Demoapplication.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentDataBaseContext _dbContext;
        public StudentController(StudentDataBaseContext context) {
            _dbContext = context;
        }
        // GET: /<controller>/
        public IActionResult Index(string dataType, string searchStudent,int pagesize,int total,string orderby)
        {
           
            dataType = dataType ?? "View";
            searchStudent = searchStudent ?? "";
             pagesize = pagesize==0 ? 1:pagesize;
            total = total == 0 ? 5 : total;
            List<StudentInformation> studentList;
            if (orderby == "desc")
            {
                studentList = _dbContext.StudentInformation.Where(o => o.Name.Contains(searchStudent))
                                                                .OrderByDescending(x => x.Marks)
                                                                .ToList();
            }
            else {
               studentList = _dbContext.StudentInformation.Where(o => o.Name.Contains(searchStudent))
                                                .OrderBy(x => x.Marks)
                                                .ToList();
            }
            ViewBag.hdnTotalRecord = studentList.Count();
            ViewBag.PageSize = pagesize;
            ViewBag.TotalPage = Math.Ceiling(Convert.ToDecimal(studentList.Count()) / total);
            ViewBag.SearchStudent = searchStudent;
            studentList = studentList.Skip(total * (pagesize-1)).Take(total).ToList();
            if (dataType == "json")
                return Json(studentList);
            else
                return View(studentList);
        }
        public int Delete(int ID) {
            int SuccessResult = 0;
            try {
                StudentInformation objStudent = _dbContext.StudentInformation.Where(o => o.ID == ID).SingleOrDefault();
                _dbContext.Entry(objStudent).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                _dbContext.SaveChanges();
                SuccessResult = 1;
            } catch (Exception) {
                SuccessResult = 0;

            }
            return SuccessResult;
        }
        public IActionResult Add(int id) {
            StudentInformation student=new StudentInformation();
            if (id != 0)
            {
                student = _dbContext.StudentInformation.Where(o => o.ID == id).SingleOrDefault();
            }
            else {
                student.ID = 0;
                student.Name = "";
                student.Email = "";
                student.FatherName = "";
                student.ContactNo = "";
            }
            return View(student);
        }
        [HttpPost]
        public ActionResult SaveUpdate(StudentInformation objStudent) {
                if (ModelState.IsValid)
                {

                    if (objStudent.ID == 0)
                    {
                        _dbContext.Add(objStudent);
                    }
                    else {
                        StudentInformation objStudentToUpdate = _dbContext.StudentInformation.Where(o=>o.ID==objStudent.ID).SingleOrDefault();
                        if (objStudentToUpdate != null)
                        {
                            //objStudentToUpdate= JsonConvert.DeserializeObject<StudentInformation>(JsonConvert.SerializeObject(objStudent));
                            objStudentToUpdate.Name = objStudent.Name;
                            objStudentToUpdate.FatherName = objStudent.FatherName;
                            objStudentToUpdate.ContactNo = objStudent.ContactNo;
                            objStudentToUpdate.Email = objStudent.Email;
                            objStudentToUpdate.Marks = objStudent.Marks;
                           _dbContext.Entry(objStudentToUpdate).State = Microsoft.EntityFrameworkCore.EntityState.Modified;    
                        }
                    }
                }
                _dbContext.SaveChanges();
            return RedirectToAction("Index", new {dataType="View"}); 
        }
    }
}
