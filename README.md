# Xamarin.Aspects.Patterns.Data

Although  postsharp has declared before that they do not support xamarin anymore , it still can be used with dot net standard that gave us some home that we still can move out the boilerplate code from our track , keep our code bases clean and readable. 
This repository should contains postsharp aspects that are related to the data concerns including validating and saving data without writing a lot of boilerplates


## Getting Started
Install from nuget 
[![NuGet](https://img.shields.io/nuget/v/Nuget.Core.svg?style=flat-square)](https://www.nuget.org/packages/Xamarin.Aspects.Patterns.Data.Validation/)

### Note

Do not install postsharp in your xamarin.ios or xamarin.android projects otherwise the compiler will complain saying that postsharp does not support xamarin anymore.


## Aspects to use

### NotifyDataErrorInfo
This aspect depend on [FluentValidation](https://github.com/JeremySkinner/FluentValidation) Framework it server as an Adapter between fluent validation validators and Microsoft [INotifyDataErrorInfo](https://msdn.microsoft.com/en-us/library/system.componentmodel.inotifydataerrorinfo(v=vs.110).aspx) which is very common to use for validation , you can check this controls for out of the box integration with UI [Syncfusion Data Form](https://www.syncfusion.com/products/xamarin-android/dataform)

    public class UserValidator : AbstractValidator<User>
     {
        public UserValidator()
        {
            RuleFor(u => u.Email).EmailAddress();
            RuleFor(u => u.Paswword).MinimumLength(6);
            RuleFor(u => u.ConfirmPassword).Matches(u => u.Paswword);
        }
    }
    [Xamarin.Aspects.Patterns.Data.Validation.NotifyDataErrorInfo(typeof(UserValidator))]
    public class User
    {
        public string Email { get; set; }
        public string Paswword { get; set; }
        public string ConfirmPassword { get; set; }
    }
    

## Acknowledgments

* We are using postsharp to build this aspects as we beilive that postsharp allow us to access the future of c# after using this aspects you will find that your code will become shorter , more readable and more SOLID.
* We are not connected in anyway with Postsharp or Xamarin. 
* You are free to post suggestions and we will keep an eye one every issue and every pull request.


