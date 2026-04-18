#if (useSwagger)
using Asp.Versioning;
#endif
#if (useMediatR)
using MediatR;
#endif
using Microsoft.AspNetCore.Mvc;

namespace Company.SampleService.WebApi.Controllers.v1;

#if (useSwagger)
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
#else
[ApiController]
[Route("api/v1/[controller]")]
#endif
public abstract class BaseApiController : ControllerBase
{
#if (useMediatR)
    protected BaseApiController(IMediator mediator)
    {
        Mediator = mediator;
    }

    protected IMediator Mediator { get; }
#endif
}
