﻿namespace CrudApi.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DepartmentID { get; set; }

        public Department Department { get; set; }
    }
}
