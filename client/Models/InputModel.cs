using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace client
{
    public class InputModel
    {
        [Required]
        [Phone(ErrorMessage ="Please enter your phone number in a proper format")]
        public string PhoneNumber { get; set; }
    }
}
