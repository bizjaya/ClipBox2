using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ClipBox2
{
    public static class SimpleJson
    {
        public static string SerializeObject<T>(T obj)
        {
            if (obj == null) return "null";

            var sb = new StringBuilder();
            SerializeObject(obj, sb, 0);
            return sb.ToString();
        }

        public static T DeserializeObject<T>(string json) where T : new()
        {
            if (string.IsNullOrWhiteSpace(json) || json == "null")
                return default(T);

            var jsonObj = ParseJson(json.Trim());
            return DeserializeObject<T>(jsonObj);
        }

        private static void SerializeObject(object obj, StringBuilder sb, int indent)
        {
            if (obj == null)
            {
                sb.Append("null");
                return;
            }

            var type = obj.GetType();

            // Handle arrays and lists
            if (type.IsArray || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>)))
            {
                sb.Append("[");
                bool first = true;
                foreach (var item in (System.Collections.IEnumerable)obj)
                {
                    if (!first) sb.Append(", ");
                    first = false;

                    if (item is string str)
                        sb.Append($"\"{EscapeString(str)}\"");
                    else if (item is DateTime dt)
                        sb.Append($"\"{dt:yyyy-MM-dd HH:mm:ss}\"");
                    else if (item is bool b)
                        sb.Append(b.ToString().ToLower());
                    else if (item is ValueType)
                        sb.Append(item.ToString());
                    else
                        SerializeObject(item, sb, indent + 1);
                }
                sb.Append("]");
                return;
            }
            
            // Handle Dictionary<string, Info> specially for MasterData
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>))
            {
                var dict = obj as System.Collections.IDictionary;
                sb.AppendLine("{");
                
                bool first = true;
                foreach (System.Collections.DictionaryEntry entry in dict)
                {
                    if (!first) sb.AppendLine(",");
                    first = false;
                    
                    sb.Append(new string(' ', (indent + 1) * 2));
                    sb.Append($"\"{EscapeString(entry.Key.ToString())}\":");
                    sb.Append(" ");
                    SerializeObject(entry.Value, sb, indent + 1);
                }
                
                if (!first) sb.AppendLine();
                sb.Append(new string(' ', indent * 2));
                sb.Append("}");
                return;
            }

            sb.AppendLine("{");
            bool firstProp = true;
            
            foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var value = prop.GetValue(obj);
                if (value != null)
                {
                    if (!firstProp) sb.AppendLine(",");
                    firstProp = false;
                    
                    sb.Append(new string(' ', (indent + 1) * 2));
                    sb.Append($"\"{prop.Name}\": ");
                    
                    if (value is string strValue)
                    {
                        sb.Append($"\"{EscapeString(strValue)}\"");
                    }
                    else if (value is DateTime dtValue)
                    {
                        sb.Append($"\"{dtValue:yyyy-MM-dd HH:mm:ss}\"");
                    }
                    else if (value.GetType().IsValueType)
                    {
                        sb.Append(value.ToString().ToLower());
                    }
                    else
                    {
                        SerializeObject(value, sb, indent + 1);
                    }
                }
            }
            
            if (!firstProp) sb.AppendLine();
            sb.Append(new string(' ', indent * 2));
            sb.Append("}");
        }

        private static Dictionary<string, object> ParseJson(string json)
        {
            var result = new Dictionary<string, object>();
            if (string.IsNullOrWhiteSpace(json)) return result;
            
            json = json.Trim();
            if (!json.StartsWith("{") || !json.EndsWith("}")) return result;
            
            json = json.Substring(1, json.Length - 2).Trim();
            
            foreach (var pair in SplitJsonPairs(json))
            {
                var parts = SplitKeyValue(pair);
                if (parts.Length == 2)
                {
                    string key = UnescapeString(parts[0].Trim('"', ' '));
                    string value = parts[1].Trim();
                    
                    if (value.StartsWith("[") && value.EndsWith("]"))
                    {
                        // Handle arrays/lists
                        var items = new List<object>();
                        string arrayContent = value.Substring(1, value.Length - 2).Trim();
                        
                        if (!string.IsNullOrWhiteSpace(arrayContent))
                        {
                            foreach (var item in SplitArrayItems(arrayContent))
                            {
                                string trimmedItem = item.Trim();
                                if (trimmedItem.StartsWith("[") && trimmedItem.EndsWith("]"))
                                {
                                    // Handle nested arrays
                                    var nestedItems = new List<object>();
                                    string nestedContent = trimmedItem.Substring(1, trimmedItem.Length - 2).Trim();
                                    if (!string.IsNullOrWhiteSpace(nestedContent))
                                    {
                                        foreach (var nestedItem in SplitArrayItems(nestedContent))
                                        {
                                            nestedItems.Add(ParseJsonValue(nestedItem.Trim()));
                                        }
                                    }
                                    items.Add(nestedItems);
                                }
                                else
                                {
                                    items.Add(ParseJsonValue(trimmedItem));
                                }
                            }
                        }
                        result[key] = items;
                    }
                    else if (value.StartsWith("{") && value.EndsWith("}"))
                    {
                        // Handle nested objects
                        result[key] = ParseJson(value);
                    }
                    else
                    {
                        result[key] = ParseJsonValue(value);
                    }
                }
            }
            
            return result;
        }

        private static object ParseJsonValue(string value)
        {
            value = value.Trim();
            
            if (value.StartsWith("\"") && value.EndsWith("\""))
            {
                return UnescapeString(value.Substring(1, value.Length - 2));
            }
            else if (value == "true")
            {
                return true;
            }
            else if (value == "false")
            {
                return false;
            }
            else if (value == "null")
            {
                return null;
            }
            else if (DateTime.TryParse(value.Trim('"'), out DateTime dt))
            {
                return dt;
            }
            else if (double.TryParse(value, out double d))
            {
                return d;
            }
            
            return value;
        }

        private static List<string> SplitArrayItems(string arrayContent)
        {
            var items = new List<string>();
            int depth = 0;
            int start = 0;
            bool inQuotes = false;
            
            for (int i = 0; i < arrayContent.Length; i++)
            {
                char c = arrayContent[i];
                
                if (c == '"' && (i == 0 || arrayContent[i - 1] != '\\'))
                {
                    inQuotes = !inQuotes;
                }
                else if (!inQuotes)
                {
                    if (c == '[' || c == '{')
                    {
                        depth++;
                    }
                    else if (c == ']' || c == '}')
                    {
                        depth--;
                    }
                    else if (c == ',' && depth == 0)
                    {
                        items.Add(arrayContent.Substring(start, i - start));
                        start = i + 1;
                    }
                }
            }
            
            if (start < arrayContent.Length)
            {
                items.Add(arrayContent.Substring(start));
            }
            
            return items;
        }

        private static T DeserializeObject<T>(Dictionary<string, object> jsonObj) where T : new()
        {
            if (jsonObj == null) return new T();

            var result = new T();
            var type = typeof(T);
            
            foreach (var prop in type.GetProperties())
            {
                if (!jsonObj.ContainsKey(prop.Name)) continue;
                
                var value = jsonObj[prop.Name];
                if (value == null) continue;

                if (prop.PropertyType == typeof(string))
                {
                    prop.SetValue(result, value.ToString());
                }
                else if (prop.PropertyType == typeof(DateTime))
                {
                    if (DateTime.TryParse(value.ToString(), out DateTime dt))
                    {
                        prop.SetValue(result, dt);
                    }
                }
                else if (prop.PropertyType == typeof(List<string>))
                {
                    var list = new List<string>();
                    if (value is List<object> objList)
                    {
                        foreach (var item in objList)
                        {
                            list.Add(item?.ToString() ?? "");
                        }
                    }
                    prop.SetValue(result, list);
                }
                else if (prop.PropertyType == typeof(List<List<string>>))
                {
                    var listOfLists = new List<List<string>>();
                    if (value is List<object> outerList)
                    {
                        foreach (var innerObj in outerList)
                        {
                            var innerList = new List<string>();
                            if (innerObj is List<object> innerObjList)
                            {
                                foreach (var item in innerObjList)
                                {
                                    innerList.Add(item?.ToString() ?? "");
                                }
                            }
                            listOfLists.Add(innerList);
                        }
                    }
                    prop.SetValue(result, listOfLists);
                }
                else if (prop.PropertyType == typeof(Dictionary<string, Info>))
                {
                    var dict = new Dictionary<string, Info>();
                    if (value is Dictionary<string, object> objDict)
                    {
                        foreach (var kvp in objDict)
                        {
                            if (kvp.Value is Dictionary<string, object> infoDict)
                            {
                                dict[kvp.Key] = DeserializeObject<Info>(infoDict);
                            }
                        }
                    }
                    prop.SetValue(result, dict);
                }
                else if (prop.PropertyType.IsValueType)
                {
                    try
                    {
                        prop.SetValue(result, Convert.ChangeType(value, prop.PropertyType));
                    }
                    catch
                    {
                        // If conversion fails, skip this property
                    }
                }
            }
            
            return result;
        }

        private static object DeserializeObject(Type type, Dictionary<string, object> jsonObj)
        {
            var result = Activator.CreateInstance(type);
            
            foreach (var prop in type.GetProperties())
            {
                if (!jsonObj.ContainsKey(prop.Name)) continue;
                
                var value = jsonObj[prop.Name];
                if (value == null) continue;

                if (prop.PropertyType == typeof(string))
                {
                    prop.SetValue(result, value.ToString());
                }
                else if (prop.PropertyType == typeof(DateTime))
                {
                    if (DateTime.TryParse(value.ToString(), out DateTime dt))
                    {
                        prop.SetValue(result, dt);
                    }
                }
                else if (prop.PropertyType == typeof(List<string>))
                {
                    var list = new List<string>();
                    if (value is List<object> objList)
                    {
                        foreach (var item in objList)
                        {
                            list.Add(item?.ToString() ?? "");
                        }
                    }
                    prop.SetValue(result, list);
                }
                else if (prop.PropertyType == typeof(List<List<string>>))
                {
                    var listOfLists = new List<List<string>>();
                    if (value is List<object> outerList)
                    {
                        foreach (var innerObj in outerList)
                        {
                            var innerList = new List<string>();
                            if (innerObj is List<object> innerObjList)
                            {
                                foreach (var item in innerObjList)
                                {
                                    innerList.Add(item?.ToString() ?? "");
                                }
                            }
                            listOfLists.Add(innerList);
                        }
                    }
                    prop.SetValue(result, listOfLists);
                }
                else if (prop.PropertyType == typeof(Dictionary<string, Info>))
                {
                    var dict = new Dictionary<string, Info>();
                    if (value is Dictionary<string, object> objDict)
                    {
                        foreach (var kvp in objDict)
                        {
                            if (kvp.Value is Dictionary<string, object> infoDict)
                            {
                                dict[kvp.Key] = DeserializeObject<Info>(infoDict);
                            }
                        }
                    }
                    prop.SetValue(result, dict);
                }
                else if (prop.PropertyType.IsValueType)
                {
                    try
                    {
                        prop.SetValue(result, Convert.ChangeType(value, prop.PropertyType));
                    }
                    catch
                    {
                        // If conversion fails, skip this property
                    }
                }
            }
            
            return result;
        }

        private static string[] SplitJsonPairs(string json)
        {
            var pairs = new List<string>();
            int start = 0;
            int braceCount = 0;
            bool inQuotes = false;
            
            for (int i = 0; i < json.Length; i++)
            {
                char c = json[i];
                if (c == '"' && (i == 0 || json[i - 1] != '\\'))
                {
                    inQuotes = !inQuotes;
                }
                else if (!inQuotes)
                {
                    if (c == '{') braceCount++;
                    else if (c == '}') braceCount--;
                    else if (c == ',' && braceCount == 0)
                    {
                        pairs.Add(json.Substring(start, i - start).Trim());
                        start = i + 1;
                    }
                }
            }
            
            if (start < json.Length)
            {
                pairs.Add(json.Substring(start).Trim());
            }
            
            return pairs.ToArray();
        }

        private static string[] SplitKeyValue(string pair)
        {
            int colonIndex = -1;
            bool inQuotes = false;
            
            for (int i = 0; i < pair.Length; i++)
            {
                char c = pair[i];
                if (c == '"' && (i == 0 || pair[i - 1] != '\\'))
                {
                    inQuotes = !inQuotes;
                }
                else if (c == ':' && !inQuotes)
                {
                    colonIndex = i;
                    break;
                }
            }
            
            if (colonIndex == -1) return new string[0];
            
            return new[]
            {
                pair.Substring(0, colonIndex).Trim(),
                pair.Substring(colonIndex + 1).Trim()
            };
        }

        private static string EscapeString(string str)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;
            
            return str.Replace("\\", "\\\\")
                     .Replace("\"", "\\\"")
                     .Replace("\r", "\\r")
                     .Replace("\n", "\\n")
                     .Replace("\t", "\\t");
        }

        private static string UnescapeString(string str)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;
            
            return str.Replace("\\\"", "\"")
                     .Replace("\\\\", "\\")
                     .Replace("\\r", "\r")
                     .Replace("\\n", "\n")
                     .Replace("\\t", "\t");
        }
    }
}
