using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using FluentValidation.Results;




namespace SipayApi.Controllers;



//Student Person
public class Person
{
   
    public string Name { get; set; }
    public string Lastname { get; set; }
    public string Phone { get; set; }
    public int AccessLevel { get; set; }
    public decimal Salary { get; set; }
}

public class PersonValidator : AbstractValidator<Person>
{
    public PersonValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .Length(5, 100);

        RuleFor(p => p.Lastname)
            .NotEmpty()
            .Length(5, 100);

        RuleFor(p => p.Phone)
            .NotEmpty()
            .Matches(@"^\d{11}$");

        RuleFor(p => p.AccessLevel)
            .InclusiveBetween(1, 5);


        RuleFor(p => p.Salary)
            .NotEmpty()
            .InclusiveBetween(5000, 50000);

    }
}

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
    public IActionResult CreatePerson ([FromBody] Person person)
    {
        var validator = new SalaryValidator();
        ValidationResult result = validator.Validate(person);
        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }

      

        return Ok(person);
    }
}
