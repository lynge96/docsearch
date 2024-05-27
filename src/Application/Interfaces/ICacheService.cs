namespace Application.Interfaces;

// Dette interface definerer metoder til at arbejde med en cache-service.

public interface ICacheService
{
    /// <summary>
    /// Henter data ved hj�lp af en n�gle fra cachen.
    /// </summary>
    /// <typeparam name="T">Typen af data, der skal hentes.</typeparam>
    /// <param name="key">N�glen, der identificerer dataene i cachen.</param>
    /// <returns>Data, der er gemt i cachen.</returns>
    /// Denne metode henter data fra cachen ved hj�lp af en angivet n�gle og returnerer dataene af typen T, eller null, hvis ingen data blev fundet.
    T? GetData<T>(string key);

    /// <summary>
    /// Gemmer data med en given n�gle og udl�bstid i cachen.
    /// </summary>
    /// <typeparam name="T">Typen af data, der skal gemmes.</typeparam>
    /// <param name="key">N�glen, der skal bruges til at gemme dataene i cachen.</param>
    /// <param name="value">V�rdien, der skal gemmes i cachen.</param>
    /// <returns>En boolsk v�rdi, der indikerer, om dataene blev gemt korrekt i cachen.</returns>
    /// Denne metode gemmer data i cachen med en angivet n�gle og den tilsvarende v�rdi af typen T. Den returnerer en boolsk v�rdi, der angiver, om dataene blev gemt korrekt i cachen.
    bool SetData<T>(string key, T value);


    /// <summary>
    /// Fjerner data fra cachen ved hj�lp af en given n�gle.
    /// </summary>
    /// <param name="key">N�glen, der identificerer dataene, der skal fjernes fra cachen.</param>
    /// <returns>Objektet, der blev fjernet fra cachen, eller null, hvis ingen data blev fjernet.</returns>
    /// Denne metode fjerner data fra cachen ved hj�lp af den angivne n�gle og returnerer objektet, der blev fjernet fra cachen, eller null, hvis ingen data blev fjernet.
    object RemoveData(string key);
}