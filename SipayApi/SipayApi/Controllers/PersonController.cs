using Microsoft.AspNetCore.Mvc;

// FluentValidation paketini eklendi.
//View->Other Windows -> Package MAnager Console : Install-Package FluentValidation

using FluentValidation;
using FluentValidation.Results;


namespace SipayApi.Controllers;

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

//Bu sınıfa bir Validator tanımlandı.
public class SalaryValidator : AbstractValidator<Person>
{
    public SalaryValidator()
    {
        RuleFor(p => p.Salary).Custom((salary, context) =>
        {
            var person = context.InstanceToValidate as Person;

            switch (person.AccessLevel)
            {
                case 1:
                    if (salary > 10000)
                    {
                        context.AddFailure("Salary cannot be greater than 10000 for access level 1");
                    }
                    break;
                case 2:
                    if (salary > 20000)
                    {
                        context.AddFailure("Salary cannot be greater than 20000 for access level 2");
                    }
                    break;
                case 3:
                    if (salary > 30000)
                    {
                        context.AddFailure("Salary cannot be greater than 30000 for access level 3");
                    }
                    break;
                case 4:
                    if (salary > 40000)
                    {
                        context.AddFailure("Salary cannot be greater than 40000 for access level 4");
                    }
                    break;
                default:
                    context.AddFailure("Invalid access level");
                    break;
            }
        });
    }
}



[Route("sipy/api/[controller]")]
[ApiController]

public class PersonController : ControllerBase
{

    public PersonController() { }


    [HttpPost]
    public IActionResult Post ([FromBody] Person person)
    {
        var validator = new SalaryValidator();
        ValidationResult result = validator.Validate(person);

        if (!result.IsValid) //doğrulama sonucunun geçerliliğiini kontrol ediyorum.
        {
            return BadRequest(result.Errors);
        }


        return Ok(person);
    }
}
