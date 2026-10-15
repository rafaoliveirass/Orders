using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Application.Erros
{
    public class CustomValidationFailure
    {
        public CustomValidationFailure(string errorMessage, string propertyName)
        {
            ErrorMessage = errorMessage;
            PropertyName = propertyName;
        }
        public string ErrorMessage { get; set; }

        public string PropertyName { get; set; }
    }
}
