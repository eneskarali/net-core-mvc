﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagementSystem.Models
{
    public class JoinCourseViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}
