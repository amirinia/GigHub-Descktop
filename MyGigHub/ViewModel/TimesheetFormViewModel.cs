using MyGigHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyGigHub.ViewModel
{
    public class TimesheetFormViewModel
    {
        [Required]
        public string IP { get; set; }

        [Required]
        public DateTime StartTime { get; set; }


        public DateTime EndTime { get; set; }

        public TimeSpan GetDifferenceTimes()
        {
            return StartTime - EndTime;
        }
    }
}