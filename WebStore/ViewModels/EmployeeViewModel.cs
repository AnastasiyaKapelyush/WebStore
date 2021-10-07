using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels
{
    public class EmployeeViewModel: IValidatableObject
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Имя не указано")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Длина от 2 до 200 символов")]
        [RegularExpression(@"([А-ЯЁ][а-яё]+)|([A-Z][a-z]+)", ErrorMessage = "Введены недопустимые символы")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Фамилия не указана")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Длина от 2 до 200 символов")]
        [RegularExpression(@"([А-ЯЁ][а-яё]+)|([A-Z][a-z]+)", ErrorMessage = "Введены недопустимые символы")]
        public string LastName { get; set; }

        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }

        [Display(Name = "Возраст")]
        [Range(18, 80, ErrorMessage = "Возраст должен быть от 18 до 80 лет")]
        public int Age { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext context)
        {
            switch (context.MemberName)
            {
                case nameof(Age):
                    if (Age < 15 || Age > 90)
                        return new[] { new ValidationResult("Недопустимый возраст", new[] { nameof(Age) }) };
                    return new[] { ValidationResult.Success };
                default: return new[] { ValidationResult.Success };
            }
        }
    }
}
