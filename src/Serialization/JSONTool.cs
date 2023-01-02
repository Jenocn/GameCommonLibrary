/*
	By Jenocn
	https://jenocn.github.io/
*/

using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GCL {

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
		/// 转换为JToken
		/// </summary>
		public static JToken ParseToToken(string src) {
			if (string.IsNullOrEmpty(src)) {
				return null;
			}
			return JToken.Parse(src);
		}

		/// <summary>
		/// 转换为Json字符串
		/// </summary>
		public static string ToString(object obj) {
			if (obj == null) { return ""; }
			return JsonConvert.SerializeObject(obj);
		}
	}

	public static class JTokenHelper {

		private static bool _RefToken(JToken root, string key, out JToken token) {
			if (root.Type == JTokenType.Object) {
				if ((root as JObject).TryGetValue(key, out token)) {
					return true;
				}
			}
			token = null;
			return false;
		}

		private static bool _RefToken(JToken root, int index, out JToken token) {
			if (root.Type == JTokenType.Array) {
				var jarray = root as JArray;
				if ((index >= 0) && (index < jarray.Count)) {
					token = jarray[index];
					return true;
				}
			}
			token = null;
			return false;
		}

		public static JToken GetToken(JToken root, string key) {
			_RefToken(root, key, out var ret);
			return ret;
		}

		public static JToken GetToken(JToken root, int index) {
			_RefToken(root, index, out var ret);
			return ret;
		}

		public static int GetInt(JToken root, string key, int def) {
			if (_RefToken(root, key, out var token)) {
				if (token.Type == JTokenType.Integer) {
					return token.Value<int>();
				}
			}
			return def;
		}
		public static long GetLong(JToken root, string key, long def) {
			if (_RefToken(root, key, out var token)) {
				if (token.Type == JTokenType.Integer) {
					return token.Value<long>();
				}
			}
			return def;
		}
		public static float GetFloat(JToken root, string key, float def) {
			if (_RefToken(root, key, out var token)) {
				if (token.Type == JTokenType.Float) {
					return token.Value<float>();
				} else if (token.Type == JTokenType.Integer) {
					return (float) token.Value<int>();
				}
			}
			return def;
		}
		public static string GetString(JToken root, string key, string def) {
			if (_RefToken(root, key, out var token)) {
				if (token.Type == JTokenType.String) {
					return token.Value<string>();
				}
			}
			return def;
		}
		public static bool GetBool(JToken root, string key, bool def) {
			if (_RefToken(root, key, out var token)) {
				if (token.Type == JTokenType.Boolean) {
					return token.Value<bool>();
				}
			}
			return def;
		}
		public static T Get<T>(JToken root, string key, T def) {
			if (_RefToken(root, key, out var token)) {
				if (token.HasValues) {
					return token.Value<T>();
				}
			}
			return def;
		}

		public static int GetInt(JToken root, int index, int def) {
			if (_RefToken(root, index, out var token)) {
				if (token.Type == JTokenType.Integer) {
					return token.Value<int>();
				}
			}
			return def;
		}
		public static long GetLong(JToken root, int index, long def) {
			if (_RefToken(root, index, out var token)) {
				if (token.Type == JTokenType.Integer) {
					return token.Value<long>();
				}
			}
			return def;
		}
		public static float GetFloat(JToken root, int index, float def) {
			if (_RefToken(root, index, out var token)) {
				if (token.Type == JTokenType.Float) {
					return token.Value<float>();
				} else if (token.Type == JTokenType.Integer) {
					return (float) token.Value<int>();
				}
			}
			return def;
		}
		public static string GetString(JToken root, int index, string def) {
			if (_RefToken(root, index, out var token)) {
				if (token.Type == JTokenType.String) {
					return token.Value<string>();
				}
			}
			return def;
		}
		public static bool GetBool(JToken root, int index, bool def) {
			if (_RefToken(root, index, out var token)) {
				if (token.Type == JTokenType.Boolean) {
					return token.Value<bool>();
				}
			}
			return def;
		}
		public static T Get<T>(JToken root, int index, T def) {
			if (_RefToken(root, index, out var token)) {
				if (token.HasValues) {
					return token.Value<T>();
				}
			}
			return def;
		}

		public static bool ListArrayValue(JToken root, string key, System.Action<JToken> itemFunc) {
			if (_RefToken(root, key, out var token)) {
				return ListArrayValue(token, itemFunc);
			}
			return false;
		}
		public static bool ListArrayValue(JToken root, System.Action<JToken> itemFunc) {
			foreach (var item in root) {
				itemFunc.Invoke(item);
			}
			return true;
		}
	}
}