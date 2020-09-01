using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnamelTask.Data.DbModels
{
    public class Customer
    {

        [Required]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string FullName { get; set; }

        [Required]
        public int Age { get; set; }


        [Required]
        public DateTime Birthdate { get; set; }

        [Required]
        [StringLength(50)]
        public string Gender { get; set; }

    }
}
