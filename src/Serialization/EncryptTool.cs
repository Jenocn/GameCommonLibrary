/*
	By Jenocn
	https://jenocn.github.io/
*/

using Assets.SimpleEncryption;

namespace GCL.Serialization {

	/// <summary>
	/// 加密工具
	/// </summary>
	public static class EncryptTool {

		public static string ToMD5(string input) {
			return Md5.ComputeHash(input);
		}

		public static string EncryptAES(byte[] value, string password, string SaltKey, string VIKey, int KeyLength = 128) {
			return AES.Encrypt(value, password, SaltKey, VIKey, KeyLength);
		}

		public static string DecryptAES(string value, string password, string SaltKey, string VIKey, int KeyLength = 128) {
			return AES.Decrypt(value, password, SaltKey, VIKey, KeyLength);
		}

		public static string EncodeBase64(string value) {
			return Base64.Encode(value);
		}

		public static string DecodeBase64(string value) {
			return Base64.Decode(value);
		}

		public static string EncodeB64R(string value) {
			return B64R.Encode(value);
		}

		public static string DecodeB64R(string value) {
			return B64R.Decode(value);
		}

		public static string EncryptB64X(string value, string key) {
			return B64X.Encrypt(value, key);
		}

		public static string DecryptB64X(string value, string key) {
			return B64X.Decrypt(value, key);
		}

		public static string EncryptRSA(string value, string publicKey) {
			return RSA.EncryptText(value, publicKey);
		}

		public static string DecryptRSA(string value, string privateKey) {
			return RSA.DecryptText(value, privateKey);
		}

		public static void GenerateKeys(out string publicKey, out string privateKey) {
			RSA.GenerateKeys(out publicKey, out privateKey);
		}

		public static string SignData(string input, string privateKey) {
			return RSA.SignData(input, privateKey);
		}

		public static bool CheckSignature(string input, string signature, string publicKey) {
			return RSA.CheckSignature(input, signature, publicKey);
		}
	}
}