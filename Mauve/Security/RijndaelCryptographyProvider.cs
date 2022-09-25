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
        public RijndaelCryptographyProvider() =>
            Initialize(null, null, Encoding.Unicode, CipherMode.CBC, PaddingMode.PKCS7);
        /// <summary>
        /// Creates a new instance of the <see cref="RijndaelCryptographyProvider"/> using <see cref="CipherMode.CBC"/> along with the specified initialization vector and key.
        /// </summary>
        /// <param name="key">The secret key to be utilized by the symmetric algorithm to encrypt and decrypt data.</param>
        /// <param name="initializationVector">The initialization vector for the symmetric algorithm.</param>
        public RijndaelCryptographyProvider(byte[] key, byte[] initializationVector) =>
            Initialize(key, initializationVector, Encoding.Unicode, CipherMode.CBC, PaddingMode.PKCS7);

        #endregion

        #region Public Methods

        public override void Dispose() => _managedRijndael.Dispose();
        public override T Decrypt<T>(string input)
        {
            string decryptedData = string.Empty;
            byte[] encodedData = Convert.FromBase64String(input);
            using (var memoryStream = new MemoryStream(encodedData))
            using (var cryptoStream = new CryptoStream(memoryStream, _managedRijndael.CreateDecryptor(Key, InitializationVector), CryptoStreamMode.Read))
            using (var streamReader = new StreamReader(cryptoStream, Encoding))
                decryptedData = streamReader.ReadToEnd();

            return decryptedData.Deserialize<T>(SerializationMethod.Json);
        }
        public override string Encrypt<T>(T input)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, _managedRijndael.CreateEncryptor(Key, InitializationVector), CryptoStreamMode.Write))
                {
                    // Get the raw data and write it to the stream.
                    string serializedData = input.Serialize(SerializationMethod.Json);
                    using (var streamWriter = new StreamWriter(cryptoStream, Encoding))
                        streamWriter.Write(serializedData);

                    // Capture the result and return it to the consumer.
                    byte[] encryptedData = memoryStream.ToArray();
                    return Convert.ToBase64String(encryptedData);
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
        private void Initialize(byte[] key, byte[] initializationVector, Encoding encoding, CipherMode cipherMode, PaddingMode paddingMode)
        {
            Encoding = encoding;
            _managedRijndael = new RijndaelManaged
            {
                Mode = cipherMode,
                Padding = paddingMode,
            };

            // If we need a key, generate it.
            if (key is null)
                _managedRijndael.GenerateKey();
            else
                _managedRijndael.Key = key;

            // If we need an initialization vector, generate it.
            if (initializationVector is null)
                _managedRijndael.GenerateIV();
            else
                _managedRijndael.IV = initializationVector;

            // Set the stored key and initialization vector.
            Key = key ?? _managedRijndael.Key;
            InitializationVector = initializationVector ?? _managedRijndael.IV;

            // Utilize unicode as the default encoding.
            Encoding = Encoding.Unicode;
        }

        #endregion

    }
}
