using Microsoft.AspNetCore.Mvc;

[Route("api/compra")]
[ApiController]
public class CompraController : ControllerBase
{

    private SalesRepository _salesRepository;

    public CompraController(IConfiguration configuration)
    {
        _salesRepository = new SalesRepository(configuration.GetConnectionString("SalesDbConn"));
    }
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_salesRepository.GetAllSales());
    }

    [HttpPost]
    public IActionResult Post([FromBody] Sale sale)
    {
      return Ok(  _salesRepository.AddSale(sale));
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Sale updated)
    {
        Sale sale = _salesRepository.UpdateSale(id, updated);
        if (sale == null)
        {
            return NotFound();
        }
        return Ok(sale);
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        _salesRepository.DeleteSale(id);
    }
}
