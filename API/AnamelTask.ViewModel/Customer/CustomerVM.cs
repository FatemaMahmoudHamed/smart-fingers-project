using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnamelTask.ViewModel.Customer
{
    public class CustomerVM
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public DateTime Birthdate { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }

    }
}
