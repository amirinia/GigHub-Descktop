using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyGigHub.Models
{
    public class Timesheet
    {
        public int Id { get; set; }

        public ApplicationUser Artist { get; set; }
        [Required]
        public string ArtistId { get; set; }

        public DateTime StartDay { get; set; }

        public DateTime EndDay { get; set; }

        public TimeSpan GetDifferenceTimes()
        {
            return StartDay - EndDay;
        }
    }
}