namespace LoadbalancerAPI.Interfaces;

public interface ILoadbalancer
{
    string? NextEndpoint();

    List<string> AllEndpoints();

}
