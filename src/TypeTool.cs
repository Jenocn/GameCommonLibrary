/*
	By Jenocn
	https://jenocn.github.io/
*/

namespace GCL {

	/// <summary>
	/// 类型工具
	/// </summary>
	public static class TypeTool {

		/// <summary>
		/// 将字符串转换为int类型,失败则返回默认值
		/// </summary>
		public static int ToInt(string s, int def = 0) {
			if (!string.IsNullOrEmpty(s) && int.TryParse(s, out var ret)) {
				return ret;
			}
			return def;
		}
	
		/// <summary>
		/// 将字符串转换为long类型,失败则返回默认值
		/// </summary>
		public static long ToLong(string s, long def = 0) {
			if (!string.IsNullOrEmpty(s) && long.TryParse(s, out var ret)) {
				return ret;
			}
			return def;
		}

		/// <summary>
		/// 将字符串转换为short类型,失败则返回默认值
		/// </summary>
		public static short ToShort(string s, short def = 0) {
			if (!string.IsNullOrEmpty(s) && short.TryParse(s, out var ret)) {
				return ret;
			}
			return def;
		}

		/// <summary>
		/// 将字符串转换为uint类型,失败则返回默认值
		/// </summary>
		public static uint ToUInt(string s, uint def = 0) {
			if (!string.IsNullOrEmpty(s) && uint.TryParse(s, out var ret)) {
				return ret;
			}
			return def;
		}

		/// <summary>
		/// 将字符串转换为float类型,失败则返回默认值
		/// </summary>
		public static float ToFloat(string s, float def = 0) {
			if (!string.IsNullOrEmpty(s) && float.TryParse(s, out var ret)) {
				return ret;
			}
			return def;
		}

		/// <summary>
		/// 将字符串转换为double类型,失败则返回默认值
		/// </summary>
		public static double ToDouble(string s, double def = 0) {
			if (!string.IsNullOrEmpty(s) && double.TryParse(s, out var ret)) {
				return ret;
			}
			return def;
		}

		/// <summary>
		/// 将字符串转换为bool类型,失败则返回默认值
		/// </summary>
		public static bool ToBool(string s, bool def = false) {
			if (!string.IsNullOrEmpty(s) && bool.TryParse(s, out var ret)) {
				return ret;
			}
			return def;
		}
	}
}