using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NC_Data;
using Newtonsoft.Json.Linq;

namespace NcDataConverterTest
{
    [TestClass]
    public class UnitTest1
    {
        private static NcData ncData;

        public static NcData NcData { get => ncData; set => ncData = value; }

        [AssemblyInitialize]
        public static void Setting(TestContext testContext)
        {
            NcData = new NcData();
            //Operation
            NcData.Operation.Add(NcData.Operation.type, "Op01-001");
            NcData.Operation.Add(NcData.Operation.toolNo, "Tool001");
            NcData.Operation.Add(NcData.Operation.startMapID, "31145");
            NcData.Operation.Add(NcData.Operation.ncProgramFile, "O0001.apt");
            NcData.Operation.Add(NcData.Operation.stepover, 10.0);
            //

            //BoundingBox
            NcData.BoundingBox.SetMinPoint(0.0, 0.0, 0.0);
            NcData.BoundingBox.SetMaxPoint(100.0, 100.0, 100.0);
            //

            //ParameterArray
            List<string> parameterList = new List<string>();
            parameterList.Add(NcData.ParameterArray.x);
            parameterList.Add(NcData.ParameterArray.y);
            parameterList.Add(NcData.ParameterArray.z);
            parameterList.Add(NcData.ParameterArray.mMRROptiConverted);
            parameterList.Add(NcData.ParameterArray.mMRROrigConverted);
            parameterList.Add(NcData.ParameterArray.MovingDistance);

            foreach(string param in parameterList)
            {
                NcData.ParameterArray.Add(param);
            }
            //

            //ValueArray
            List<double> firstExampleList = new List<double>();
            for(int i = 0; i < 10; i++)
            {
                firstExampleList.Add(i);
            }

            List<double> secondExampleList = new List<double>();
            for(int i = 100; i < 110; i++)
            {
                secondExampleList.Add(i);
            }

            List<double> thirdExampleList = new List<double>();
            for(int i = 200; i < 210; i++)
            {
                thirdExampleList.Add(i);
            }

            NcData.ValueArray.AddList(firstExampleList);
            NcData.ValueArray.AddList(secondExampleList);
            NcData.ValueArray.AddList(thirdExampleList);
            //
        }

        [TestMethod]
        public void TestNcData()
        {
            String directoryPath = Environment.CurrentDirectory;
            String filePath = directoryPath + "\\jsonFormatTest.txt";
            String original = File.ReadAllText(filePath);

            //결과를 출력할 txt파일 이름을 변수로
            JObject jObject = NcData.ToJson("jsonFormat.txt");
            String test = jObject.ToString();
            
            Assert.AreEqual(original, test);
        }

        [AssemblyCleanup]
        public static void CleanUp()
        {
            NcData = null;
        }
    }
}
