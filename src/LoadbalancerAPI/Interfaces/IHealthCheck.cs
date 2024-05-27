using System.Collections.Generic;
using System.Threading.Tasks;

// Dette interface definerer en kontrakt for sundhedstjek af servere.
// Implementeringen af dette interface skal indeholde en metode til at udføre sundhedstjek og returnere en dictionary med servernavne og deres sundhedsstatus.

namespace LoadbalancerAPI.Interfaces;

public interface IHealthCheck
{
    // Metode til at udføre sundhedstjek af servere
    //Metoden udfører sundhedstjek af servere og returnerer en dictionary med servernavne og deres sundhedsstatus.
    Task<Dictionary<string,string>> CheckServers();

}
