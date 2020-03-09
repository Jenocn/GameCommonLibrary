/*
	By Jenocn
	https://jenocn.github.io/
*/

namespace GCL.Base {
	/// <summary>
	/// 随机数工具
	/// </summary>
	public static class RandomTool {

		/// <summary>
		/// 包括min但不包括max
		/// </summary>
		public static int Range(int min, int max) {
			return new System.Random(System.Guid.NewGuid().GetHashCode()).Next(min, max);
		}

		/// <summary>
		/// <para>从min和max中命中value这个数,命中返回true,否则false</para>
		/// <para>包括min但不包括max</para>
		/// </summary>
		public static bool Hit(int min, int max, int value) {
			return value == Range(min, max);
		}

		/// <summary>
		/// <para>从min和max中命中minValue~maxValue这个范围,命中返回true,否则false</para>
		/// <para>包括min但不包括max,包括minValue但不包括maxValue</para>
		/// </summary>
		public static bool Hit(int min, int max, int minValue, int maxValue) {
			int value = Range(min, max);
			return value >= minValue && value < maxValue;
		}

		/// <summary>
		/// 判断是否命中概率,0~1
		/// </summary>
		public static bool HitWithPercent(float percent) {
			if (percent >= 1) { return true; }
			if (percent <= 0) { return false; }
			int by = percent.ToString().Length - 2;
			int max = (int) System.Math.Pow(10, by);
			var rand = Range(0, max);
			var count = percent * max;
			return rand >= 0 && rand < count;
		}
	}
}