namespace Loadbalancer;

public interface ILoadBalancer
{
    string GetNextEndpoint();

    List<string> GetAllEndpoints();
}
