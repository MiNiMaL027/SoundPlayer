namespace Services.Interfaces
{
    public interface IAuthorizeService
    {
        public string GetUserId();

        Task AuthorizeUser(int id);
    }
}
