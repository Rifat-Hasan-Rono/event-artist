using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Event_Management.Models;

namespace Event_Management.Context
{
    public class EventManagementDbContaxt:DbContext
    {
	    public DbSet<Hall> Halls { get; set; }
	    public DbSet<Book> Books { get; set; }
    }
}