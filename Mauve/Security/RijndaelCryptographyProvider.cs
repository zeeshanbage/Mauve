using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

using Mauve.Extensibility;

using Newtonsoft.Json;

namespace Mauve.Security
{
    /// <summary>
    /// Represents a <see cref="CryptographyProvider"/> providing simplified access to the managed version of the <see cref="Rijndael"/> algorithm.
    /// </summary>
    public class RijndaelCryptographyProvider : CryptographyProvider
    {

        #region Fields

        private ICryptoTransform _encryptionTransform;
        private ICryptoTransform _decryptionTransform;
        private RijndaelManaged _managedRijndael;

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
        /// Creates a new instance of the <see cref="RijndaelCryptographyProvider"/> using <see cref="CipherMode.CBC"/> along with a generated initialization vector and key.
        /// </summary>
        public RijndaelCryptographyProvider()
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
            Encoding = Encoding.UTF8;
        }
        /// <summary>
        /// Creates a new instance of the <see cref="RijndaelCryptographyProvider"/> using <see cref="CipherMode.CBC"/> along with the specified initialization vector and key.
        /// </summary>
        /// <param name="key">The secret key to be utilized by the symmetric algorithm to encrypt and decrypt data.</param>
        /// <param name="initializationVector">The initialization vector for the symmetric algorithm.</param>
        public RijndaelCryptographyProvider(byte[] key, byte[] initializationVector)
        {
            
        }

        #endregion

        #region Public Methods

        public override void Dispose() => _managedRijndael.Dispose();
        public override T Decrypt<T>(string input)
        {
            string decryptedData = string.Empty;
            byte[] encodedData = Encoding.GetBytes(input);
            using (var memoryStream = new MemoryStream(encodedData))
            using (var cryptoStream = new CryptoStream(memoryStream, _managedRijndael.CreateDecryptor(), CryptoStreamMode.Read))
            using (var streamReader = new StreamReader(cryptoStream, Encoding))
                decryptedData = streamReader.ReadToEnd();

            return decryptedData.Deserialize<T>(SerializationMethod.Json);
        }
        public override string Encrypt<T>(T input)
        {
            using (var memoryStream = new MemoryStream())
            {
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

        #region Private Methods

        /// <summary>
        /// Initializes the <see cref="RijndaelCryptographyProvider"/>.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="initializationVector"></param>
        /// <param name="encoding"></param>
        /// <param name="cipherMode"></param>
        private void Initialize(byte[] key, byte[] initializationVector, Encoding encoding, CipherMode cipherMode)
        {
            Key = key;
            InitializationVector = initializationVector;
            Encoding = encoding;
            _managedRijndael = new RijndaelManaged
            {
                Mode = cipherMode,
                Key = Key,
                IV = InitializationVector
            };
        }

        #endregion

    }
}
