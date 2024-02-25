﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace login_and_register.Models
{
    public class Assignment
    {
        public int Id { get; set; }

        public int CourseId { get; set; }
        public string Tittle { get; set; }
        public string Describtion { get; set; }
        public DateTime EndDate { get; set; }
        public Course Course { get; set; }
    }
}
