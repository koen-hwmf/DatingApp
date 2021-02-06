using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // user has to enter api/users, so [controller] extracts users from UsersController 
    public class BaseApiController: ControllerBase
    {
    }
}
