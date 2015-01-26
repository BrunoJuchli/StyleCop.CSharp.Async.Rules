namespace Specifications.Rules
{
    using StyleCop.CSharp;
    using System;

    public static class RulesRegistry
    {
        public static readonly GenericKeyedCollection<Type, RuleInfo> Rules = InitializeRulesMap();

        private static GenericKeyedCollection<Type, RuleInfo> InitializeRulesMap()
        {
            var map = new GenericKeyedCollection<Type, RuleInfo>(x => x.RuleType);
            foreach (RuleInfo rule in RulesReader.ReadRules(typeof(AsyncRules)))
            {
                map.Add(rule);
            }

            return map;
        }
    }
}