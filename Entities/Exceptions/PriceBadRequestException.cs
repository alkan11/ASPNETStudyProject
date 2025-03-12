namespace Entities.Exceptions
{
    public class PriceBadRequestException : BadRequestExceptions
    {
        public PriceBadRequestException() : base("Maximum price shold be less than 1000 and greater than 10.")
        {
        }
    }
}
