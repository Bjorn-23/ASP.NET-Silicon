using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Silicon_design_webapp.Helpers;


public static class ErrorChecker
{

    public static Object ModelStateErrorChecker(ModelStateDictionary modelState)
    {
        var errors = modelState
        .Where(x => x.Value!.Errors.Count > 0)
        .Select(x => new { x.Key, x.Value!.Errors })
        .ToArray();

        return errors;
    }
}

public class JustAClass
{
    public bool HereIstTheCodeToCheckTheMethodWith(ModelStateDictionary ModelState)
    {
        var error = ErrorChecker.ModelStateErrorChecker(ModelState); // use this line to check for errors in modelState
        return true;
    }
}
