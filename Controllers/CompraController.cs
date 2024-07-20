using Microsoft.AspNetCore.Mvc;


namespace CompraApi.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController : ControllerBase
{

    [HttpGet]
    public string[] Get()
    {
        string[] compras = {"Compra 1", "Compra 2", "Compra 3"};
        return compras;
    }
}

}