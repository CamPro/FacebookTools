using System;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace KSS.Patterns.Extensions
{
    public static class ControlExtensions
    {
        public static Control GetTopParent(this Control control)
        {
            if (control == null)
                return null;

            if (control.Parent == null)
                return control;

            return GetTopParent(control.Parent);
        }

        public static void SafeInvoke(this Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action.DynamicInvoke();
            }
        }

        public static T SafeInvoke<T>(this Control control, Func<T> func)
        {
            if (control.InvokeRequired)
            {
                return (T)control.Invoke(func);
            }

            return func();
        }

        public static Binding AddExt<TProperty, TMember>(this ControlBindingsCollection bindings,
            Expression<Func<TProperty>> propertyNameExpression, object dataSource,
            Expression<Func<TMember>> dataMemberExpression)
        {
            var propertyName = GetPropertyName(propertyNameExpression);
            var dataMember = GetPropertyName(dataMemberExpression);

            return bindings.Add(propertyName, dataSource, dataMember);
        }

        private static string GetPropertyName<T>(Expression<Func<T>> expression)
        {
            MemberExpression body = expression.Body as MemberExpression;
            if (body == null)
            {
                throw new ArgumentException("expression");
            }
            return body.Member.Name;
        }
    }
}