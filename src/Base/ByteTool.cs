/*
	By Jenocn
	https://jenocn.github.io/
*/

using System.Linq;
using System.Text;

namespace GCL.Base {

	/// <summary>
	/// 编码类型
	/// </summary>
	public enum EncodingType {
		Default = 0,
		UTF8,
		GBK,
		GB18030,
		GB2312,
		ASCII,
	}

	/// <summary>
	/// Byte工具
	/// </summary>
	public static class ByteTool {

		/// <summary>
		/// 拼接两个byte数据
		/// </summary>
		public static byte[] Concat(byte[] a, byte[] b) {
			var ret = new byte[a.Length + b.Length];
			a.CopyTo(ret, 0);
			b.CopyTo(ret, a.Length);
			return ret;
		}

		/// <summary>
		/// 截取data数据的start到end部分
		/// </summary>
		public static byte[] Cut(byte[] data, int start, int end) {
			var dataSize = data.Length;
			if (start >= dataSize) { return null; }
			if (end > dataSize) { return null; }
			return data.Skip(start).Take(end - start).ToArray();
		}

		/// <summary>
		/// <para>压包整个数据的大小到包头</para>
		/// <para>在数据头加入此数据的size大小</para>
		/// </summary>
		public static byte[] PackSizeToHead(byte[] data) {
			return Pack(data, data.Length + 4);
		}

		/// <summary>
		/// <para>压包一个整型数值到包头</para>
		/// <para>在data数据头插入一个int数值</para>
		/// </summary>
		public static byte[] Pack(byte[] data, int num) {
			var head = System.BitConverter.GetBytes(num);
			if (data == null) {
				return head;
			}
			return Concat(head, data);
		}

		/// <summary>
		/// <para>解包方法</para>
		/// <para>从data数据中截取前4个字节的int数值和之后的所有数据</para>
		/// </summary>
		public static System.Tuple<int, byte[]> UnpackFromHead(byte[] data) {
			if (data == null || data.Length < 4) {
				return null;
			}
			var key = System.BitConverter.ToInt32(data, 0);
			var retData = Cut(data, 4, data.Length);
			return new System.Tuple<int, byte[]>(key, retData);
		}

		/// <summary>
		/// <para>将字符串转换为byte[]</para>
		/// </summary>
		public static byte[] GetBytes(string s, EncodingType encoding = EncodingType.UTF8) {
			switch (encoding) {
			case EncodingType.Default:
				return Encoding.Default.GetBytes(s);
			case EncodingType.GB18030:
				return Encoding.GetEncoding("GB18030").GetBytes(s);
			case EncodingType.GB2312:
				return Encoding.GetEncoding("GB2312").GetBytes(s);
			case EncodingType.GBK:
				return Encoding.GetEncoding("GBK").GetBytes(s);
			case EncodingType.ASCII:
				return Encoding.ASCII.GetBytes(s);
			}
			return Encoding.UTF8.GetBytes(s);
		}
	}
}