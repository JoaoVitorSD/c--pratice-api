public class CreateSaleUseCase : IUseCase<CreateSaleOutput, CreateSaleInput>
{

    private readonly ISaleRepository _repository;
    public CreateSaleUseCase(ISaleRepository salesRepository){
        this._repository = salesRepository;
    }
    public async Task<CreateSaleOutput> Execute(CreateSaleInput input){
        Sale sale = InputToSale(input);
        return SaleToOutput(await _repository.Create(sale));
    }


    private static Sale InputToSale(CreateSaleInput input){
        return new Sale{
            ProductName = input.ProductName,
            Price = input.Price,
            SaleDate = input.SaleDate
        };
    }
    private static CreateSaleOutput SaleToOutput(Sale sale){
        return new CreateSaleOutput{
            Id = sale.Id,
            ProductName = sale.ProductName,
            Price = sale.Price,
            SaleDate = sale.SaleDate
        };
    }
}


public class CreateSaleOutput{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public DateTime SaleDate { get; set; }
}

public class CreateSaleInput
{
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public DateTime SaleDate { get; set; }
}