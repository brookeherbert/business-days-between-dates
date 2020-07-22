# Business Days Between Dates

.NETCore 3.1 solution, that provides a BusinessDayCounter service, to determine
- Number of week days given a start and end date as parameters
- Number of business days given a start and end date, and a list of specific dates as parameters
- Number of business days given a start and end date, and a list of specific holiday objects as a parameter

This service has been registered in Startup.cs, so to use it in any other class, make use of .NETCore Dependency Injection in your constructor as so:

readonly IBusinessDayCounterService _businessCounter;

public SomeClass(IBusinessDayCounter businessDayCounter)
{
  _businessDayCounter = businessDayCounter;
}

## Getting Started

Clone this repository, and run the test suite 


### Prerequisites

What things you need to run the software:

```
ASP.NET Core Runtime 3.1.6
```


## Running the tests

Use the Visual Studio 2019 test explorer, or Visual Studio Code, to run the Nunit tests on the BusinessDayCounter service


## Built With

* [.NETCore 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1) - The web framework used
* [NUnit](https://nunit.org/) - Unit testing


## Authors

* **Brooke Couchman** - (https://github.com/brookeherbert)





