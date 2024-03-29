using System.Collections.Generic;

namespace GCL {

	/// <summary>
	/// 路径工具
	/// </summary>
	public static class PathTool {

		/// <summary>
		/// 连接多个路径
		/// </summary>
		public static string Join(params string[] dirs) {
			return System.IO.Path.Combine(dirs).Replace('\\', '/');
		}

		/// <summary>
		/// 组合一组路径,并且标准化输出
		/// </summary>
		public static string NormalizeJoin(params string[] paths) {
			string ret = "";
			foreach (var item in paths) {
				var path = Normalize(item);
				if (string.IsNullOrEmpty(path)) {
					continue;
				}
				if (string.IsNullOrEmpty(ret)) {
					ret = path;
					continue;
				}
				if (!_IsSepartor(path[0])) {
					ret += '/';
				}
				ret += path;
			}
			return Normalize(ret);
		}

		/// <summary>
		/// 标准化一个路径,如果路径不存在则返回空
		/// </summary>
		public static string Normalize(string path) {
			if (string.IsNullOrEmpty(path)) { return ""; }
			bool bFirstSepartor = _IsSepartor(path[0]);
			if (bFirstSepartor) {
				path = path.Substring(1, path.Length - 1);
			}

			var arr = path.Split('/', '\\');

			var tempStack = new Stack<string>();
			foreach (var item in arr) {
				if (item == "..") {
					if (tempStack.Count > 0) {
						var peekItem = tempStack.Peek();
						if (peekItem != "..") {
							tempStack.Pop();
							continue;
						}
					} else {
						if (bFirstSepartor) {
							return "";
						}
					}
				}
				tempStack.Push(item);
			}

			string ret = "";
			while (tempStack.Count > 0) {
				var item = tempStack.Pop();
				if (string.IsNullOrEmpty(ret)) {
					ret = item;
				} else {
					if (string.IsNullOrEmpty(item)) {
						return "";
					}
					ret = item + "/" + ret;
				}
			}

			if (bFirstSepartor) {
				ret = '/' + ret;
			}

			return ret;
		}

		public static string Extname(string filename) {
			var pos = filename.LastIndexOf('.');
			if (pos != -1) {
				return filename.Substring(pos);
			}
			return "";
		}

		private static bool _IsSepartor(char ch) {
			return ch == '/' || ch == '\\';
		}

	}
}