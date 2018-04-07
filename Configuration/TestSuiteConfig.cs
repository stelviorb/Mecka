using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using NUnit.Framework;

namespace MeckaAutomation.Configuration
{
    public static class TestSuiteConfig
    {
        private static XmlDocument xmlDoc { get; set; }

        public static string ConfigFileName { get; set; }

        private static void assureConfigOpen()
        {
            if (xmlDoc == null)
            {
                Init();
            }
        }

        private static string ReadStringParam(string xPath, string defaultValue)
        {
            assureConfigOpen();
            var node = xmlDoc.SelectSingleNode(xPath);
            if (node != null)
            {
                return node.InnerText;
            }
            return defaultValue;
        }

        private static int ReadIntParam(string xPath, int defaultValue)
        {
            assureConfigOpen();
            var node = xmlDoc.SelectSingleNode(xPath);
            int result = defaultValue;
            if (node != null)
            {
                Int32.TryParse(node.InnerText, out result);
            }
            return result;
        }

        public static IEnumerable<TestCaseData> GetTestCases(string testMethodName, List<TestParameterDefinition> paramDefs)
        {
            assureConfigOpen();
            var testMethodNode = xmlDoc.SelectSingleNode(string.Format("/TestSuite/TestMethod[@name='{0}']", testMethodName));
            if (testMethodNode != null)
            {
                foreach (XmlNode testCaseNode in testMethodNode.ChildNodes)
                {
                    List<object> testParams = new List<object>();
                    foreach (var paramDef in paramDefs)
                    {
                        Type paramType = paramDef.GetType().GetGenericArguments()[0];
                        testParams.Add(paramDef.GetParamValue(testCaseNode));
                    }
                    yield return new TestCaseData(testParams.ToArray()).SetName(testCaseNode.Attributes["description"].Value);
                }
            }
        }

        public static void Init(string configFileName = null)
        {
            if (!string.IsNullOrEmpty(configFileName))
            {
                ConfigFileName = configFileName;
            }
            else if (string.IsNullOrEmpty(ConfigFileName))
            {
                //ConfigFileName = "testSuite.xml";
                ConfigFileName = ConfigurationManager.AppSettings["testSuitePath"];
            }

            xmlDoc = new XmlDocument();
            xmlDoc.Load(ConfigFileName);
        }
    }
}
