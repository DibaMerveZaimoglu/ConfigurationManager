using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConfigurationManager.Models
{
    public class Submodule
    {
        public Submodule()
        {
            SubmoduleParameters = new HashSet<SubmoduleParameter>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Submodule name is required.")]
        [RegularExpression("[A-Z]{3}", ErrorMessage = "Submodule name must be consist of three uppercase letters.")]
        public string Name { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<SubmoduleParameter> SubmoduleParameters { get; set; }
    }
}
