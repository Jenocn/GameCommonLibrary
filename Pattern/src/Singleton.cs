/*
	By Jenocn
	https://jenocn.github.io/
*/

namespace GCL.Pattern {

	/// <summary>
	/// 单例
	/// </summary>
	public class Singleton<T> where T : class, new() {
		public static T instance { get; private set; }
		static Singleton() {
			instance = new T();
		}
		public static T GetInstance() {
			return instance;
		}
	}
}