/*
	By Jenocn
	https://jenocn.github.io/
*/

using System.Collections.Generic;

namespace GCL.Pattern {
	public class SimpleNotify<TMessage> {
		private Dictionary<int, System.Action<TMessage>> _listeners = new Dictionary<int, System.Action<TMessage>>();
		bool AddListener(object sender, System.Action<TMessage> func) {
			if (!_listeners.ContainsKey(sender.GetHashCode())) {
				_listeners.Add(sender.GetHashCode(), func);
				return true;
			}
			return false;
		}
		bool RemoveListener(object sender) {
			return _listeners.Remove(sender.GetHashCode());
		}
		void Send(TMessage msg) {
			foreach (var item in _listeners) {
				item.Value.Invoke(msg);
			}
		}
	};

	public class SimpleNotifyVoid {
		private Dictionary<int, System.Action> _listeners = new Dictionary<int, System.Action>();
		bool AddListener(object sender, System.Action func) {
			if (!_listeners.ContainsKey(sender.GetHashCode())) {
				_listeners.Add(sender.GetHashCode(), func);
				return true;
			}
			return false;
		}
		bool RemoveListener(object sender) {
			return _listeners.Remove(sender.GetHashCode());
		}
		void Send() {
			foreach (var item in _listeners) {
				item.Value.Invoke();
			}
		}
	};
} // GCL.Pattern