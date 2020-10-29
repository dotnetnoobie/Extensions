using System;
using System.Linq.Expressions;

namespace Microsoft.JSInterop
{
    internal static class StorageExtensions
    {
        internal static string GetKey<T>(this Expression<Func<T>> expression)
        {
            var member = ((MemberExpression)expression.Body).Member;
            var name = member.Name;
            var key = member.DeclaringType.FullName + "." + name;

            return key;
        }

        internal static T GetValue<T>(this Expression<Func<T>> expression)
        {
            var value = expression.Compile()();

            return value;
        }
    }
}
