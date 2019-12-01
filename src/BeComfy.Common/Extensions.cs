using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace BeComfy.Common
{
    public static class Extensions
    {
        public static string Underscore(this string value)
            => string.Concat(value.Select((x, i) => i > 0 && char.IsUpper(x) 
                ? "_" + x.ToString() 
                : x.ToString()));

        public static TModel GetOptions<TModel>(this IConfiguration configuration, string section) 
            where TModel : new()
        {
            var model = new TModel();
            configuration.GetSection(section).Bind(model);
            
            return model;
        }
        
        public static string ToCommaSeparatedInt(this IEnumerable<int> input)
        {
            string output = string.Empty;
            foreach (int airport in input)
            {
                output = string.Concat(output, airport.ToString(), ',');
            }

            return output.Remove(output.Length - 1);
        }

        public static IEnumerable<int> FromCommaSeparatedString(this string input)
        {
            ICollection<int> output = new List<int>();

            foreach (var x in input.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                output.Add(Int16.Parse(x));
            }

            return output;
        }
    }
}