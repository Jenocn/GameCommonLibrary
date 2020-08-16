/*
	By Jenocn
	https://jenocn.github.io/
*/

using System.Collections.Generic;

namespace GCL.Base {
	/// <summary>
	/// <para>支持参数的字符串</para>
	/// <para>写法:参数用"{{ }}"双大括号括住</para>
	/// <para>通过 SetString 方法设置源字符串</para>
	/// <para>通过 SetParam 方法设置参数</para>
	/// <para>通过 ToString 方法获得最终结果</para>
	/// </summary>
	public class ParamString {

		private static readonly string PARAM_SIGN_L = "{{";
		private static readonly string PARAM_SIGN_R = "}}";
		private static readonly int PARAM_SIGN_L_LEN = PARAM_SIGN_L.Length;
		private static readonly int PARAM_SIGN_R_LEN = PARAM_SIGN_R.Length;

		private string _src = "";
		private string _out = "";
		private Dictionary<string, string> _paramDict = new Dictionary<string, string>();
		private bool _bDirty = false;

		public ParamString() {}

		public ParamString(string src) {
			_src = src;
			_out = _src;
		}

		/// <summary>
		/// 设置文本内容
		/// </summary>
		public ParamString SetString(string src) {
			_src = src;
			_out = _src;
			_bDirty = true;
			return this;
		}

		/// <summary>
		/// 获取源字符串
		/// </summary>
		public string GetSrc() {
			return _src;
		}

		/// <summary>
		/// 设置参数替换的内容
		/// </summary>
		public ParamString SetParam(string key, object value) {
			if (value != null) {
				_paramDict[key] = value.ToString();
				_bDirty = true;
			}
			return this;
		}

		/// <summary>
		/// 获得已设置的参数内容
		/// </summary>
		public object GetParam(string key) {
			if (_paramDict.TryGetValue(key, out var value)) {
				return value;
			}
			return null;
		}

		/// <summary>
		/// 获得最终字符串
		/// </summary>
		public override string ToString() {
			if (_bDirty) {
				_bDirty = false;
				_out = _Parse(_src);
			}
			return _out;
		}

		private string _Parse(string src) {
			if (src.Length < PARAM_SIGN_L_LEN + PARAM_SIGN_R_LEN) {
				return src;
			}

			string ret = src;
			int count = 0;

			int indexR = ret.IndexOf(PARAM_SIGN_R);
			while (indexR != -1) {
				var indexL = ret.LastIndexOf(PARAM_SIGN_L, indexR);
				if (indexL == -1) {
					indexR += PARAM_SIGN_R_LEN;
				} else {
					var key = ret.Substring(indexL + PARAM_SIGN_L_LEN, indexR - indexL - PARAM_SIGN_L_LEN);
					if (_paramDict.TryGetValue(key.Trim(), out var value)) {
						string oldValue = PARAM_SIGN_L + key + PARAM_SIGN_R;
						if (oldValue == value) {
							indexR += PARAM_SIGN_R_LEN;
						} else {
							++count;
							ret = ret.Replace(oldValue, value);
							indexR = indexL + value.Length;
						}
					} else {
						indexR += PARAM_SIGN_R_LEN;
					}
				}

				if (indexR >= ret.Length) {
					break;
				}

				indexR = ret.IndexOf(PARAM_SIGN_R, indexR);
			}

			if (count > 0) {
				ret = _Parse(ret);
			}

			return ret;
		}
	}
}