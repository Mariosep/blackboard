using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

// TODO: Refactor in order to not be specific for EventChannels 
public class ScriptCreator
{
    public static void CreateScript(string scriptPath, string scriptName, List<string> fields, List<string> dependencies)
    {
        // Specify the script name and location
        scriptPath = $"{scriptPath}{scriptName}.cs";
        
        // Create the script content
        string scriptContent = GenerateScriptContent(scriptName, fields, dependencies);
        
        // Compare content with existing file
        if (File.Exists(scriptPath))
        {
            string existingContent = File.ReadAllText(scriptPath);

            // Check if content is equal
            if (string.Equals(existingContent, scriptContent))
            {
                //Debug.Log("Script content is identical. No changes made.");
                return; // Exit without overwriting
            }
        }
        
        // Write the script to the specified path
        File.WriteAllText(scriptPath, scriptContent);

        // Refresh the AssetDatabase to make the new script visible in the Unity Editor
        AssetDatabase.Refresh();

        Debug.Log("Script created: " + scriptName);
    }
    
    private static string GenerateScriptContent(string scriptName, List<string> fields, List<string> dependencies)
    {
        // Customize the script template as needed
        string template = @"[DEPENDENCIES]

namespace Blackboard.Events
{
    public class [SCRIPTNAME] : EventChannel
    {
[FIELDS]
    }
}
";

        // Replace placeholder with the actual script name
        string scriptContent = template.Replace("[SCRIPTNAME]", scriptName);
        scriptContent = scriptContent.Replace("[DEPENDENCIES]", GenerateDependenciesContent(dependencies));
        scriptContent = scriptContent.Replace("[FIELDS]", GenerateFieldsContent(fields));

        scriptContent = scriptContent.Replace("Int32", "int");
        scriptContent = scriptContent.Replace("String", "string");
        
        return scriptContent;
    }

    private static string GenerateDependenciesContent(List<string> dependencies)
    {
        if(dependencies.Contains("System"))
            dependencies.Remove("System");
        
        StringBuilder fieldsContentBuilder = new StringBuilder();
        fieldsContentBuilder.AppendLine($"using System;");
        foreach (string dependency in dependencies)
            fieldsContentBuilder.AppendLine($"using {dependency};");

        // Remove the trailing newline
        return fieldsContentBuilder.ToString().TrimEnd('\r', '\n');
    }
    
    private static string GenerateFieldsContent(List<string> fields)
    {
        StringBuilder fieldsContentBuilder = new StringBuilder();
        foreach (string field in fields)
            fieldsContentBuilder.AppendLine($"\t \t{field}");

        // Remove the trailing newline
        return fieldsContentBuilder.ToString().TrimEnd('\r', '\n');
    }
}