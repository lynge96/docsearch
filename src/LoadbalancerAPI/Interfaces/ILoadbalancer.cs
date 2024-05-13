using System.Collections.Generic;

namespace LoadbalancerAPI.Interfaces;

public interface ILoadbalancer
{
    string? NextEndpoint(string? username);

    Dictionary<string, string> AllEndpoints();

}
