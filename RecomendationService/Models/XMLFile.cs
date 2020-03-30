using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace SerializeObjects
{
    public static class XMLFile
    {
        private static string path;
        public static string Path {
            get { return path; }
            set {
                IsPathSet = true;
                path = value;
            }
        }
        private static bool IsPathSet = false;
        private const string PATH_NOT_SET_ERROR = "Path is not set";

        /// <summary>
        /// Serialize a List of Objects into an XML file. If the file does not exist create a new one.
        /// The path must be set before, otherwise an exception with the mesagge "Path is not set"
        /// will be thrown.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">List of objects to serialize</param>
        /// <param name="fileName">File name including the extention (.xml)</param>
        public static void SerializeList<T>(T obj, string fileName)
        {
            try
            {
                if (!IsPathSet)
                    throw new Exception(PATH_NOT_SET_ERROR);
                XmlSerializer ser = new XmlSerializer(typeof(T));
                //Create a FileStream object connected to the target file
                FileStream fileStream = new FileStream(Path + fileName, FileMode.Create);
                ser.Serialize(fileStream, obj);
                fileStream.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }

        /// <summary>
        /// Deserialize a List of Objects from an XML file. If the file does not exist thrown
        /// an exception.
        /// The path must be set before, otherwise an exception with the mesagge "Path is not set"
        /// will be thrown.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">File name including the extention (.xml)</param>
        public static T DeserializeList<T>(string fileName)
        {
            try
            {
                if (!IsPathSet)
                    throw new Exception(PATH_NOT_SET_ERROR);
                XmlDocument doc = new XmlDocument();
                doc.Load(Path + fileName);
                T result;
                XmlSerializer ser = new XmlSerializer(typeof(T));
                using (TextReader tr = new StringReader(doc.OuterXml))
                {
                    result = (T)ser.Deserialize(tr);
                }
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }
    }
}
