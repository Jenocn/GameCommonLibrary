/*
	By Jenocn
	https://jenocn.github.io/
*/

using System.Collections.Generic;
using System.Net.Sockets;
using GCL.Base;

namespace GCL.Net {
	/// <summary>
	/// TCP连接类
	/// </summary>
	public class TCPConnect {

		private string _host = "127.0.0.1";
		private int _port = 80;

		private TcpClient _tcpClient = null;

		private byte[] _preBuffer = null;
		private byte[] _buffer = new byte[1024];

		private Dictionary<object, System.Action<byte[]>> _lis = new Dictionary<object, System.Action<byte[]>>();

		/// <summary>
		/// 地址和端口号
		/// </summary>
		public TCPConnect(string host, int port) {
			_host = host;
			_port = port;
		}

		/// <summary>
		/// 连接到服务器
		/// </summary>
		public bool Connect() {
			if (_tcpClient != null) { return false; }
			_tcpClient = new TcpClient();
			_tcpClient.Connect(_host, _port);
			if (!_tcpClient.Connected) {
				return false;
			}
			var stream = _tcpClient.GetStream();
			stream.BeginRead(_buffer, 0, 1024, _OnReadResult, null);
			return true;
		}

		private void _OnReadResult(System.IAsyncResult ar) {
			if (_tcpClient == null || !_tcpClient.Connected) { return; }
			var stream = _tcpClient.GetStream();
			int index = stream.EndRead(ar);
			_Unpack(ByteTool.Cut(_buffer, 0, index));
			stream.BeginRead(_buffer, 0, 1024, _OnReadResult, null);
		}

		/// <summary>
		/// 断开连接
		/// </summary>
		public void Disconnect() {
			_tcpClient.Close();
			_tcpClient = null;
		}

		/// <summary>
		/// 添加消息监听者
		/// </summary>
		public void AddListener(object sender, System.Action<byte[]> lis) {
			_lis.Add(sender, lis);
		}

		/// <summary>
		/// 删除监听者
		/// </summary>
		public void RemoveListener(object sender, System.Action<byte[]> lis) {
			_lis.Remove(sender);
		}

		/// <summary>
		/// 发送数据
		/// </summary>
		public void Send(byte[] data) {
			if (_tcpClient == null || !_tcpClient.Connected) { return; }
			var stream = _tcpClient.GetStream();
			var sendData = _Pack(data);
			stream.Write(sendData, 0, sendData.Length);
		}

		private void _DealData(byte[] data) {
			foreach (var item in _lis) {
				item.Value.Invoke(data);
			}
		}

		private byte[] _Pack(byte[] data) {
			if (data == null || data.Length == 0) {
				return null;
			}
			return ByteTool.PackSizeToHead(data);
		}

		private void _Unpack(byte[] data) {
			if (_preBuffer == null) {
				_preBuffer = data;
			} else {
				_preBuffer = ByteTool.Concat(_preBuffer, data);
			}
			var head = ByteTool.Cut(_preBuffer, 0, 4);
			if (head == null) { return; }
			var size = System.BitConverter.ToInt32(head, 0);
			var body = ByteTool.Cut(_preBuffer, 4, size);
			if (body == null) { return; }
			_DealData(body);
			var more = ByteTool.Cut(_preBuffer, size, _preBuffer.Length);
			_preBuffer = null;
			if (more != null) {
				_Unpack(more);
			}
		}
	}
}