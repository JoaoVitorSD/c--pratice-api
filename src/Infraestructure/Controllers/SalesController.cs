using Microsoft.AspNetCore.Mvc;

[Route("api/compra")]
[ApiController]
public class CompraController : ControllerBase
{

    private CreateSaleUseCase _createSaleUseCase;
    private UpdateSaleUseCase _updateSaleUseCase;
    private DeleSaleUseCase _deleSaleUseCase;
    private GetSaleUseCase _getSaleUseCase;

    public CompraController(IConfiguration configuration)
    {
        var repository = new SalesRepositoryAdapter(configuration.GetConnectionString("SalesDbConn"));
        _createSaleUseCase = new CreateSaleUseCase(repository);
        _updateSaleUseCase = new UpdateSaleUseCase(repository);
        _deleSaleUseCase = new DeleSaleUseCase(repository);
        _getSaleUseCase = new GetSaleUseCase(repository);
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _getSaleUseCase.Execute());
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateSaleInput input)
    {
      return Ok(await _createSaleUseCase.Execute(input));
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateSaleInput input)
    {
        return Ok(await _updateSaleUseCase.Execute(input));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        DeleSaleInput deleSaleInput = new DeleSaleInput();
        await _deleSaleUseCase.Execute(deleSaleInput);
        return NoContent();
    }
}
