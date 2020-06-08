/*
	By Jenocn
	https://jenocn.github.io/
*/

namespace GCL.Serialization {
	/// <summary>
	/// 数据对象
	/// </summary>
	public class DataObject {
		public static implicit operator bool(DataObject exists) {
			return exists != null;
		}
	}

	/// <summary>
	/// 可克隆的对象
	/// </summary>
	public class CloneableData<T> : DataObject where T : CloneableData<T> {

		public T Clone() {
			return CloneTool.Clone(this as T);
		}
	}
}