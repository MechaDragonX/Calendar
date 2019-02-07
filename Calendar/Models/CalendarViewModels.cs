using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Calendar.Models
{
    public class EventViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [Display(Name = "End Time")]
        public DateTime EndTime { get; set; }

        [Required]
        [Display(Name = "All Day")]
        public bool IsAllDay { get; set; }

        [Required]
        [Display(Name = "Repeating")]
        public bool IsRepeating { get; set; }

        [Display(Name = "Location")]
        public string Location { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}