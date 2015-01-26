using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;

namespace Specifications.Rules
{
    public static class RulesReader
    {
        public static IEnumerable<RuleInfo> ReadRules(Type sourceAnalyzerType)
        {
            XmlDocument xmlDocument = LoadAnalyzerXml(sourceAnalyzerType);

            var ruleNodes = xmlDocument.SelectNodes("//Rule");
            if (ruleNodes == null)
            {
                return Enumerable.Empty<RuleInfo>();
            }

            var types = sourceAnalyzerType.Assembly.GetTypes();

            return ruleNodes.OfType<XmlNode>()
                .Select(rule =>
                {
                    string name = rule.Attributes["Name"].Value;
                    return new RuleInfo
                    {
                        Name = name,
                        CheckId = rule.Attributes["CheckId"].Value,
                        RuleType = types.Single(x => x.Name == name)
                    };
                });
        }

        private static XmlDocument LoadAnalyzerXml(Type sourceAnalyzerType)
        {
            using (var sourceAnalyzerXmlStream = GetAnalyzerXmlStream(sourceAnalyzerType))
            {
                var document = new XmlDocument();
                document.Load(sourceAnalyzerXmlStream);
                return document;
            }
        }

        private static Stream GetAnalyzerXmlStream(Type sourceAnalyzerType)
        {
            string resourceName = string.Format(CultureInfo.InvariantCulture, "{0}.xml", sourceAnalyzerType.FullName);

            return sourceAnalyzerType.Assembly
                .GetManifestResourceStream(resourceName);
        }
    }
}