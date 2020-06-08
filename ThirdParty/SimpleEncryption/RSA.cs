using System;
using System.Security.Cryptography;
using System.Text;

namespace Assets.SimpleEncryption
{
	public static class RSA
	{
		/// <summary>
		/// Generates privateKey and publicKey. You must share publicKey but keep privateKey hidden.
		/// </summary>
		public static void GenerateKeys(out string publicKey, out string privateKey)
		{
			var rsa = new RSACryptoServiceProvider();
			
			publicKey = rsa.ToXmlString(false);
			privateKey = rsa.ToXmlString(true);
		}

		/// <summary>
		/// Sign data with privateKey. Digital signature may be verified using publicKey later.
		/// </summary>
		public static string SignData(string input, string privateKey)
		{
			var rsa = new RSACryptoServiceProvider();

			rsa.FromXmlString(privateKey);

			return Convert.ToBase64String(rsa.SignData(Encoding.UTF8.GetBytes(input), new SHA1CryptoServiceProvider()));
		}

		/// <summary>
		/// Check digital signature with publicKey.
		/// </summary>
		public static bool CheckSignature(string input, string signature, string publicKey)
		{
			var rsa = new RSACryptoServiceProvider();

			rsa.FromXmlString(publicKey);

			return rsa.VerifyData(Encoding.UTF8.GetBytes(input), new SHA1CryptoServiceProvider(), Convert.FromBase64String(signature));
		}

		/// <summary>
		/// Encrypt a text with publicKey. The only one who knows privateKey can decrypt it.
		/// </summary>
		public static string EncryptText(string input, string publicKey)
		{
			var dataToEncrypt = Encoding.UTF8.GetBytes(input);

			byte[] encryptedData;

			using (var rsa = new RSACryptoServiceProvider())
			{
				rsa.FromXmlString(publicKey);
				encryptedData = rsa.Encrypt(dataToEncrypt, false);
			}

			return Convert.ToBase64String(encryptedData);
		}

		/// <summary>
		/// Decrypt data with privateKey.
		/// </summary>
		public static string DecryptText(string input, string privateKey)
		{
			var dataToDecrypt = Convert.FromBase64String(input);

			byte[] decryptedData;

			using (var rsa = new RSACryptoServiceProvider())
			{
				rsa.FromXmlString(privateKey);
				decryptedData = rsa.Decrypt(dataToDecrypt, false);
			}

			return Encoding.UTF8.GetString(decryptedData);
		}
	}
}