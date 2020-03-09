/*
	By Jenocn
	https://jenocn.github.io/
*/

using System.Collections.Generic;
using GCL.Base;

namespace GCL.Serialization {

	/// <summary>
	/// INI配置读取器
	/// </summary>
	public class INIReader {
		public Dictionary<string, Dictionary<string, string>> prototype { get; private set; }

		public INIReader() {
			prototype = new Dictionary<string, Dictionary<string, string>>();
		}

		public INIReader(Dictionary<string, Dictionary<string, string>> prototype) {
			this.prototype = prototype;
		}

		/// <summary>
		/// 获取指定session下的key配置
		/// </summary>
		public string GetString(string session, string key, string defaultValue = "") {
			string ret = defaultValue;
			Dictionary<string, string> sessionDict = null;
			if (prototype.TryGetValue(session, out sessionDict)) {
				if (sessionDict.TryGetValue(key, out ret)) {
					return ret;
				}
			}
			return defaultValue;
		}

		public double GetDouble(string session, string key, double defaultValue = 0) {
			return TypeTool.ToDouble(GetString(session, key, ""), defaultValue);
		}

		public float GetFloat(string session, string key, float defaultValue = 0) {
			return TypeTool.ToFloat(GetString(session, key, ""), defaultValue);
		}

		public int GetInt(string session, string key, int defaultValue = 0) {
			return TypeTool.ToInt(GetString(session, key, ""), defaultValue);
		}

		public long GetLong(string session, string key, long defaultValue = 0) {
			return TypeTool.ToLong(GetString(session, key, ""), defaultValue);
		}

		public bool GetBool(string session, string key, bool defaultValue = false) {
			return TypeTool.ToBool(GetString(session, key, ""), defaultValue);
		}

		/// <summary>
		/// 添加或修改配置
		/// </summary>
		public void Set(string session, string key, object value) {
			Dictionary<string, string> sessionDict = null;
			if (!prototype.TryGetValue(session, out sessionDict)) {
				sessionDict = new Dictionary<string, string>();
			}
			sessionDict[key] = value.ToString();
		}
	}
}