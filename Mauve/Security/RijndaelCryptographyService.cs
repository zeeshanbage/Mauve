using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

using Mauve.Extensibility;

using Newtonsoft.Json;

namespace Mauve.Security
{
    /// <summary>
    /// Represents a <see cref="CryptographyService"/> providing simplified access to the managed version of the <see cref="Rijndael"/> algorithm.
    /// </summary>
    public class RijndaelCryptographyService : CryptographyService
    {

        #region Fields

        private readonly RijndaelManaged _managedRijndael;

        #endregion

        #region Properties

        /// <summary>
        /// The secret key to be utilized by the symmetric algorithm to encrypt and decrypt data.
        /// </summary>
        public byte[] Key { get; private set; }
        /// <summary>
        /// The initialization vector for the symmetric algorithm.
        /// </summary>
        public byte[] InitializationVector { get; private set; }
        /// <summary>
        /// The encoding that used during the encryption and decryption process.
        /// </summary>
        public Encoding Encoding { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="RijndaelCryptographyService"/> using <see cref="CipherMode.CBC"/> along with a generated initialization vector and key.
        /// </summary>
        public RijndaelCryptographyService()
        {
            // Create an instance of the managed Rijndael algorithm.
            _managedRijndael = new RijndaelManaged { Mode = CipherMode.CBC };

            // Generate an initialization vector.
            _managedRijndael.GenerateIV();
            InitializationVector = _managedRijndael.IV;

            // Generate a key.
            _managedRijndael.GenerateKey();
            Key = _managedRijndael.Key;

            // Utilize unicode as the default encoding.
            Encoding = Encoding.Unicode;
        }
        /// <summary>
        /// Creates a new instance of the <see cref="RijndaelCryptographyService"/> using <see cref="CipherMode.CBC"/> along with the specified initialization vector and key.
        /// </summary>
        /// <param name="key">The secret key to be utilized by the symmetric algorithm to encrypt and decrypt data.</param>
        /// <param name="initializationVector">The initialization vector for the symmetric algorithm.</param>
        public RijndaelCryptographyService(byte[] key, byte[] initializationVector)
        {
            // Set the key and initialization vector.
            Key = key;
            InitializationVector = initializationVector;

            // Create an instance of the managed Rijndael algorithm.
            _managedRijndael = new RijndaelManaged
            {
                Mode = CipherMode.CBC,
                Key = Key,
                IV = InitializationVector
            };

            // Utilize unicode as the default encoding.
            Encoding = Encoding.Unicode;
        }

        #endregion

        #region Public Methods

        public override void Dispose() => _managedRijndael.Dispose();
        public override T Decrypt<T>(string input)
        {
            string decryptedData = string.Empty;
            byte[] encodedData = Encoding.GetBytes(input);

            try
            {
                using (var memoryStream = new MemoryStream(encodedData))
                {
                    // Read the initialization vector from the stream.
                    byte[] iv = new byte[16];
                    int offset = 0;
                    while (offset < iv.Length)
                        offset += memoryStream.Read(iv, offset, iv.Length - offset);

                    // Set the initialization vector and key.
                    _managedRijndael.IV = iv;
                    _managedRijndael.Key = Key;
                    using (var cryptoStream = new CryptoStream(memoryStream, _managedRijndael.CreateDecryptor(), CryptoStreamMode.Read))
                    using (var streamReader = new StreamReader(cryptoStream, Encoding))
                        decryptedData = streamReader.ReadToEnd();
                }
            } finally
            {
                _managedRijndael.IV = InitializationVector;
            }

            return decryptedData.Deserialize<T>(SerializationMethod.Json);
        }
        public override string Encrypt<T>(T input)
        {
            // Ensure the Rijndael instance is using the proper iv and key.
            _managedRijndael.Key = Key;
            _managedRijndael.IV = InitializationVector;

            using (var memoryStream = new MemoryStream())
            {
                // Append the IV to the front of the stream.
                memoryStream.Write(InitializationVector, 0, InitializationVector.Length);
                using (var cryptoStream = new CryptoStream(memoryStream, _managedRijndael.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    // Get the raw data.
                    string serializedData = input.Serialize(SerializationMethod.Json);
                    byte[] rawData = Encoding.GetBytes(serializedData);

                    // Encode the supplied data and write it to the buffer.
                    cryptoStream.Write(rawData, 0, rawData.Length);
                    cryptoStream.FlushFinalBlock();

                    // Encrypt and return the data.
                    byte[] encryptedData = memoryStream.ToArray();
                    return Encoding.GetString(encryptedData);
                }
            }
        }

        #endregion

    }
}
