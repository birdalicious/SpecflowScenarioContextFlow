using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace StepScanner
{
    public static class StepDefinitionScanner
    {
        public static IEnumerable<StepDefinitionInfo> ScanForStepDefinitions()
        {
            var stepDefinitionAttributes = new List<Type>
            {
                typeof(GivenAttribute),
                typeof(WhenAttribute),
                typeof(ThenAttribute),
                typeof(StepDefinitionAttribute),
            };

            var assemblies = new List<Assembly> { Assembly.LoadFrom("location of assembly dll") };

            foreach (var assembly in assemblies)
            {
                var bindingClasses = assembly.GetTypes().Where(t => t.GetCustomAttributes(typeof(BindingAttribute), false).Any());

                foreach (var bindingClass in bindingClasses)
                {
                    var methods = bindingClass.GetMethods()
                        .Where(m => m.GetCustomAttributes().Any(attr => stepDefinitionAttributes.Contains(attr.GetType())));

                    foreach (var method in methods)
                    {
                        var attributes = method.GetCustomAttributes()
                            .Where(attr => stepDefinitionAttributes.Contains(attr.GetType()))
                            .ToList();

                        foreach (var attribute in attributes)
                        {
                            var attr = attribute as StepDefinitionBaseAttribute;
                            if (attr is null)
                            {
                                throw new NotSupportedException();
                            }

                            yield return new StepDefinitionInfo
                            {
                                StepType = attr.Types.Length == 1 ? attr.Types[0].ToString() : "Step",
                                Regex = attr.Regex,
                                MethodType = method.Name,
                                ClassName = bindingClass.Name,
                            };
                        }
                    }
                }
            }
        }
    }
}
