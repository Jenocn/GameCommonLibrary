/*
	By Jenocn
	https://jenocn.github.io/
*/

namespace GCL {
	public static class MathTool {
		// PI
		public const float PI = 3.141593f;
		// 弧度/每角度 (PI / 180)
		public const float RADIAN_PER_ANGLE = PI / 180.0f;
		// 角度/每弧度 (180 / PI)
		public const float ANGLE_PER_RADIAN = 180.0f / PI;

		// 贝塞尔曲线获取目标点
		public static float BezierAt(float start, float control0, float control1, float end, float timePercent) {
			return System.Convert.ToSingle(System.Math.Pow(1 - timePercent, 3) * start +
				3 * timePercent * (System.Math.Pow(1 - timePercent, 2)) * control0 +
				3 * System.Math.Pow(timePercent, 2) * (1 - timePercent) * control1 +
				System.Math.Pow(timePercent, 3) * end);
		}
		/// <summary>
		/// 贝塞尔曲线获取长度
		/// </summary>
		/// <param name="precision">精准度/步长(1~10000)</param>
		/// <returns></returns>
		public static float BezierLength(float startx, float starty, float cx0, float cy0, float cx1, float cy1, float endx, float endy, uint precision) {
			precision = System.Math.Min(10000, System.Math.Max(1, precision));
			float dt = 1.0f / precision;
			float ct = 0;
			float sx = 0;
			float sy = 0;

			cx0 -= startx;
			cy0 -= starty;
			cx1 -= startx;
			cy1 -= starty;
			endx -= startx;
			endy -= starty;

			float length = 0;
			for (int i = 0; i < (int) precision; ++i) {
				float tx = BezierAt(0, cx0, cx1, endx, ct);
				float ty = BezierAt(0, cy0, cy1, endy, ct);

				float x = tx - sx;
				float y = ty - sy;

				length += System.Convert.ToSingle(System.Math.Sqrt(x * x + y * y));

				sx = tx;
				sy = ty;

				ct += dt;
			}

			return length;
		}
	}
}