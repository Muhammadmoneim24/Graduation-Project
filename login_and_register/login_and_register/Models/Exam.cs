﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace login_and_register.Models
{
    public class Exam
    {
        public int Id { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public int SubmissionId { get; set; }
        public string? Tittle { get; set; }

        public string? Describtion { get; set; }

        public string? Instructions { get; set; }
        public string? Time { get; set; }

        public int Grades { get; set; }
        public DateTime EndDate { get; set; }
        public Course Course { get; set; }

        public Submission Submission { get; set; }

        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}
