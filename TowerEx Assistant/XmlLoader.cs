using System;
using System.IO;
using System.Text;
using System.Xml;

namespace TowerEx_Assistant
{
	public class XmlLoader
	{
		private XmlDocument doc = new XmlDocument();

		private StringBuilder sb = new StringBuilder();

		public XmlDocument Load(string path)
		{
			try
			{
				doc.Load(path);
				return doc;
			}
			catch (Exception)
			{
				NewXML(path);
				doc.Load(path);
				return doc;
			}
		}

		public void OutputXML(XmlDocument xml, string path)
		{
			FileStream fileStream = new FileStream(path, FileMode.Create);
			xml.Save(fileStream);
			fileStream.Close();
		}

		private void NewXML(string path)
		{
			try
			{
				sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
				sb.Append("<Root>");
				sb.Append("</Root>");
				using (StreamWriter streamWriter = new StreamWriter(path, append: false, Encoding.UTF8))
                {
					streamWriter.Write(sb.ToString());
				}
			}
			catch (Exception)
			{
			}
		}
	}
}
