/*
	By Jenocn
	https://jenocn.github.io/
*/

using System.Collections.Generic;
using GCL.Pattern;

namespace GCL.Serialization {

	/// <summary>
	/// 表接口
	/// </summary>
	public interface ITable {
		void Load();
		void Reload();
		void Clear();
	}

	/// <summary>
	/// 表基类
	/// </summary>
	/// <typeparam name="TClass">表类型</typeparam>
	/// <typeparam name="TKey">键类型</typeparam>
	/// <typeparam name="TElement">元素类型</typeparam>
	public abstract class TableBase<TClass, TKey, TElement> : Singleton<TClass>, ITable where TClass : class, new() where TElement : class {

		private Dictionary<TKey, TElement> _dataDict = new Dictionary<TKey, TElement>();

		public abstract void Load();

		public virtual void Reload() {
			Clear();
			Load();
		}

		public virtual void Clear() {
			_dataDict.Clear();
		}

		/// <summary>
		/// 获取元素
		/// </summary>
		public virtual TElement GetElement(TKey key) {
			TElement value = null;
			_dataDict.TryGetValue(key, out value);
			return value;
		}

		/// <summary>
		/// 获取迭代器
		/// </summary>
		public virtual Dictionary<TKey, TElement>.Enumerator GetEnumerator() {
			return _dataDict.GetEnumerator();
		}

		/// <summary>
		/// 表中数据个数
		/// </summary>
		public virtual int Size() {
			return _dataDict.Count;
		}

		/// <summary>
		/// 插入数据到表中
		/// </summary>
		protected virtual void Emplace(TKey key, TElement element) {
			_dataDict.Add(key, element);
		}

		/// <summary>
		/// 直接给表元数据字典赋值
		/// </summary>
		protected virtual void Assign(Dictionary<TKey, TElement> dict) {
			_dataDict = dict;
		}
	}
}