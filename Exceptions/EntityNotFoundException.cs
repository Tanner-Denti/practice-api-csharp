namespace sample_api.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string message) : base(message)
    {
    }
    
    public EntityNotFoundException(string entity, object key) :
        base($"{entity} with identifier {key} was not found.")
    {
    }
}