using System.ComponentModel.DataAnnotations;

namespace Calendar.Models
{
    public enum RepeatingFrequency
    {
        [Display(Name = " ")]
        None,
        Daily,
        Weekly,
        Monthly,
        Yearly,
    }
}