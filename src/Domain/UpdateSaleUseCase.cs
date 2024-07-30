public class UpdateSaleUseCase : IUseCase<UpdateSaleOutput, UpdateSaleInput>
{

    private readonly ISaleRepository _repository;
    public UpdateSaleUseCase(ISaleRepository salesRepository){
        this._repository = salesRepository;
    }
    public async Task<UpdateSaleOutput> Execute(UpdateSaleInput input){
        Sale sale = InputToSale(input);
        await _repository.Update(sale);
        return SaleToOutput(sale);
    }

    private static Sale InputToSale(UpdateSaleInput input){
        return new Sale{
            Id = input.Id,
            ProductName = input.ProductName,
            Price = input.Price,
            SaleDate = input.SaleDate
        };
    }

    private static UpdateSaleOutput SaleToOutput(Sale sale){
        return new UpdateSaleOutput{
            Id = sale.Id,
            ProductName = sale.ProductName,
            Price = sale.Price,
            SaleDate = sale.SaleDate
        };
    }
}

public class UpdateSaleOutput{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public DateTime SaleDate { get; set; }
}

public class UpdateSaleInput
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public DateTime SaleDate { get; set; }
}