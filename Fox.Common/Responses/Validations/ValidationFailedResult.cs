using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Fox.Common.Responses
{
    public class ValidationFailedResult : OkObjectResult
    {
        public ValidationFailedResult(ModelStateDictionary modelState)
            : base(new ValidationResultModel<ModelStateDictionary>(modelState))
        {
            //StatusCode = StatusCodes.Status422UnprocessableEntity;
        }
    }
}
