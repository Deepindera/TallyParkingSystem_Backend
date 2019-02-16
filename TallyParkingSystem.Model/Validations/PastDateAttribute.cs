using System;
using System.ComponentModel.DataAnnotations;

namespace TallyParkingSystem.Model
{
    public class PastDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var datetime = Convert.ToDateTime(value);
            return datetime.ToLocalTime() < DateTime.Now; 
        }
    }
}
