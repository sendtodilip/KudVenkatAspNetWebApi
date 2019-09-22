using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    [RoutePrefix("api/students")]
    public class StudentsController : ApiController
    {
        static List<Student> students = new List<Student>()
        {
            new Student(){Id=1, Name="Tome"},
            new Student(){Id=2, Name="Sam"},
            new Student(){Id=3, Name="John"}
        };

        [Route("~/api/teachers")]
        public IEnumerable<Teacher> GetTeachers()
        {
            List<Teacher> teachers = new List<Teacher>()
            {
                new Teacher(){Id=1, Name="Rob"},
                new Teacher(){Id=2, Name="Mike"},
                new Teacher(){Id=3, Name="Mary"}
            };
            return teachers;
        }

        [Route("")]
        public IEnumerable<Student> Get()
        {
            return students;
        }

        //[Route("{id:int:min(1)}:max(3)")]
        [Route("{id:int:range(1, 3)}", Name = "GetStudentById")]
        public Student Get(int id)
        {
            return students.First(s => s.Id == id);
        }

        public HttpResponseMessage Post(Student student)
        {
            students.Add(student);
            var response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location = new Uri(Url.Link("GetStudentById", new { id = student.Id }));
            return response;
        }

        [Route("{name:alpha}")]
        public Student Get(string name)
        {
            return students.First(s => s.Name == name);
        }

        [Route("{id}/courses")]
        public IEnumerable<string> GetStudentCouses(int id)
        {
            if (id == 1)
                return new List<string>() { "C#", "ASP.NET", "SQL Server" };
            else if (id == 2)
                return new List<string>() { "ASP.NET Web API", "C#", "SQL Server" };
            else
                return new List<string>() { "Bootstrap", "jQuery", "AngularJs" };
        }
    }
}
