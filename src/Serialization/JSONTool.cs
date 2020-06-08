/*
	By Jenocn
	https://jenocn.github.io/
*/

using System.Collections.Generic;
using Newtonsoft.Json;

namespace GCL.Serialization {

	/// <summary>
	/// JSON工具
	/// </summary>
	public static class JSONTool {

		/// <summary>
		/// 解析Json
		/// </summary>
		public static List<Dictionary<string, string>> ParseToList(string src) {
			if (string.IsNullOrEmpty(src)) { return new List<Dictionary<string, string>>(); }
			return JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(src);
		}

		/// <summary>
		/// 解析Json
		/// </summary>
		public static Dictionary<string, string> ParseToKV(string src) {
			if (string.IsNullOrEmpty(src)) { return new Dictionary<string, string>(); }
			return JsonConvert.DeserializeObject<Dictionary<string, string>>(src);
		}

		/// <summary>
		/// 解析Json
		/// </summary>
		public static List<T> ParseToList<T>(string src) {
			if (string.IsNullOrEmpty(src)) { return new List<T>(); }
			return JsonConvert.DeserializeObject<List<T>>(src);
		}

		/// <summary>
		/// <para>直接序列化Json为对象</para>
		/// <para>如果解析失败返回null</para>
		/// </summary>
		public static T ParseToObject<T>(string src) where T : class, new() {
			if (string.IsNullOrEmpty(src)) { return null; }
			return JsonConvert.DeserializeObject<T>(src);
		}

		/// <summary>
		/// 解析为自定义Key-Value类型的字典中
		/// </summary>
		public static Dictionary<TKey, TValue> ParseToCustomKV<TKey, TValue>(string src) {
			if (string.IsNullOrEmpty(src)) { return new Dictionary<TKey, TValue>(); }
			return JsonConvert.DeserializeObject<Dictionary<TKey, TValue>>(src);
		}

		/// <summary>
		/// 转换为Json字符串
		/// </summary>
		public static string ToString(object obj) {
			if (obj == null) { return ""; }
			return JsonConvert.SerializeObject(obj);
		}
	}
}