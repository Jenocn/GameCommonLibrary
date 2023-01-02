/*
	By Jenocn
	https://jenocn.github.io/
*/

using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace GCL {

	/// <summary>
	/// XML工具
	/// </summary>
	public static class XMLTool {

		/// <summary>
		/// 解析xml数据
		/// </summary>
		public static List<Dictionary<string, string>> ParseToList(string src) {
			if (string.IsNullOrEmpty(src)) { return new List<Dictionary<string, string>>(); }
			var xrs = new XmlReaderSettings();
			xrs.IgnoreComments = true;
			var stringReader = new StringReader(src);
			var xmlReader = XmlReader.Create(stringReader, xrs);
			var doc = new XmlDocument();
			doc.Load(xmlReader);
			var xmlNode = doc.LastChild;
			var nodeList = xmlNode.ChildNodes;
			var ret = new List<Dictionary<string, string>>(nodeList.Count);
			foreach (XmlNode item in nodeList) {
				int lineCount = item.Attributes.Count;
				if (lineCount == 0) { continue; }
				var lineData = new Dictionary<string, string>(lineCount);
				for (int i = 0; i < lineCount; ++i) {
					string key = item.Attributes.Item(i).Name;
					string value = item.Attributes.Item(i).Value;
					lineData.Add(key, value);
				}
				ret.Add(lineData);
			}
			return ret;
		}

		/// <summary>
		/// 解析xml数据
		/// </summary>
		public static Dictionary<string, string> ParseToKV(string src, string keySign = "key") {
			if (string.IsNullOrEmpty(src)) { return new Dictionary<string, string>(); }
			var xrs = new XmlReaderSettings();
			xrs.IgnoreComments = true;
			var stringReader = new StringReader(src);
			var xmlReader = XmlReader.Create(stringReader, xrs);
			var doc = new XmlDocument();
			doc.Load(xmlReader);
			var xmlNode = doc.LastChild;
			var nodeList = xmlNode.ChildNodes;
			var ret = new Dictionary<string, string>(nodeList.Count);
			foreach (XmlNode item in nodeList) {
				int lineCount = item.Attributes.Count;
				if (lineCount < 2) { continue; }
				string key0 = item.Attributes.Item(0).Name;
				if (key0 == keySign) {
					ret.Add(item.Attributes.Item(0).Value, item.Attributes.Item(1).Value);
					continue;
				}
				string key1 = item.Attributes.Item(1).Name;
				if (key1 == keySign) {
					ret.Add(item.Attributes.Item(1).Value, item.Attributes.Item(0).Value);
					continue;
				}
			}
			return ret;
		}

		/// <summary>
		/// 将xml数据转为string
		/// </summary>
		public static string ToString(List<Dictionary<string, string>> xmlData, string lineSign = "item", string rootName = "root") {
			if (xmlData.Count == 0) { return ""; }
			var xmlDoc = new XmlDocument();
			xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null));
			var rootNode = xmlDoc.AppendChild(xmlDoc.CreateElement(rootName));

			foreach (var lineData in xmlData) {
				if (lineData.Count == 0) { continue; }
				var itemNode = rootNode.AppendChild(xmlDoc.CreateElement(lineSign));
				foreach (var pair in lineData) {
					// create item
					var attirbuteValue = xmlDoc.CreateAttribute(pair.Key);
					attirbuteValue.Value = pair.Value.ToString();
					itemNode.Attributes.Append(attirbuteValue);
				}
			}
			var stream = new MemoryStream();
			var writer = new XmlTextWriter(stream, null);
			writer.Formatting = Formatting.Indented;
			xmlDoc.Save(writer);
			var sr = new StreamReader(stream, System.Text.Encoding.UTF8);
			stream.Position = 0;
			string xmlString = sr.ReadToEnd();
			sr.Close();
			stream.Close();
			return xmlString;
		}
	}
}