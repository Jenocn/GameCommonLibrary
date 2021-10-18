namespace GCL.Base {
	public static class Utility {
		public static void Swap<T>(ref T left, ref T right) {
			T temp = left;
			left = right;
			right = temp;
		}

		/// <summary>
		/// return 1 when value is more than 0
		/// return -1 when value is less than 0
		/// return 0 when value is equal 0
		/// </summary>
		public static int PositiveNegativeNumber(int value) {
			if (value > 0) return 1;
			else if (value < 0) return -1;
			return 0;
		}
		/// <summary>
		/// return 1 when value is more than 0
		/// return -1 when value is less than 0
		/// return 0 when value is equal 0
		/// </summary>
		public static int PositiveNegativeNumber(float value) {
			if (value > 0) return 1;
			else if (value < 0) return -1;
			return 0;
		}
	}
}