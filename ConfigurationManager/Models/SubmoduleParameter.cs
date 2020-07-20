using System;
using System.ComponentModel.DataAnnotations;

namespace ConfigurationManager.Models
{
    public class SubmoduleParameter
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Parameter name is required.")]
        [StringLength(50, ErrorMessage = "Parameter name can't be longer than 50 characters.")]
        [RegularExpression(@"^[A-Z]+\S*$", ErrorMessage = "Parameter name should start with upper case and can not have white space.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Parameter description is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Parameter type is required.")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Parameter value is required.")]
        public string Value { get; set; }

        public int SubmoduleId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? IsActive { get; set; }

        public virtual Submodule Submodule { get; set; }
    }
}
