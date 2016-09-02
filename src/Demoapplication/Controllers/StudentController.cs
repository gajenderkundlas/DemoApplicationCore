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
            total = total == 0 ? 1000 : total;
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
            
            if (dataType == "json")
                return Json(studentList);
            else
                return View(studentList);
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
        public void SaveUpdate(StudentInformation objStudent) {
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
            RedirectToAction("Index",new {dataType="View"});
        }
    }
}
