namespace Application.Interfaces;

// Dette interface definerer metoder til at arbejde med en cache-service.

public interface ICacheService
{
    /// <summary>
    /// Henter data ved hjælp af en nøgle fra cachen.
    /// </summary>
    /// <typeparam name="T">Typen af data, der skal hentes.</typeparam>
    /// <param name="key">Nøglen, der identificerer dataene i cachen.</param>
    /// <returns>Data, der er gemt i cachen.</returns>
    /// Denne metode henter data fra cachen ved hjælp af en angivet nøgle og returnerer dataene af typen T, eller null, hvis ingen data blev fundet.
    T? GetData<T>(string key);

    /// <summary>
    /// Gemmer data med en given nøgle og udløbstid i cachen.
    /// </summary>
    /// <typeparam name="T">Typen af data, der skal gemmes.</typeparam>
    /// <param name="key">Nøglen, der skal bruges til at gemme dataene i cachen.</param>
    /// <param name="value">Værdien, der skal gemmes i cachen.</param>
    /// <returns>En boolsk værdi, der indikerer, om dataene blev gemt korrekt i cachen.</returns>
    /// Denne metode gemmer data i cachen med en angivet nøgle og den tilsvarende værdi af typen T. Den returnerer en boolsk værdi, der angiver, om dataene blev gemt korrekt i cachen.
    bool SetData<T>(string key, T value);


    /// <summary>
    /// Fjerner data fra cachen ved hjælp af en given nøgle.
    /// </summary>
    /// <param name="key">Nøglen, der identificerer dataene, der skal fjernes fra cachen.</param>
    /// <returns>Objektet, der blev fjernet fra cachen, eller null, hvis ingen data blev fjernet.</returns>
    /// Denne metode fjerner data fra cachen ved hjælp af den angivne nøgle og returnerer objektet, der blev fjernet fra cachen, eller null, hvis ingen data blev fjernet.
    object RemoveData(string key);
}