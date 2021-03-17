using Microsoft.AspNetCore.Mvc;


namespace control.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LightingController : ControllerBase
    {
        [HttpPost]
        public void ToggleLight([FromBody] Lighting lighting)
        {
            lighting.setRgb();
        }
    }
}
