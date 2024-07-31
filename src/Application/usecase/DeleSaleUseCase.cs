public class DeleSaleUseCase : IUseCase<DeleSaleInput>
{

    private readonly ISaleRepository _repository;
    public DeleSaleUseCase(ISaleRepository salesRepository){
        this._repository = salesRepository;
    }
    public async Task Execute(DeleSaleInput input){
        await _repository.Delete(input.Id);
    }
}

public class DeleSaleInput
{
    public int Id { get; set; }
}