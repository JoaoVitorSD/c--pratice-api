public class GetSaleUseCase{
    private readonly ISaleRepository _repository;
    public GetSaleUseCase(ISaleRepository salesRepository){
        this._repository = salesRepository;
    }
    public async Task<List<GetSaleOutput>> Execute(){
        List<Sale> sales = await _repository.FindAllSales();
        return sales.Select(SaleToOutput).ToList();
    }
    private static GetSaleOutput SaleToOutput(Sale sale){
        return new GetSaleOutput{
            Id = sale.Id,
            ProductName = sale.ProductName,
            Price = sale.Price,
            SaleDate = sale.SaleDate
        };
    }
}

public class GetSaleOutput{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public DateTime SaleDate { get; set; }
}