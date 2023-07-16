
# Sipay Bootcamp assignment-1

Person class icersindeki validation lar FluentValidation kutuphanesi kullanilarak yeniden duzenlenecek.
Controller uzerindeki POST methodu attributelar yerinde FluentValidation ile calisacak sekilde duzenlenecek. 
Odev icerisinde sadece 1 controller ve 1 method teslim edilecek. 


### Fluent Validation Nedir?

FluentValidation bir veri doğrulama kütüphanesidir. FluentValidation ve benzeri ürünlerin kullanılması,
verilerin doğru şekilde yani verilerin oluştururken konulmuş kısıtlamaları sağlayarak kurallara uyumlu
halde olmasını ve kullanıcı ya da sistem kaynaklı hataların oluşmasını engeller.

### Neden FluentValidation?
FluentValidation yerine if else yapısı ve data annotation gibi yapılar kullanılıyor. İf else yapısı her zaman kodun anlaşabilirliğini azaltır ve yapısal olarak en son istenen sonuçlara neden olur. Daha çok data annotation kullanılıyor ve daha pratik kullanılıyor. 

### Fluent Validation Kurulumu:

FluentValidation paketini eklendi.

View->Other Windows -> Package MAnager Console : 

```bash
  Install-Package FluentValidation
```



### İlk Validator’un Oluşturulması
 Person Class: validasyon örneklendirmesi yapacağımız sınıfımızı tasarladık.

 ```bash
    public class Person
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public int AccessLevel { get; set; }
        public decimal Salary { get; set; }
    }
```

 Person sınıfına ait kuralları tanımlamak için bir sınıf oluşturuyoruz ve gerekli miras alma işlemlerini yapıyoruz.
 RuleFor() metotu çağrılarak lambda expression ile hangi property üzerinden kural yazılacağı sağlanır.

 ```bash
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

```



## Badges

Add badges from somewhere like: [shields.io](https://shields.io/)

[![MIT License](https://img.shields.io/badge/License-MIT-green.svg)](https://choosealicense.com/licenses/mit/)
[![GPLv3 License](https://img.shields.io/badge/License-GPL%20v3-yellow.svg)](https://opensource.org/licenses/)
[![AGPL License](https://img.shields.io/badge/license-AGPL-blue.svg)](http://www.gnu.org/licenses/agpl-3.0)
