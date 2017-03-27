using System.Collections.Generic;

namespace pmi.Droid.Utilities
{
    static class MenuItemValueMapper
    {
        static Dictionary<int, string> values = new Dictionary<int, string>()
        {
            { Resource.Id.home, "HOME"},
            { Resource.Id.program, "PROGRAM"},
            { Resource.Id.conference, "CONFERENCE"},
            { Resource.Id.map, "MAP"},
            { Resource.Id.information, "INFORMATION"},
        };

        static Dictionary<int, int> translations = new Dictionary<int, int>()
        {
            { Resource.Id.home, Resource.String.home},
            { Resource.Id.program, Resource.String.program},
            { Resource.Id.conference, Resource.String.conference},
            { Resource.Id.map, Resource.String.map},
            { Resource.Id.information, Resource.String.information},
        };

        public static string GetValue(int key) {
            return values.ContainsKey(key) ? values[key] : string.Empty;
        }

        public static int GetTranslation(int key) {
            return translations.ContainsKey(key) ? translations[key] : 0;
        }
        
    }
}