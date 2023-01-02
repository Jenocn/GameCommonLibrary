/*
	By Jenocn
	https://jenocn.github.io/
*/

using System.Collections.Generic;

namespace GCL {
	public class SimpleNotify<TMessage> {
		private Dictionary<object, System.Action<TMessage>> _listeners = new Dictionary<object, System.Action<TMessage>>();
		public bool AddListener(object sender, System.Action<TMessage> func) {
			if (!_listeners.ContainsKey(sender)) {
				_listeners.Add(sender, func);
				return true;
			}
			return false;
		}
		public bool RemoveListener(object sender) {
			return _listeners.Remove(sender);
		}
		public void RemoveAllListeners() {
			_listeners.Clear();
		}
		public void Send(TMessage msg) {
			foreach (var item in _listeners) {
				item.Value.Invoke(msg);
			}
		}
	};

	public class SimpleNotifyVoid {
		private Dictionary<object, System.Action> _listeners = new Dictionary<object, System.Action>();
		public bool AddListener(object sender, System.Action func) {
			if (!_listeners.ContainsKey(sender)) {
				_listeners.Add(sender, func);
				return true;
			}
			return false;
		}
		public bool RemoveListener(object sender) {
			return _listeners.Remove(sender);
		}
		public void RemoveAllListeners() {
			_listeners.Clear();
		}
		public void Send() {
			foreach (var item in _listeners) {
				item.Value.Invoke();
			}
		}
	};
} // GCL