using CSCI_308_TEAM5.API.Actions.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CSCI_308_TEAM5.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IAuthenticationAction authenticationAction) : ControllerBase
    {



    }
}
