using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SipayApi.Controllers;

public class Person2
{
    /*
    [DisplayName("Staff person name")]
    [Required]
    [StringLength(maximumLength: 100, MinimumLength = 5)]
    public string Name { get; set; }


    [DisplayName("Staff person lastname")]
    [Required]
    [StringLength(maximumLength: 100, MinimumLength = 5)]
    public string Lastname { get; set; }


    [DisplayName("Staff person phone number")]
    [Required]
    [Phone]
    public string Phone { get; set; }


    [DisplayName("Staff person access level to system")]
    [Range(minimum: 1, maximum: 5)]
    [Required]
    public int AccessLevel { get; set; }



    [DisplayName("Staff person salary")]
    [Required]
    [Range(minimum: 5000, maximum: 50000)]
    [SalaryAttribute]
    public decimal Salary { get; set; }
  

}

*/


    //İlk Validator’un Oluşturulması:
    // Person Class: validasyon örneklendirmesi yapacağımız sınıfımızı tasarladık.
    public class Person
    {

        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public int AccessLevel { get; set; }
        public decimal Salary { get; set; }
    }

    //Person sınıfına ait kuralları tanımlamak için
    //bir sınıf oluşturuyoruz ve gerekli miras alma işlemlerini yapıyoruz.
    //RuleFor() metotu çağrılarak lambda expression ile hangi property üzerinden kural yazılacağı sağlanır.
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty() // Name property’sinin boş olmamalı
                .MinimumLength(5) //minimum 5 karakter olmalı
                .MaximumLength(100); // maximum 100 karakter olmalı
                                     //.Length(5, 100);

            RuleFor(p => p.Lastname)
                .NotEmpty()
                //.MinimumLength(5) 
                //.MaximumLength(100);
                .Length(5, 100);

            RuleFor(p => p.Phone)
                .NotEmpty()
                .Matches(@"^\d{11}$");

            RuleFor(p => p.AccessLevel)
                .InclusiveBetween(1, 5); //Özellik değerinin belirtilen iki sayı (dahil) arasında olup olmadığını kontrol eder.


            RuleFor(p => p.Salary)
                .NotEmpty()
                .InclusiveBetween(5000, 50000);


        }
    }

    public class SalaryAttribute : ValidationAttribute
{
    public SalaryAttribute()
    {

    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var person = (Person)validationContext.ObjectInstance;
        decimal salary = (decimal)value;
        ValidationResult? message = ValidationResult.Success;
        switch (person.AccessLevel)
        {
            case 1:
                message = salary > 10000 ? new ValidationResult("Salary cannot be greater then 10000") : ValidationResult.Success;
                return message;
            case 2:
                message = salary > 20000 ? new ValidationResult("Salary cannot be greater then 20000") : ValidationResult.Success;
                return message;
            case 3:
                message = salary > 30000 ? new ValidationResult("Salary cannot be greater then 30000") : ValidationResult.Success;
                return message;
            case 4:
                message = salary > 40000 ? new ValidationResult("Salary cannot be greater then 40000") : ValidationResult.Success;
                return message;
            default:
                message = new ValidationResult("Salary cannot invalid");
                break;
        }
        return message;
    }
}

[ApiController]
[Route("sipy/api/[controller]")]
public class Person2Controller : ControllerBase
{

    public Person2Controller() { }


    [HttpPost]
    public Person Post([FromBody] Person person)
    {
        return person;
    }
}