using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace pavlovLab.Models
{
    public class LabData
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public byte GroupIndex { get; set; }
        public string GroupPrefix { get; set; }
        
        public BaseModelValidationResult Validate()
        {
            var validationResult = new BaseModelValidationResult();

            if (string.IsNullOrWhiteSpace(Name)) validationResult.Append($"Name cannot be empty");
            if (string.IsNullOrWhiteSpace(Surname)) validationResult.Append($"Surname cannot be empty");
            if (!(0 < GroupIndex && GroupIndex < 100)) validationResult.Append($"GroupIndex {GroupIndex} is out of range (0..100)");

            if (!string.IsNullOrEmpty(Name) && !char.IsUpper(Name.FirstOrDefault())) validationResult.Append($"Name {Name} should start from capital letter");
            if (!string.IsNullOrEmpty(Surname) && !char.IsUpper(Surname.FirstOrDefault())) validationResult.Append($"Surname {Surname} should start from capital letter");

            return validationResult;
        }

        public override string ToString()
        {
            return $"{Name} {Surname} from {GroupPrefix}-{GroupIndex}";
        }
    }
}