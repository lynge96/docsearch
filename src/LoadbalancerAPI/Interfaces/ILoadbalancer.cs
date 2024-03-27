namespace LoadbalancerAPI.Interfaces;

public interface ILoadbalancer
{
    Task<string?> NextEndpoint();

    Task<List<string>> AllEndpoints();

}
