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

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the date when the event will begin.")]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the date when the event will end.")]
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
        
        [Display(Name = "Frequency")]
        public RepeatingFrequency Frequency { get; set; }

        [Display(Name = "Location")]
        public string Location { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        //[DataType(DataType.)]
        //[Display(Name = "color")]
        //public string color { get; set; }

        // Contructors begin here

        public EventViewModel() { }
        
        /// <summary>
        /// Basic constructor for an event
        /// </summary>
        /// <param name="Name">Name of the event</param>
        /// <param name="StartDate">The event's start date</param>
        /// <param name="EndDate">The event's end date</param>
        public EventViewModel(string Name, DateTime StartDate, DateTime EndDate)
        {
            this.Name = Name;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            IsAllDay = false;
            IsRepeating = false;
            Frequency = RepeatingFrequency.None;
            Location = "";
            Description = "";
        }

        /// <summary>
        /// Basic contructor for an all-day event
        /// </summary>
        /// <param name="Name">Name of the event</param>
        /// <param name="StartDate">The event's start date</param>
        /// <param name="EndDate">The event's end date</param>
        /// <param name="IsAllDay">Is the event all day?</param>
        public EventViewModel(string Name, DateTime StartDate, DateTime EndDate, bool IsAllDay)
        {
            this.Name = Name;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.IsAllDay = IsAllDay;
            IsRepeating = false;
            Frequency = RepeatingFrequency.None;
            Location = "";
            Description = "";
        }

        /// <summary>
        /// Basic contructor for a repeating event
        /// </summary>
        /// <param name="Name">Name of the event</param>
        /// <param name="StartDate">The event's start date</param>
        /// <param name="EndDate">The event's end date</param>
        /// <param name="Frequency">The frequency at which the event repeats</param>
        public EventViewModel(string Name, DateTime StartDate, DateTime EndDate, RepeatingFrequency Frequency)
        {
            this.Name = Name;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            IsAllDay = false;
            IsRepeating = true;
            this.Frequency = Frequency;
            Location = "";
            Description = "";
        }

        /// <summary>
        /// Basic constructor for a repeating all-day event
        /// </summary>
        /// <param name="Name">Name of the event</param>
        /// <param name="StartDate">The event's start date</param>
        /// <param name="EndDate">The event's end date</param>
        /// <param name="IsAllDay">Is the event all day?</param>
        /// <param name="Frequency">The frequency at which the event repeats</param>
        public EventViewModel(string Name, DateTime StartDate, DateTime EndDate, bool IsAllDay, RepeatingFrequency Frequency)
        {
            this.Name = Name;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.IsAllDay = IsAllDay;
            IsRepeating = false;
            this.Frequency = Frequency;
            Location = "";
            Description = "";
        }

        /// <summary>
        /// Constructor for an event with a starting and ending time
        /// </summary>
        /// <param name="Name">Name of the event</param>
        /// <param name="StartDate">The event's start date</param>
        /// <param name="EndDate">The event's end date</param>
        /// <param name="StartTime">The event's start time</param>
        /// <param name="EndTime">The event's end time</param>
        public EventViewModel(string Name, DateTime StartDate, DateTime EndDate, DateTime StartTime, DateTime EndTime)
            : this(Name, StartDate, EndDate)
        {
            this.StartTime = StartTime;
            this.EndTime = EndTime;
        }

        /// <summary>
        /// Constructor for an all-day event with a starting and ending time
        /// </summary>
        /// <param name="Name">Name of the event</param>
        /// <param name="StartDate">The event's start date</param>
        /// <param name="EndDate">The event's end date</param>
        /// <param name="StartTime">The event's start time</param>
        /// <param name="EndTime">The event's end time</param>
        /// <param name="IsAllDay">Is the event all day?</param>
        public EventViewModel(string Name, DateTime StartDate, DateTime EndDate, DateTime StartTime, DateTime EndTime, bool IsAllDay)
            : this(Name, StartDate, EndDate, IsAllDay)
        {
            this.StartTime = StartTime;
            this.EndTime = EndTime;
        }

        /// <summary>
        /// Constructor for a repeating event with a starting and ending time
        /// </summary>
        /// <param name="Name">Name of the event</param>
        /// <param name="StartDate">The event's start date</param>
        /// <param name="EndDate">The event's end date</param>
        /// <param name="StartTime">The event's start time</param>
        /// <param name="EndTime">The event's end time</param>
        /// <param name="Frequency">The frequency at which the event repeats</param>
        public EventViewModel(string Name, DateTime StartDate, DateTime EndDate, DateTime StartTime, DateTime EndTime, RepeatingFrequency Frequency)
            : this(Name, StartDate, EndDate, Frequency)
        {
            this.StartTime = StartTime;
            this.EndTime = EndTime;
        }

        /// <summary>
        /// Constructor for a repeating all-day event with a starting and ending time
        /// </summary>
        /// <param name="Name">Name of the event</param>
        /// <param name="StartDate">The event's start date</param>
        /// <param name="EndDate">The event's end date</param>
        /// <param name="StartTime">The event's start time</param>
        /// <param name="EndTime">The event's end time</param>
        /// <param name="Frequency">The frequency at which the event repeats</param>
        public EventViewModel(string Name, DateTime StartDate, DateTime EndDate, DateTime StartTime, DateTime EndTime, bool IsAllDay, RepeatingFrequency Frequency)
            : this(Name, StartDate, EndDate, IsAllDay, Frequency)
        {
            this.StartTime = StartTime;
            this.EndTime = EndTime;
        }
    }
}