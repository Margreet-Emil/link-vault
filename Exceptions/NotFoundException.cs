namespace LinkVault.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string resource , int id) : base($"{resource} with id {id} not found!!")
            { }
    }
}
