﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyProject.Models
{
    public class AssignTask
    {
        [Key]
        public int AssingnTaskId { get; set; }
        public int EmployeeId { get; set; }
        public int ClientId { get; set; }
        public int ProjectId { get; set; }
        public string Task { get; set; }
        public string Note { get; set; }

        public virtual Employee Employee { get; set;}
        public virtual Client Client { get; set; }
        public virtual Project  Project { get; set; }


    }
}