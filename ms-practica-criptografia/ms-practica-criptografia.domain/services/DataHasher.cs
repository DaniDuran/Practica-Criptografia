using ms_practica_criptografia.domain.contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ms_practica_criptografia.domain.services
{
    /// <summary>
    /// Clase que proporciona funciones de encriptación y generación de hash.
    /// </summary>
    public class DataHasher : IDataHasher
    {
        private const int SALT_SIZE = 16;//Datos aleatorios = salt (prepend)
        private const int KEY_SIZE = 64;
        private const int ITERATIONS = 50000;
        private static readonly HashAlgorithmName algorithmName = HashAlgorithmName.SHA512;
        private const char DELIMITER = ':';

        /// <summary>
        /// Genera un hash a partir de los datos proporcionados, utilizando un salt único.
        /// </summary>
        /// <param name="data">Datos a ser hashados.</param>
        /// <returns>Una cadena que contiene el hash, el salt, el número de iteraciones y el nombre del algoritmo, separados por el delimitador.</returns>
        public string Hash(byte[] data)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SALT_SIZE);
            // Aplica el algoritmo PBKDF2 para derivar una clave hash a partir de los datos y el salt
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(data,salt,ITERATIONS, algorithmName, KEY_SIZE);
            // Combina el hash, salt, número de iteraciones y nombre del algoritmo en una cadena con el delimitador
            return string.Join(DELIMITER, Convert.ToHexString(hash),Convert.ToHexString(salt),ITERATIONS,algorithmName);
        }

        /// <summary>
        /// Verifica si los datos proporcionados coinciden con un hash almacenado.
        /// </summary>
        /// <param name="data">Datos a ser verificados.</param>
        /// <param name="hashData">Cadena que contiene el hash, salt, número de iteraciones y nombre del algoritmo, separados por el delimitador.</param>
        /// <returns>True si los datos coinciden con el hash almacenado; de lo contrario, False.</returns>
        public bool Verify(byte[] data, string hashData)
        {
            // Divide la cadena hashData en segmentos utilizando el delimitador
            string[] segmento = hashData.Split(DELIMITER);

            // Recupera el hash, salt, número de iteraciones y nombre del algoritmo de los segmentos
            byte[] storedHash = Convert.FromHexString(segmento[0]);
            byte[] storedSalt = Convert.FromHexString(segmento[1]);
            int iterations = Convert.ToInt32(segmento[2]);
            HashAlgorithmName algorithmName = new HashAlgorithmName(segmento[3]);

            // Calcula un nuevo hash utilizando los datos, salt y parámetros almacenados
            byte[] calculatedHash = Rfc2898DeriveBytes.Pbkdf2(data, storedSalt, iterations, algorithmName,storedHash.Length);

            // Compara de manera segura el hash calculado con el hash almacenado para evitar ataques de tiempo fijo para evitar ataque vector lateral
            return CryptographicOperations.FixedTimeEquals(calculatedHash, storedHash);
        }
    }
}
