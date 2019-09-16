using EmployeeDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeService
{
    public class EmployeeSecurity
    {
        public static bool login(string username, string password)
        {
            using (EmployeeDBEntities entities=new EmployeeDBEntities() )
            {
                return entities.Users.Any(user => user.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && user.Password == password);
            }
        }
    }
}