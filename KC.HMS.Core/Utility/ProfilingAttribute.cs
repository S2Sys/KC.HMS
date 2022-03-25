//using KingAOP.Aspects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Unity.Interception.InterceptionBehaviors;
using Unity.Interception.PolicyInjection.Pipeline;

namespace KC.HMS.Core.Utility
{

    //https://docs.microsoft.com/en-us/previous-versions/msp-n-p/dn178466(v=pandp.30)#sec15
    //Concept Crosscutting Concerns
    public class ProfilingAspect : NLogBase, IInterceptionBehavior
    {

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {

            string fullyQualifiedName = $"{input.MethodBase.DeclaringType.Namespace}.{input.MethodBase.DeclaringType.Name}.{input.MethodBase.Name}";
            string method = input.MethodBase.Name;
            string arguments = GetArguments(input.Arguments);

            var timer = Stopwatch.StartNew();

            Log.Info($"Input        : {fullyQualifiedName}({arguments})");

            //WriteLog(PrintInputParameters(input.Arguments));

            // Before invoking the method on the original target.   
            //WriteLog(String.Format("Invoking method {0} at {1}", input.MethodBase, DateTime.Now.ToLongTimeString()));
            // Invoke the next behavior in the chain. 
            var result = getNext()(input, getNext);
            // After invoking the method on the original target. 
            if (result.Exception != null)
            {
                //WriteLog(String.Format("Method {0} threw exception {1} at {2}", input.MethodBase, result.Exception.Message, DateTime.Now.ToString()));

                Log.Error($"Message      : {result.Exception.Message}");
                Log.Error($"Stack Trace  : {result.Exception.StackTrace}");
            }
            else
            {

                Log.Info($"Profile      :  Completed on {DateTime.UtcNow} tooks {timer.ElapsedMilliseconds / 1000} sec");
                Log.Info($"Returns      : {JsonConvert.SerializeObject(result.ReturnValue) ?? "VOID"}");

                //WriteLog(String.Format("Method {0} returned {1} at {2}", input.MethodBase, result.ReturnValue, DateTime.Now.ToString()));
            }

            return result;
        }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes.AsEnumerable();
        }


        public bool WillExecute { get { return true; } }

        //private void WriteLog(string message)
        //{
        //    Console.WriteLine("LOG:" + message);
        //}

        private static string GetArguments(IParameterCollection parameters)
        {

            Dictionary<string, object> argumentDictionary = new Dictionary<string, object>();

            for (int i = 0; i < parameters.Count; i++)
            {
                ParameterInfo parameter = parameters.GetParameterInfo(i);
                string name = parameter.Name;
                object obj = parameters[i] ?? "null";
                argumentDictionary.Add(name, obj);
            }


            //if (0 == parameters.Count)
            //{
            //    return " without parameters ";
            //}

            //var parameterString = new StringBuilder();
            //parameterString.Append("(");
            //for (int i = 0; i < parameters.Count; i++)
            //{
            //    ParameterInfo parameter = parameters.GetParameterInfo(i);

            //    parameterString.Append(parameter.Name + " = " + parameters[i]);
            //}
            //parameterString.Append(")");
            return JsonConvert.SerializeObject(argumentDictionary); //parameterString.ToString();
        }
    }
}


