/*
	By Jenocn
	https://jenocn.github.io/
*/

using System.Collections.Generic;

namespace GCL {

	/// <summary>
	/// 表容器
	/// </summary>
	public sealed class TableContainer {
		private List<ITable> _tableList = null;
		public TableContainer() {
			_tableList = new List<ITable>(4);
		}

		/// <summary>
		/// 添加一个表
		/// </summary>
		public void Push(ITable table) {
			if (table == null) { return; }
			_tableList.Add(table);
		}

		/// <summary>
		/// 所有表加载数据
		/// </summary>
		public void Load() {
			foreach (var item in _tableList) {
				item.Load();
			}
		}

		/// <summary>
		/// 所有表重新加载
		/// </summary>
		public void Reload() {
			foreach (var item in _tableList) {
				item.Reload();
			}
		}

		/// <summary>
		/// 所有表清空数据
		/// </summary>
		public void Clear() {
			foreach (var item in _tableList) {
				item.Clear();
			}
			_tableList.Clear();
		}
	}
}