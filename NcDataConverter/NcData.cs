using Newtonsoft.Json.Linq;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NC_Data
{
    public class NcData
    {
        private const string OPERATION = "operation";
        private const string BOUNDINGBOX = "boundingBox";
        private const string PARAMETERARRAY = "parameterArray";
        private const string VALUEARRAY = "valueArray";
        private Operation_t operation;
        private BoundingBox_t boundingBox;
        private ParameterArray_t parameterArray;
        private ValueArray_t valueArray;

        public Operation_t Operation { get => operation; set => operation = value; }
        public BoundingBox_t BoundingBox { get => boundingBox; set => boundingBox = value; }
        public ParameterArray_t ParameterArray { get => parameterArray; set => parameterArray = value; }
        public ValueArray_t ValueArray { get => valueArray; set => valueArray = value; }
        public NcData()
        {
            Operation = new Operation_t();
            BoundingBox = new BoundingBox_t();
            ParameterArray = new ParameterArray_t();
            ValueArray = new ValueArray_t();
        }

        public JObject ToJson(string filename)
        {
            JObject jObject = new JObject();
            jObject.Add(OPERATION, Operation.ToJson());
            jObject.Add(BOUNDINGBOX, BoundingBox.ToJson());
            jObject.Add(PARAMETERARRAY, ParameterArray.ToJson());
            jObject.Add(VALUEARRAY, ValueArray.ToJson());
            WriteToText(jObject.ToString(), filename);
            return jObject;
        }

        private void WriteToText(String json, String filePath)
        {
            //현재 경로 설정은 임시
            String currentPath = Environment.CurrentDirectory;
            String fileName = "jsonFormat.txt";
            filePath = currentPath + "\\" + fileName;
            //

            File.WriteAllText(filePath, json, Encoding.Default);
        }

        public class Operation_t
        {
            private Dictionary<string, object> operation;
            public Dictionary<string, object> Operation { get => operation; set => operation = value; }

            public readonly string type = "type";
            public readonly string toolNo = "toolNo";
            public readonly string startMapID = "startMapID";
            public readonly string ncProgramFile = "ncProgramFile";
            public readonly string stepover = "stepover";

            public Operation_t() {
                Operation = new Dictionary<string, object>();
            }

            public void Add(string key, object value)
            {
                Operation.Add(key, value);
            }

            public JObject ToJson()
            {
                JObject json = JObject.FromObject(Operation);
                return json;
            }
        }

        public class BoundingBox_t
        {
            private Point3d minPoint3d;
            private Point3d maxPoint3d;

            private const string MINPOINT = "minPoint";
            private const string MAXPOINT = "maxPoint";
            private const string x = "x";
            private const string y = "y";
            private const string z = "z";

            public Point3d MinPoint { get => minPoint3d; set => minPoint3d = value; }
            public Point3d MaxPoint { get => maxPoint3d; set => maxPoint3d = value; }

            public BoundingBox_t() { }

            public void SetMinPoint(double minX, double minY, double minZ)
            {
                MinPoint = new Point3d(minX, minY, minZ);
            }

            public void SetMaxPoint(double maxX, double maxY, double maxZ)
            {
                MaxPoint = new Point3d(maxX, maxY, maxZ);
            }

            public JObject ToJson()
            {
                JObject json = new JObject();
                //JObject minJObject = JObject.FromObject(MinPoint);
                JObject minJObject = new JObject();
                minJObject.Add(x, MinPoint.X);
                minJObject.Add(y, MinPoint.Y);
                minJObject.Add(z, MinPoint.Z);

                //JObject maxJObject = JObject.FromObject(MaxPoint);
                JObject maxJObject = new JObject();
                maxJObject.Add(x, MaxPoint.X);
                maxJObject.Add(y, MaxPoint.Y);
                maxJObject.Add(z, MaxPoint.Z);

                json.Add(MINPOINT, minJObject);
                json.Add(MAXPOINT, maxJObject);
                return json;
            }
        }

        public class ParameterArray_t
        {
            private List<string> parameterArray;

            public List<string> ParameterArray { get => parameterArray; set => parameterArray = value; }

            public readonly string x = "x";
            public readonly string y = "y";
            public readonly string z = "z";
            public readonly string mMRROptiConverted = "mMRROptiConverted";
            public readonly string mMRROrigConverted = "mMRROrigConverted";
            public readonly string MovingDistance = "MovingDistance";

            public ParameterArray_t()
            {
                ParameterArray = new List<string>();
            }

            public void Add(string param)
            {
                ParameterArray.Add(param);
            }

            public JArray ToJson()
            {
                JArray jArray = new JArray();
                jArray = JArray.FromObject(ParameterArray);
                return jArray;
            }
        }


        public class ValueArray_t
        {
            private List<List<double>> valueArray;
            
            public List<List<double>> ValueArray { get => valueArray; set => valueArray = value; }

            public ValueArray_t()
            {
                valueArray = new List<List<double>>();
            }

            public void AddList(List<double> list)
            {
                ValueArray.Add(list);
            }

            public void AddList(double[] arr)
            {
                AddList(arr.ToList());
            }

            public JArray ToJson()
            {
                JArray jArray = new JArray();
                jArray = JArray.FromObject(ValueArray);
                return jArray;
            }
        }
    }
}
