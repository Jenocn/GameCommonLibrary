/*
	By Jenocn
	https://jenocn.github.io/
*/

namespace GCL {
	public static class MathTool {
		// PI
		public const float PI = 3.14159265f;
		public const float PIx2 = PI * 2.0f;
		// 弧度/每角度 (PI / 180)
		public const float RADIAN_PER_ANGLE = PI / 180.0f;
		// 角度/每弧度 (180 / PI)
		public const float ANGLE_PER_RADIAN = 180.0f / PI;

		/// <summary>
		/// 将角度限定在0～359度之间
		/// </summary>
		public static float NormalizeAngle(float angle) {
			if (angle >= 0 && angle < 360.0f) {
				return angle;
			}
			var ret = angle % 360;
			if (ret < 0) {
				ret = 360 + ret;
			}
			return ret;
		}

		/// <summary>
		/// 将弧度限定在0~2PI(0~360)之间
		/// </summary>
		public static float NormalizeAngle_Radain(float radain) {
			return NormalizeAngle(radain * ANGLE_PER_RADIAN) * RADIAN_PER_ANGLE;
		}

		/// <summary>
		/// 将角度限定在指定范围-180~180之间
		/// </summary>
		public static float NormalizeAngle180PN(float angle) {
			var ret = NormalizeAngle(angle);
			if (ret > 180.0f) {
				ret -= 360.0f;
			}
			return ret;
		}

		/// <summary>
		/// 将弧度限定在指定范围-180~180之间
		/// </summary>
		public static float NormalizeAngle180PN_Radain(float radain) {
			var ret = NormalizeAngle_Radain(radain);
			if (ret > PI) {
				ret -= PIx2;
			}
			return ret;
		}		
		
		/// <summary>
		/// 是否经过某个角度,返回经过的次数
		/// </summary>
		/// <param name="angle">当前角度</param>
		/// <param name="step">变化的角度</param>
		/// <param name="stamp">角度戳</param>
		public static int CountCrossAngle(float angle, float step, float stamp) {
			if (step == 0) {
				return 0;
			}
			int count = 0;
			if (System.Math.Abs(step) >= 360) {
				count = (int)(step / 360);
				step %= 360;
			}
			var last = NormalizeAngle(angle - step);
			stamp = NormalizeAngle(stamp);

			if (step > 0) {
				if (last < stamp) {
					if (last + step >= stamp) {
						++count;
					}
				} else {
					if (last + step >= stamp + 360) {
						++count;
					}
				}
			} else {
				if (last > stamp) {
					if (last + step <= stamp) {
						++count;
					}
				} else {
					if (last + step <= stamp - 360) {
						++count;
					}
				}
			}
			return count;
		}

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