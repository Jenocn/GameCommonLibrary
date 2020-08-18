/*
	By Jenocn
	https://jenocn.github.io/
*/

using System.Collections.Generic;

namespace GCL.Pattern {
	public class SimpleNotify<TMessage> {
		private Dictionary<int, System.Action<TMessage>> _listeners = new Dictionary<int, System.Action<TMessage>>();
		public bool AddListener(object sender, System.Action<TMessage> func) {
			if (!_listeners.ContainsKey(sender.GetHashCode())) {
				_listeners.Add(sender.GetHashCode(), func);
				return true;
			}
			return false;
		}
		public bool RemoveListener(object sender) {
			return _listeners.Remove(sender.GetHashCode());
		}
		public void Send(TMessage msg) {
			foreach (var item in _listeners) {
				item.Value.Invoke(msg);
			}
		}
	};

	public class SimpleNotifyVoid {
		private Dictionary<int, System.Action> _listeners = new Dictionary<int, System.Action>();
		public bool AddListener(object sender, System.Action func) {
			if (!_listeners.ContainsKey(sender.GetHashCode())) {
				_listeners.Add(sender.GetHashCode(), func);
				return true;
			}
			return false;
		}
		public bool RemoveListener(object sender) {
			return _listeners.Remove(sender.GetHashCode());
		}
		public void Send() {
			foreach (var item in _listeners) {
				item.Value.Invoke();
			}
		}
	};
} // GCL.Pattern