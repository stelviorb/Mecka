using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MeckaAutomation.Configuration
{
    class TestParameterBuilder
    {
        private readonly string callerName;
        private readonly List<TestParameterDefinition> testCases = new List<TestParameterDefinition>();

        public TestParameterBuilder([CallerMemberName] string callerName = "")
        {
            this.callerName = callerName.Substring(0, callerName.Length - "Case".Length);
        }

        public TestParameterBuilder Add<T>(string name, T defaultValue)
        {
            testCases.Add(new TestParameterDefinition<T>(name, defaultValue));
            return this;
        }

        public IEnumerable<TestCaseData> GetTestCases()
        {
            return TestSuiteConfig.GetTestCases(callerName, testCases);
        }
    }
}
