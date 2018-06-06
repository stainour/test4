using Castle.Facilities.TypedFactory;
using System.Reflection;

namespace ApplicationServices.IoC
{
    internal class NamedSelector : DefaultTypedFactoryComponentSelector
    {
        protected override string GetComponentName(MethodInfo method, object[] arguments)
        {
            if (arguments.Length > 0 && arguments[0] is string)
            {
                return arguments[0].ToString();
            }

            return base.GetComponentName(method, arguments);
        }
    }
}