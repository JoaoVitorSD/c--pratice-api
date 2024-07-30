public interface IUseCase<TOutput, TInput>{
    Task<TOutput> Execute(TInput input);
}


public interface IUseCase<TInput>{
    Task Execute(TInput input);
}