using Microsoft.AspNetCore.Mvc;
using System;

namespace control.Controllers
{
    [ApiController]
    public class LightingController : ControllerBase
    {
        [Route("[controller]/SetRgb")]
        [HttpPost]
        public void SetRgb([FromBody] Lighting lighting)
        {
            lighting.setRgb();
        }
    }
}
