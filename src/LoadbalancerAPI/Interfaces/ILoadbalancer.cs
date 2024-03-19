namespace LoadbalancerAPI.Interfaces;

public interface ILoadbalancer
{
    string GetNextEndpoint();

    List<string> GetAllEndpoints();

    void CheckAvailableServers();
}
