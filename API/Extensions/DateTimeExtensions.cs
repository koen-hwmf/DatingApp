using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge(this DateTime dob)  
        {
            // get current date
            var today = DateTime.Today;
            // calculate age by subtracting birth year from current year
            var age = today.Year - dob.Year;
            // check if birthday is yet to come this year or not and adjust age accordingly
            if (dob.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}
