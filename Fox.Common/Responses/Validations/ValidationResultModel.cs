using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Fox.Common.Responses
{
    public interface IValidationResultModel<out T>
    {
        T Model { get; }
    }

    public class ValidationResultModel<T> : IValidationResultModel<T> where T : class
    {
        public T Model { get; set; }
        public List<ValidationResult> Validations { get; set; }

        public ValidationResultModel(T model) : this()
        {
            if (model is ModelStateDictionary)
            {
                var modelStateDictionary = model as ModelStateDictionary;
                this.Validations.AddRange(modelStateDictionary.Keys
                    .SelectMany(key => modelStateDictionary[key].Errors.Select(x => new ValidationResult(key, x.ErrorMessage))
                        .ToList()));
            }
            else if (model is IdentityResult)
            {
                var identityResult = model as IdentityResult;
                this.Validations.AddRange(identityResult.Errors.Select(x => new ValidationResult(x.Code, x.Description)).ToList());
            }
            else
            {
                this.Model = model;
            }
        }

        public ValidationResultModel()
        {
            Validations = new List<ValidationResult>();
        }

        public bool Succeeded
        {
            get
            {
                var valid = this.Validations.Count == 0;
                return valid;
            }
        }
    }
}
