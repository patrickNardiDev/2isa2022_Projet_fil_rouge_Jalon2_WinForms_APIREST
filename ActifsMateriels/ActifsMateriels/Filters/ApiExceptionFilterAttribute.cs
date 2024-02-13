using Domain.Exeption;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters;

public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

    public ApiExceptionFilterAttribute()
    {
        // Register known exception types and handlers.
        _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
        {
            { typeof(NotFoundEntityException), HandleNotFoundException }, // 404
            { typeof(NotFoundEntitiesExeption), HandleNotFoundException },
            { typeof(UnauthorizeException), HandleUnauthorizedAccessException }, //401
            { typeof(ForbiddenAccessException), HandleForbiddenAccessException }, //403
            {typeof(InsertEntityException), HandleBadRequest }, //400
            {typeof(UpdateEntityException), HandleBadRequest },
            {typeof(DeleteEntityException), HandleBadRequest },
            {typeof(ValidationException), HandleValidationException }, // Fluentvalidator 415
            {typeof(Domain.Exeption.SysException), HandleSysException } 
         // { typeof(ForbiddenAccessException), HandleForbiddenAccessException },

        };
    }

    public override void OnException(ExceptionContext context)
    {
        HandleException(context);

        base.OnException(context);
    }
    /// <summary>
    /// Handle the good exception for the context
    /// </summary>
    /// <param name="context">Context of exception</param>
    private void HandleException(ExceptionContext context)
    {
        Type type = context.Exception.GetType();
        if (_exceptionHandlers.ContainsKey(type))
        {
            _exceptionHandlers[type].Invoke(context);
            return;
        }
        HandleUnknownException(context); // 500
    }

    /// <summary>
    /// Handle bad request 400
    /// </summary>
    /// <param name="context"></param>
    private void HandleBadRequest(ExceptionContext context)
    {
        var exception = context.Exception;

        var details = new ProblemDetails()
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Title = "Client error.",
            Detail = exception?.Message,
        };
        context.Result = new BadRequestObjectResult(details)
        {
            //Value = exception?.Message,

        };
        context.ExceptionHandled = true;
    }

    /// <summary>
    /// Handle a not found ressource exception 404
    /// </summary>
    /// <param name="context">Context of exception</param>
    private void HandleNotFoundException(ExceptionContext context)
    {
        var exception = context.Exception;

        var details = new ProblemDetails()
        {
            Status = StatusCodes.Status404NotFound,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            Title = "The specified resource was not found.",
            Detail = exception?.Message,

        };

        context.Result = new NotFoundObjectResult(details)
        {
            StatusCode = StatusCodes.Status404NotFound,
            //Value = exception?.Message,
        };

        context.ExceptionHandled = true;
    }


    /// <summary>
    /// Handle for Unauthorized Access Exception 401
    /// </summary>
    /// <param name="context">Context of the exception</param>
    private void HandleUnauthorizedAccessException(ExceptionContext context)
    {
        var exception = context.Exception as UnauthorizeException;

        var details = new ProblemDetails
        {
            Status = StatusCodes.Status401Unauthorized,
            Title = "Unauthorized",
            Type = "https://tools.ietf.org/html/rfc7235#section-3.1",
            Detail = exception?.Message,
        };

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status401Unauthorized,
            //Value = exception?.Message,

        };

        context.ExceptionHandled = true;
    }

    /// <summary>
    /// Handle a forbidden Access excception 403
    /// </summary>
    /// <param name="context">Context of the exception</param>
    private void HandleForbiddenAccessException(ExceptionContext context)
    {
        var exception = context.Exception as ForbiddenAccessException;

        var details = new ProblemDetails
        {
            Status = StatusCodes.Status403Forbidden,
            Title = "Forbidden",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3",
            Detail = exception?.Message,
        };

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status403Forbidden,
            //Value = exception?.Message,

        };

        context.ExceptionHandled = true;
    }

    private void HandleValidationException(ExceptionContext context)
    {
        var exception = context.Exception as ValidationException;

        var details = new ProblemDetails()
        {
            Status = StatusCodes.Status415UnsupportedMediaType,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.13",
            Title = "Format Error",
            Detail = exception?.Message,

        };

        context.Result = new NotFoundObjectResult(details)
        {
            StatusCode = StatusCodes.Status415UnsupportedMediaType,
            //Value = exception?.Message,

        };

        context.ExceptionHandled = true;
    }


    private void HandleSysException(ExceptionContext context)
    {
        var exception = context.Exception as SysException;

        var details = new ProblemDetails()
        {
            Status = StatusCodes.Status418ImATeapot,
            Type = "https://developer.mozilla.org/fr/docs/Web/HTTP/Status/418",
            Title = "Systeme Error.",
            Detail = exception?.Message,

        };

        context.Result = new NotFoundObjectResult(details)
        {
            StatusCode = StatusCodes.Status418ImATeapot,
            //Value = exception?.Message,

        };

        context.ExceptionHandled = true;
    }


    /// <summary>
    /// Handle for an Unknow Exception 500
    /// </summary>
    /// <param name="context">Context of the exception</param>
    private void HandleUnknownException(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "An error occurred while processing your request.",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            Detail = "Erreur inatandue",

        };

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status500InternalServerError
            //Value = "Erreur inatandue",

        };

        context.ExceptionHandled = true;
    }
}
