using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Event_Management.Models
{
    public class RoleViewModel
    {
        public string Name { get; set; }
        public string Id { get; set; }

        public RoleViewModel() { }

	    public RoleViewModel(ApplicationRole role)
	    {
		    Id = role.Id;
		    Name = role.Name;
	    }


    }
}