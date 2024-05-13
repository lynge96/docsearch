using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadbalancerAPI.Interfaces;

public interface IHealthCheck
{
    Task<Dictionary<string,string>> CheckServers();

}
