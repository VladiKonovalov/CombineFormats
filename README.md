using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

class Program
{
    static void Main(string[] args)
    {
        var inputPath = "input.xml";
        var outputPath = "output.xml";

        var inputXml = XDocument.Load(inputPath);
        var fileMap = new Dictionary<string, List<(string AppName, string Destination)>>();

        foreach (var app in inputXml.Descendants("Application"))
        {
            var appName = app.Attribute("name")?.Value;

            foreach (var file in app.Elements("File"))
            {
                var fileName = file.Attribute("name")?.Value;
                var destination = file.Value;

                if (!fileMap.ContainsKey(fileName))
                    fileMap[fileName] = new List<(string, string)>();

                fileMap[fileName].Add((appName, destination));
            }
        }

        var outputXml = new XDocument(new XElement("Files"));

        foreach (var entry in fileMap)
        {
            var fileElement = new XElement("File", new XAttribute("name", entry.Key));

            foreach (var (appName, dest) in entry.Value)
            {
                fileElement.Add(new XElement("App", new XAttribute("name", appName), dest));
            }

            outputXml.Root.Add(fileElement);
        }

        outputXml.Save(outputPath);
        Console.WriteLine($"Output saved to {outputPath}");
    }
}

