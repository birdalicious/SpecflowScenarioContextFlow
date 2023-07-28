using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepScanner
{
    public class StepDefinitionWriter
    {
        public static void WriteToJsonFile(IEnumerable<StepDefinitionInfo> stepDefinitions, string outputFilePath)
        {
            // Create a dictionary to store the final JSON structure
            var jsonDictionary = new Dictionary<string, Dictionary<string, string>>();

            // Populate the dictionary from the list of StepDefinitionInfo
            foreach (var stepDefinition in stepDefinitions)
            {
                // Get the current regex from the StepDefinitionInfo
                string regex = stepDefinition.Regex;

                // Get the current StepType and MethodType from the StepDefinitionInfo
                string stepType = stepDefinition.StepType;
                string methodType = stepDefinition.MethodType;

                // Create a new entry for the current regex if it doesn't exist
                if (!jsonDictionary.ContainsKey(regex))
                {
                    jsonDictionary[regex] = new Dictionary<string, string>();
                }

                // Add the StepType and MethodType to the dictionary for the current regex
                jsonDictionary[regex]["scenarioBlock"] = stepType;
                jsonDictionary[regex]["stepDefinition"] = methodType;
            }

            // Serialize the dictionary to JSON format
            string jsonString = JsonConvert.SerializeObject(jsonDictionary, Formatting.Indented);

            // Write the JSON string to the output file
            File.WriteAllText(outputFilePath, jsonString);
        }
    }
}
