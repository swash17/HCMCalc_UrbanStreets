using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;


namespace HCMCalc_UrbanStreets
{
    public static class FileIOserviceVols
    {

        public static void SerializeServiceVolInputData(string filename, List<SerVolTablesByClass> serVolInputs)
        {
            // Writing the file requires a TextWriter.
            //TextWriter myStreamWriter = new StreamWriter(filename);
            FileStream myStream = new FileStream(filename, FileMode.Create);

            // Create the XmlSerializer instance.
            XmlSerializer mySerializer = new XmlSerializer(typeof(List<SerVolTablesByClass>));

            mySerializer.Serialize(myStream, serVolInputs);
            myStream.Close();
        }

        public static List<ServiceVolumeTableFDOT> DeserializeServiceVolInputData(string filename)
        {
            FileStream myFileStream = new FileStream(filename, FileMode.Open);
            // Create the XmlSerializer instance.
            XmlSerializer mySerializer = new XmlSerializer(typeof(List<ServiceVolumeTableFDOT>));
            List<ServiceVolumeTableFDOT> ServiceVolInputData = (List<ServiceVolumeTableFDOT>)mySerializer.Deserialize(myFileStream);
            myFileStream.Close();

            return ServiceVolInputData;
        }

    }
}
