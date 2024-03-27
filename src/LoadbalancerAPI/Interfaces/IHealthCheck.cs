namespace LoadbalancerAPI.Interfaces;

public interface IHealthCheck
{
    Task<List<string>> CheckServers();

}
