using ConveyorDoc.Business.Constants;
using ConveyorDoc.Business.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Collections;

namespace ConveyorDoc.Business.Extension
{
    public static class CollectionExtension
    {
        public static List<T> DistinctCollection<T>(this List<T> list, string propertyName)
        {
            return list.GroupBy(x => x.GetType().GetProperty(propertyName)?.GetValue(x, null)).Select(y => y.First()).ToList();
        }

        public static async Task ForEachAsync<T>(this List<T> list, Func<T, Task> func)
        {
            foreach (var value in list)
            {
                await func(value);
            }
        }

        public static async Task ForEachAsync<T>(this ObservableCollection<T> list, Func<T, Task> func)
        {
            foreach (var value in list)
            {
                await func(value);
            }
        }

        public static void ForEach<T>(this IList<T> list, Action<T> action)
        {
            foreach(var value in list)
            {
                action(value);
            }
        }

        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (var value in list)
            {
                action(value);
            }
        }

        public static void GetVariableValues(this ObservableCollection<Variable> vars, object data)
        {

            if (data !=  null)
            {
                PropertyInfo[] info = data.GetType().GetProperties();

                foreach (var item in vars)
                {
                    var result = info.Select(x => x.Name).Where(y => y == item.Name).FirstOrDefault();

                    if (result != null)
                    {
                        var value = info.SingleOrDefault(e => e.Name == result).GetValue(data, null);

                        if (value is string v && v != null)
                        {
                            item.Value = v;

                        }
                        else
                            item.Value = string.Empty;
                    }
                }
            }

        }

        public static void GetVariableValue(this IEnumerable<Variable> vars, object value)
        {
            if(value != null) 
            {
               if (vars.Any(x=>x.Name == nameof(value)))
               {
                  vars.First(x => x.Name == nameof(value)).Value = value.ToString();
               }              
            }
        }

        public static void GetVariableValues(this IEnumerable<Variable> vars, object data)
        {
            if (data != null )
            {
                PropertyInfo[] info = data.GetType().GetProperties();

                foreach (var item in vars)
                {
                    var result = info.Select(x => x.Name).Where(y => y == item.Name).FirstOrDefault();

                    if (result != null)
                    {
                        item.Value = info.Single(e => e.Name == result).GetValue(data, null).ToString();
                    }
                }
            }

        }

        public static void AddVariable(this List<Variable> var, string text)
        {
            if(!var.Any(x=>x.Default == text))
            {
                var.Add(new Variable
                {
                    Name = Regex.Match(text, RegexPatternsConstants.NO_TAGS).Value,
                    Default = text,
                    Value = Regex.Match(text, RegexPatternsConstants.NO_TAGS).Value,
                });
            }

        }

        public static void Replace<T>(this ObservableCollection<T> observableColection, IEnumerable<T> collection)
        {
            observableColection.Clear();
            collection.ForEach(element=> observableColection.Add(element));      
        }

    }
}
