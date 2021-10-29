/*
	By Jenocn
	https://jenocn.github.io/
*/

using System.Collections.Generic;

namespace GCL.Pattern {

	/// <summary>
	/// 消息派发器
	/// </summary>
	public class MessageDispatcher {
		private Dictionary<string, IMessageListener> _listenerDict = new Dictionary<string, IMessageListener>();
		private Dictionary<bool, LinkedList<IMessage>> _messageQueue = new Dictionary<bool, LinkedList<IMessage>>();

		private LinkedList<System.Tuple<bool, string, IMessageListener>> _safeQueue = new LinkedList<System.Tuple<bool, string, IMessageListener>>();
		private bool _bSafeMode = false;
		private bool _activeQueueSign = false;

		public MessageDispatcher() {
			_messageQueue[true] = new LinkedList<IMessage>();
			_messageQueue[false] = new LinkedList<IMessage>();
		}

		/// <summary>
		/// 添加消息监听者
		/// </summary>
		public void AddListener<T>(object obj, System.Action<T> func) where T : MessageBase<T> {
			if (func != null) {
				var key = _GetKey<T>(obj);
				if (!_listenerDict.ContainsKey(key)) {
					var listener = new MessageListener<T>(func);
					if (_bSafeMode) {
						_safeQueue.AddLast(new System.Tuple<bool, string, IMessageListener>(true, key, listener));
					} else {
						_listenerDict.Add(key, listener);
					}
				}
			}
		}

		/// <summary>
		/// 删除监听者
		/// </summary>
		public void RemoveListener<T>(object obj) where T : MessageBase<T> {
			var key = _GetKey<T>(obj);
			if (_bSafeMode) {
				if (_listenerDict.ContainsKey(key)) {
					_safeQueue.AddLast(new System.Tuple<bool, string, IMessageListener>(false, key, null));
				}
			} else {
				_listenerDict.Remove(key);
			}
		}

		/// <summary>
		/// 立即发送消息
		/// </summary>
		public void Send(IMessage message) {
			if (message == null) { return; }
			var messageID = message.GetMessageID();
			_bSafeMode = true;
			foreach (var item in _listenerDict) {
				if (item.Value.GetMessageID() == messageID) {
					item.Value.Invoke(message);
				}
			}
			_bSafeMode = false;
			_ExecuteQueue();
		}

		/// <summary>
		/// 入队消息,待OnDispathch时发送
		/// </summary>
		public void Push(IMessage message) {
			if (message == null) {
				return;
			}
			_messageQueue[!_activeQueueSign].AddLast(message);
		}

		/// <summary>
		/// 派发队列中的消息
		/// </summary>
		public void OnDispatch() {
			_activeQueueSign = !_activeQueueSign;
			foreach (var message in _messageQueue[_activeQueueSign]) {
				Send(message);
			}
			_messageQueue[_activeQueueSign].Clear();
		}

		/// <summary>
		/// 清空消息和监听者
		/// </summary>
		public void Clear() {
			_listenerDict.Clear();
			_messageQueue[true].Clear();
			_messageQueue[false].Clear();
		}

		private void _ExecuteQueue() {
			var p = _safeQueue.First;
			while (p != null) {
				var data = p.Value;
				if (data.Item1) {
					if (!_listenerDict.ContainsKey(data.Item2)) {
						_listenerDict.Add(data.Item2, data.Item3);
					}
				} else {
					_listenerDict.Remove(data.Item2);
				}
				p = p.Next;
			}
			_safeQueue.Clear();
		}

		private string _GetKey<T>(object obj) where T : MessageBase<T> {
			return typeof(T).ToString() + "_" + obj.GetHashCode();
		}
	}
}