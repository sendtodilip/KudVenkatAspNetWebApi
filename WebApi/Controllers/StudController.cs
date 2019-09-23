using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class StudController : ApiController
    {
        static List<Student> students = new List<Student>()
        {
            new Student(){Id=1, Name="Tome"},
            new Student{Id=2, Name="Sam"},
            new Student{Id=3, Name="John"}
        };

        //public HttpResponseMessage Get()
        //{
        //    return Request.CreateResponse(HttpStatusCode.OK, students);
        //}

        //public HttpResponseMessage Get(int id)
        //{
        //    var student = students.FirstOrDefault(s => s.Id == id);
        //    if (student==null)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Student not found.");
        //    }
        //    return Request.CreateResponse(HttpStatusCode.OK, student);
        //}

        public IHttpActionResult Get()
        {
            return Ok(students);
        }

        public IHttpActionResult Get(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                //return NotFound();
                return Content(HttpStatusCode.NotFound, "Student Not found");
            }
            return Ok(student);
        }
    }
}
