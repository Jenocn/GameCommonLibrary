/*
	By Jenocn
	https://jenocn.github.io/
*/

using System.Collections.Generic;

namespace GCL {
	/// <summary>
	/// 日期类
	/// </summary>
	public class GameDate {

		private int _year = 1;
		private int _month = 1;
		private int _day = 1;

        public int year { get { return _year; } set { SetYear(value); } }
        public int month { get { return _month; } set { SetMonth(value); } }
        public int day { get { return _day; } set { SetDay(value); } }

        // 是否支持闰年
        private bool _bLeapSupport = true;
		// 今年是否是闰年
		private bool _bLeapYear = false;

		private List<int> _generalDays = null;

		/// <summary>
		/// <para>参数:是否支持闰年</para>
		/// </summary>
		public GameDate(bool bLeapSupport = true) {

			_bLeapSupport = bLeapSupport;

			_generalDays = new List<int>() {
				31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31
			};
		}

		public void SetYear(int year) {
			_year = year;
			if (_bLeapSupport) {
				_bLeapYear = ((_year % 4 == 0) && (_year % 100 != 0)) || (_year % 400 == 0);
				if (_bLeapYear) {
					_generalDays[1] = 29;
				} else {
					_generalDays[1] = 28;
					_day = System.Math.Min(_day, 28);
				}
			}
		}
		public void SetMonth(int month) {
			if (month >= 1 && month <= 12) {
				_month = month;
				_day = System.Math.Min(_day, GetMaxDayOfMonth(_month));
			}
		}
		public void SetDay(int day) {
			if (day >= 1 && day <= GetMaxDayOfMonth(_month)) {
				_day = day;
			}
		}

		public override string ToString() {
			return _year + "-" + _month + "-" + _day;
		}

		/// <summary>
		/// 获取当前月的上限天数
		/// </summary>
		public int GetMaxDayOfMonth() {
			return GetMaxDayOfMonth(_month);
		}

		/// <summary>
		/// 获取本年某个月的上限天数
		/// </summary>
		public int GetMaxDayOfMonth(int month_) {
			if (month_ <= 0 || month_ > 12) {
				return 1;
			}
			return _generalDays[month_ - 1];
		}

		/// <summary>
		/// 本年是否是闰年(只有当开启支持闰年的情况下才有效)
		/// </summary>
		public bool IsLeapYear() {
			return _bLeapYear;
		}

		/// <summary>
		/// 前进一天
		/// </summary>
		public void NextDay() {
			++_day;
			if (_day > GetMaxDayOfMonth(_month)) {
				_day = 1;
				NextMonth();
			}
		}

		/// <summary>
		/// 前进一个月
		/// <para>参数表示是否重置天数</para>
		/// </summary>
		public void NextMonth(bool clear = false) {
			++_month;
			if (_month > 12) {
				_month = 1;
				NextYear();
			}
			if (clear) {
				_day = 1;
			} else {
				_day = System.Math.Min(_day, GetMaxDayOfMonth(_month));
			}
		}

		/// <summary>
		/// 前进一年
		/// <para>参数表示是否重置天数</para>
		/// </summary>
		public void NextYear(bool clear = false) {
			++_year;
			if (clear) {
				_month = 1;
				_day = 1;
			}
			SetYear(_year);
		}
	}
}