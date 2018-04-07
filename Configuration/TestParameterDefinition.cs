using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MeckaAutomation.Configuration
{
    public interface TestParameterDefinition
    {
        object GetParamValue(XmlNode node);
    }

    public class TestParameterDefinition<T> : TestParameterDefinition
    {
        public string Name { get; set; }

        public T DefaultValue { get; set; }

        public TestParameterDefinition(string name, T defaultValue)
        {
            Name = name;
            DefaultValue = defaultValue;
        }

        public object GetParamValue(XmlNode testCaseNode)
        {
            if (testCaseNode != null)
            {
                var testParamNode = testCaseNode.SelectSingleNode(string.Format("TestParam[@name='{0}']", Name));
                if (testParamNode != null && testParamNode.Attributes["value"] != null)
                {
                    string strVal = testParamNode.Attributes["value"].Value;
                    if (strVal != null)
                    {
                        try
                        {
                            return (T)Convert.ChangeType(strVal, typeof(T));
                        }
                        catch (Exception ex)
                        {
                            return DefaultValue;
                        }
                    }
                }
            }
            return DefaultValue;
        }
    }
}
