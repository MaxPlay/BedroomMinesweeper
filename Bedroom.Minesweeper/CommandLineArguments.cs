using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bedroom.Minesweeper
{
    public static class CommandLineArguments
    {
        private static Dictionary<string, ArgumentData> argumentReflection;

        /// <summary>
        /// Do not use the log file
        /// </summary>
        [Argument("-nolog")]
        public static bool NoLog { get; set; } = false;

        /// <summary>
        /// The height of the window
        /// </summary>
        [Argument("-windowheight", typeof(int))]
        [Argument("-height", typeof(int))]
        public static int WindowHeight { get; set; } = 400;

        /// <summary>
        /// The width of the window
        /// </summary>
        [Argument("-windowwidth", typeof(int))]
        [Argument("-width", typeof(int))]
        public static int WindowWidth { get; set; } = 600;

        static CommandLineArguments()
        {
            LoadArguments();
            string[] args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length; i++)
            {
                if (argumentReflection.ContainsKey(args[i]))
                {
                    ArgumentData argument = argumentReflection[args[i]];
                    if (argument.HasParameter)
                    {
                        if (++i < args.Length)
                            ApplyArgument(argument, args[i]);
                        continue;
                    }
                    ApplyArgument(argument);
                }
            }
        }

        private static void ApplyArgument(ArgumentData argument, string parameter = null)
        {
            if (!argument.HasParameter)
            {
                argument.Property.SetValue(null, true);
                return;
            }

            MethodInfo parseMethod = argument.ParameterType.GetMethod("Parse", new Type[] { typeof(string) });
            if (parseMethod == null)
                return;

            object result = parseMethod.Invoke(null, new object[] { parameter });
            argument.Property.SetValue(null, result);
        }

        private static void LoadArguments()
        {
            argumentReflection = new Dictionary<string, ArgumentData>();
            PropertyInfo[] properties = typeof(CommandLineArguments).GetProperties(BindingFlags.Static | BindingFlags.Public);
            for (int i = 0; i < properties.Length; i++)
            {
                foreach (var attribute in properties[i].GetCustomAttributes<ArgumentAttribute>())
                {
                    if (argumentReflection.ContainsKey(attribute.Argument))
                    {
                        Debug.LogError($"Argument {attribute.Argument} defined multiple times");
                        continue;
                    }
                    argumentReflection.Add(attribute.Argument, new ArgumentData(attribute, properties[i]));
                }
            }
        }

        [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
        private class ArgumentAttribute : Attribute
        {
            public ArgumentAttribute(string argument)
            {
                Argument = argument;
                HasParameter = false;
                ParameterType = null;
            }

            public ArgumentAttribute(string argument, Type parameterType)
            {
                Argument = argument;
                HasParameter = true;
                ParameterType = parameterType;
            }

            public string Argument;
            public bool HasParameter;
            public Type ParameterType;
        }

        private class ArgumentData
        {
            public PropertyInfo Property;
            public string Argument;
            public bool HasParameter;
            public Type ParameterType;

            public ArgumentData(ArgumentAttribute attribute, PropertyInfo property)
            {
                Property = property;
                Argument = attribute.Argument;
                HasParameter = attribute.HasParameter;
                ParameterType = attribute.ParameterType;
            }
        }
    }
}
