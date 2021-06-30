namespace Helium.DependencyInjection
{
    public interface IDbClientFactory
    {
        DbClient CreateClient(string name);
    }
}