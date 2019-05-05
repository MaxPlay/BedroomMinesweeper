using System;
using System.Collections.Generic;
using System.Reflection;

namespace Bedroom.Minesweeper
{
    public static class CommandLineArguments
    {
        #region Private Fields

        private static Dictionary<string, ArgumentData> argumentReflection;
        private static bool loaded;

        #endregion Private Fields

        #region Public Properties

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

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Loads the command line arguments (if they aren't already loaded).
        /// </summary>
        public static void Load()
        {
            if (loaded)
                return;
            loaded = true;

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

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Sets a variable to a value given as "parameter". If the argument has no parameter, the
        /// "parameter"-parameter will be ignored and thus is assigned null by default. Note that
        /// when no parameter is given to a variable, bool is assumed and set to true.
        /// </summary>
        /// <param name="argument">The argument data that comes from the reflection</param>
        /// <param name="parameter">The value that is assigned</param>
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

        /// <summary>
        /// Creates the argumentReflection dictionary and fills it with arguments via reflection.
        /// </summary>
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

        #endregion Private Methods

        #region Private Classes

        /// <summary>
        /// An attribute for properties that allows them to be assigned by the user
        /// </summary>
        [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
        private class ArgumentAttribute : Attribute
        {
            #region Public Fields

            public string Argument;

            public bool HasParameter;

            public Type ParameterType;

            #endregion Public Fields

            #region Public Constructors

            /// <summary>
            /// Gives the property a single argument without a parameter
            /// </summary>
            /// <param name="argument">The argument that needs to be passed in</param>
            public ArgumentAttribute(string argument)
            {
                Argument = argument;
                HasParameter = false;
                ParameterType = null;
            }

            /// <summary>
            /// Gives the property a single argument with a parameter of a given type
            /// </summary>
            /// <param name="argument">The argument that needs to be passed in</param>
            /// <param name="parameterType">
            /// The type that should be casted to. Note: This should be a numerical struct (like int
            /// or float) or a string. Everything else may not be parsable
            /// </param>
            public ArgumentAttribute(string argument, Type parameterType)
            {
                Argument = argument;
                HasParameter = true;
                ParameterType = parameterType;
            }

            #endregion Public Constructors
        }

        /// <summary>
        /// Transport class to get data from the analysis via reflection to the actual evaluation and assignment
        /// </summary>
        private class ArgumentData
        {
            #region Public Fields

            public string Argument;
            public bool HasParameter;
            public Type ParameterType;
            public PropertyInfo Property;

            #endregion Public Fields

            #region Public Constructors

            /// <summary>
            /// Constructor that makes assignment easier (so you do not have to pass everything in.
            /// </summary>
            /// <param name="attribute">The attribute of the property</param>
            /// <param name="property">The property itself</param>
            public ArgumentData(ArgumentAttribute attribute, PropertyInfo property)
            {
                Property = property;
                Argument = attribute.Argument;
                HasParameter = attribute.HasParameter;
                ParameterType = attribute.ParameterType;
            }

            #endregion Public Constructors
        }

        #endregion Private Classes
    }
}

// Yes, I am aware that some comments may be... sarcastic.