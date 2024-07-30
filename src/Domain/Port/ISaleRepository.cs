public interface ISaleRepository{
    Task<Sale> Create(Sale sale);
    Task<Sale> Update(Sale sale);

    Task<List<Sale>> FindAllSales();
    Task Delete(int id);
}