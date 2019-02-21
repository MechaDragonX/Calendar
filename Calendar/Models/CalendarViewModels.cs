using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Calendar.Models
{
    public class EventViewModel
    {
        public Guid Id { get; set;}

        public string Creator { get; set; }

        public DateTime CreationDateTime { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a name for the event.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the date when the will begin.")]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the date when the will end.")]
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [JsonIgnore]
        [DataType(DataType.Time)]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }
        
        [JsonIgnore]
        [DataType(DataType.Time)]
        [Display(Name = "End Time")]
        public DateTime EndTime { get; set; }

        [Display(Name = "All Day")]
        public bool IsAllDay { get; set; }

        [Display(Name = "Repeating")]
        public bool IsRepeating { get; set; }

        [Display(Name = "Location")]
        public string Location { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        //[DataType(DataType.)]
        //[Display(Name = "color")]
        //public string color { get; set; }
    }
}