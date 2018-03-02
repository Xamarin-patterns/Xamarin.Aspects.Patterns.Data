using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using FluentValidation;
using FluentValidation.Internal;
using PostSharp.Aspects;
using PostSharp.Aspects.Advices;
using PostSharp.Aspects.Configuration;
using PostSharp.Aspects.Serialization;
using PostSharp.Serialization;

namespace Xamarin.Aspects.Patterns.Data.Validation
{
    [PSerializable]
    [AspectConfiguration(SerializerType = typeof(MsilAspectSerializer))]
    [IntroduceInterface(typeof(INotifyDataErrorInfo))]
    public sealed class NotifyDataErrorInfoAttribute : InstanceLevelAspect ,INotifyDataErrorInfo
    {
        private  IValidator _validator;

        public NotifyDataErrorInfoAttribute(Type validatorType)
        {
            _validator =(IValidator) Activator.CreateInstance(validatorType);
        }
        [IntroduceMember(OverrideAction = MemberOverrideAction.Ignore)]
        public IEnumerable GetErrors(string propertyName)
        {
            
            var validationResult = _validator.Validate(new ValidationContext(Instance, new PropertyChain(),
                new MemberNameValidatorSelector(new []{propertyName})));
            return validationResult.Errors.Select(x => x.ErrorMessage);
        }

        [IntroduceMember(OverrideAction = MemberOverrideAction.Ignore)]
        public bool HasErrors
        {
            get
            {
                var validationResult = _validator.Validate(Instance);
                var validationFailures = validationResult.Errors;
                return validationFailures.Any();
            }
        }

        [IntroduceMember(OverrideAction = MemberOverrideAction.Ignore)]
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
    }
}
