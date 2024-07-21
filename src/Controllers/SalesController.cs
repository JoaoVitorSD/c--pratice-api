using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class CompraController : ControllerBase
{

    private SalesRepository _salesRepository;

    public CompraController(IConfiguration configuration)
    {
        Console.WriteLine("Connection Initiated" + configuration.GetConnectionString("SalesDbConn"));
        _salesRepository = new SalesRepository(configuration.GetConnectionString("SalesDbConn"));
    }
    [HttpGet]
    public List<Sale> Get()
    {
        return _salesRepository.GetAllSales();
    }

    [HttpPost]
    public void Post([FromBody] Sale sale)
    {
        _salesRepository.AddSale(sale);
    }
}
