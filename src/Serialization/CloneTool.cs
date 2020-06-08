/*
	By Jenocn
	https://jenocn.github.io/
*/

using Newtonsoft.Json;

namespace GCL.Serialization {

	/// <summary>
	/// 克隆工具
	/// </summary>
	public static class CloneTool {

		/// <summary>
		/// 克隆对象
		/// </summary>
		public static T Clone<T>(T obj) where T : class {
			if (obj == null) { return null; }
			return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(obj));
		}
	}
}