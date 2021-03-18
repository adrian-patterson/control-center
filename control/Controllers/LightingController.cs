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

        [Route("[controller]/Yellow")]
        public void Yellow()
        {
            Lighting lighting = new Lighting(0,255,255,true);
            lighting.setRgb();
        }
    }
}
