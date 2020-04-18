using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Lb.Common
{
    /// <summary>
    /// ������ṩ��һЩʵ�õķ�����ת��XML�Ͷ���
    /// </summary>
    public sealed class XmlConvertor
    {
        private XmlConvertor()
        {
        }

        /// <summary>
        /// ��XML�ַ���ת����ָ���Ķ���
        /// </summary>
        /// <param name="xml">XML�ַ�����</param>
        /// <param name="type">��������͡�</param>
        /// <returns>��XML�ַ��������л��Ķ���</returns>
        public static object XmlToObject(string xml, Type type)
        {
            if (null == xml)
            {
                throw new ArgumentNullException("xml");
            }
            if (null == type)
            {
                throw new ArgumentNullException("type");
            }

            object obj = null;
            XmlSerializer serializer = new XmlSerializer(type);
            StringReader strReader = new StringReader(xml);
            XmlReader reader = new XmlTextReader(strReader);

            try
            {
                obj = serializer.Deserialize(reader);
            }
            catch (InvalidOperationException ie)
            {
                throw new InvalidOperationException("Can not convert xml to object", ie);
            }
            finally
            {
                reader.Close();
            }
            return obj;
        }

        /// <summary>
        /// ת��ΪXML�ַ����Ķ���
        /// </summary>
        /// <param name="obj">Ҫ���л��Ķ���</param>
        /// <param name="toBeIndented"><c>true</c> �����Ҫ��XML�ַ�����Ŀ�ģ����� <c>false</c>.</param>
        /// <returns>XML�ַ�����</returns>
        public static string ObjectToXml(object obj, bool toBeIndented)
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }
            UTF8Encoding encoding = new UTF8Encoding(false);
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            MemoryStream stream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(stream, encoding);
            writer.Formatting = (toBeIndented ? Formatting.Indented : Formatting.None);

            try
            {
                serializer.Serialize(writer, obj);
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("Can not convert object to xml.");
            }
            finally
            {
                writer.Close();
            }

            string xml = encoding.GetString(stream.ToArray());
            return xml;
        }
    }
}