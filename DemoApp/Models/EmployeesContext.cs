﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApp.Models
{
    public class EmployeesContext : DbContext
    {

        public EmployeesContext(DbContextOptions<EmployeesContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
    }
}
