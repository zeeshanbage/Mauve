using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

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
        private readonly ICryptoTransform _encryptionTransform;
        private readonly ICryptoTransform _decryptionTransform;

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

            // Create the crypto transforms.
            _encryptionTransform = _managedRijndael.CreateEncryptor();
            _decryptionTransform = _managedRijndael.CreateDecryptor();

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
            _managedRijndael = new RijndaelManaged {
                Mode = CipherMode.CBC,
                Key = Key,
                IV = InitializationVector
            };

            // Create the crypto transforms.
            _encryptionTransform = _managedRijndael.CreateEncryptor();
            _decryptionTransform = _managedRijndael.CreateDecryptor();

            // Utilize unicode as the default encoding.
            Encoding = Encoding.Unicode;
        }

        #endregion

        #region Public Methods

        public override void Dispose()
        {
            _encryptionTransform.Dispose();
            _decryptionTransform.Dispose();
            _managedRijndael.Dispose();
        }
        public override T Decrypt<T>(string input)
        {
            byte[] encodedData = Encoding.GetBytes(input);
            using (var memoryStream = new MemoryStream(encodedData))
            {
                using (var cryptoStream = new CryptoStream(memoryStream, _decryptionTransform, CryptoStreamMode.Read))
                {
                    // Read the decrypted data to the buffer.
                    byte[] decryptedData = new byte[encodedData.Length];
                    int decryptedByteCount = cryptoStream.Read(decryptedData, 0, decryptedData.Length);

                    string utf8Data = Encoding.UTF8.GetString(decryptedData, 0, decryptedByteCount);
                    return typeof(T) == typeof(string) ? (T)Convert.ChangeType(utf8Data, typeof(T)) : JsonConvert.DeserializeObject<T>(utf8Data);
                }
            }
        }
        public override string Encrypt<T>(T input)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, _encryptionTransform, CryptoStreamMode.Write))
                {
                    // Get the raw data.
                    byte[] rawData = typeof(T) == typeof(string)
                        ? Encoding.UTF8.GetBytes((string)Convert.ChangeType(input, typeof(string)))
                        : Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(input));

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
