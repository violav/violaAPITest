using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xxf.Common.Exceptions;

namespace BusinessLogic.DomainDataError
{
    public class BusinessError
    { public static class BusinessMatErrors
        {

            public static readonly Xxf.Common.Exceptions.DomainDataError OrderSavingErrors = new Xxf.Common.Exceptions.DomainDataError(BusinessLogic.DomainDataError.Resources.ErrorMessages.CategoryNotCreated);//, Instance, ErrorMessages.ValidationErrors, StatusCodes.Status400BadRequest, Instance);
            public static readonly Xxf.Common.Exceptions.DomainDataError ShapeSavingException = new Xxf.Common.Exceptions.DomainDataError(BusinessLogic.DomainDataError.Resources.ErrorMessages.CategoryNotCreated);
            public static readonly Xxf.Common.Exceptions.DomainDataError MaterialSavingException = new Xxf.Common.Exceptions.DomainDataError(BusinessLogic.DomainDataError.Resources.ErrorMessages.MaterialException);

            public static readonly OperationResultValidationError CategoryAlreadyExists = new OperationResultValidationError() { Key = "ERR004", Value = BusinessLogic.DomainDataError.Resources.ErrorMessages.CategoryNotCreated };
            public static readonly OperationResultValidationError CategoryAlreadyExists2 = new OperationResultValidationError() { Key = "ERR001", Value = BusinessLogic.DomainDataError.Resources.ErrorMessages.FeatureMaterialError };

        }
    }
}
