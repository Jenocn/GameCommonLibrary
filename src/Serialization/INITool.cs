/*
	By Jenocn
	https://jenocn.github.io/
*/

using System.Collections.Generic;
using System.Text;

namespace GCL {

	/// <summary>
	/// INI工具类
	/// </summary>
	public static class INITool {

		private static readonly char[] TRIM_WORD = { ' ', '\t', '\r', '\n' };
		private static readonly char[] COMMENTS = { ';', '#' };
		private static readonly string sCOMMENTS = string.Concat(COMMENTS);
		private static char KV_SPLIT = '=';

		/// <summary>
		/// 解析配置到INI读取器中
		/// </summary>
		public static INIReader ParseTo(string src) {
			return new INIReader(Parse(src));
		}

		/// <summary>
		/// 将INI读取器中的配置数据转为字符串
		/// </summary>
		public static string ToString(INIReader reader) {
			return ToString(reader.prototype);
		}

		/// <summary>
		/// 解析配置到Dictionary中
		/// </summary>
		public static Dictionary<string, Dictionary<string, string>> Parse(string src) {
			if (string.IsNullOrEmpty(src)) { return new Dictionary<string, Dictionary<string, string>>(); }
			Dictionary<string, string> curHeadDict = null;
			var retDict = new Dictionary<string, Dictionary<string, string>>();
			var lineStrArray = src.Split('\n');
			for (int i = 0; i < lineStrArray.Length; ++i) {
				string lineStr = lineStrArray[i];
				lineStr = lineStr.Trim(TRIM_WORD);
				var length = lineStr.Length;
				if (length < 3) { continue; }
				if (sCOMMENTS.IndexOf(lineStr[0]) >= 0) {
					continue;
				}
				var headStr = _ParseHead(lineStr);
				if (headStr != "") {
					if (!retDict.TryGetValue(headStr, out curHeadDict)) {
						curHeadDict = new Dictionary<string, string>();
						retDict.Add(headStr, curHeadDict);
					}
					continue;
				}
				if (curHeadDict == null) {
					continue;
				}
				int kvIndex = lineStr.IndexOf(KV_SPLIT);
				if (kvIndex < 1 || kvIndex == length - 1) {
					continue;
				}
				var key = lineStr.Substring(0, kvIndex);
				if (key == "") {
					continue;
				}
				string value = lineStr.Substring(kvIndex + 1);
				if (value == "") {
					continue;
				}
				int findNoteIdx = value.IndexOfAny(COMMENTS);
				if (findNoteIdx == 0) {
					continue;
				}
				if (findNoteIdx > 0) {
					value = value.Substring(0, findNoteIdx);
				}
				curHeadDict.Add(key.TrimEnd(TRIM_WORD), value.Trim(TRIM_WORD));
			}

			return retDict;
		}

		/// <summary>
		/// 从Dictionary中转换为INI字符串
		/// </summary>
		public static string ToString(Dictionary<string, Dictionary<string, string>> data) {
			if (data.Count == 0) { return ""; }
			var stringBuilder = new StringBuilder(data.Count * 4);
			foreach (var item in data) {
				stringBuilder.Append("[");
				stringBuilder.Append(item.Key);
				stringBuilder.Append("]\n");
				foreach (var content in item.Value) {
					stringBuilder.Append(content.Key);
					stringBuilder.Append("=");
					stringBuilder.Append(content.Value);
					stringBuilder.Append("\n");
				}
			}
			return stringBuilder.ToString();
		}

		private static string _ParseHead(string str) {
			int len = str.Length;
			if (str[0] != '[' && str[len - 1] != ']') {
				return "";
			}
			return str.Substring(1, len - 2);
		}
	}
}