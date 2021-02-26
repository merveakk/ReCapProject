# ReCapProject - Araba Kiralama Sistemi
Entities, DataAccess, Business, Core, WebAPI katmanlarından oluşan ve gelişmekte olan bir araba kiralama console projesidir.


# LAYERS
Entities: Veritabanı nesneleri(varlıkları) için oluşturuldu. Bu katmanda 2 klasör vardır. Concrete(somut nesneler için) ve DTOs

Business: Veritabanı ile proje arasında bir köprü niteliğindedir. 5 klasöre sahip. Abstract soyut nesneler için, Concrete ise somut nesneler için var. Ayrıca Constants, DependencyResolvers(autofac için) ve ValidationRules(validation için) klasörlerine de sahip.

Data Access: Veriler ve veritabanı ile çalıştığımız katman. Abstract ve Concrete klasörlerine sahip.

Core: Bu katmanda ortak kodlar tutulur. Diğer katmanları referans almaz. Aspects(ValidationAspect), CrossCuttingConcerns(ValidationTool), DataAccess(DataAccess katmanı nesnesi tutar), Entities(Entities katmanının nesnelerini tutar.), Interceptors, Utilities klasörlerinden oluşur.


# Tables in Database 

Cars

Brands

Colors

Customers

Rentals

Users


# Installation

Aşağıdaki paketler NuGet aracığıyla belirtilen katmana eklenmelidir:

DataAccess  ->    Microsoft.EntityFrameworkCore (3.1.11), Microsoft.EntityFrameworkCore.SqlServer (3.1.11)

Business    ->    "Autofac" Version="6.1.0" , "Autofac.Extras.DynamicProxy" Version="6.0.0" , "FluentValidation" Version="9.5.1"

Core        ->    "Autofac" Version="6.1.0" , "Autofac.Extensions.DependencyInjection" Version="7.1.0" , "Autofac.Extras.DynamicProxy" Version="6.0.0" , "FluentValidation" Version="9.5.1" , "Microsoft.EntityFrameworkCore.SqlServer" Version="3.1.11"

WebAPI      ->    "Autofac.Extensions.DependencyInjection" Version="7.1.0"
