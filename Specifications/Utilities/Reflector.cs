namespace Specifications.Utilities
{
    using Seterlund.CodeGuard;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class Reflector<TTarget>
    {
        private const string EnumerationValueNotConstant = "Specified enumeration value is not a constant expression.";
        private const string MemberIsNotAccess = "Specified member is not a member access expression.";
        private const string MemberIsNotField = "Specified member is not a field.";
        private const string MemberIsNotProperty = "Specified member is not a property.";
        private const string MethodIsNotCall = "Specified method is not a method call expression.";

        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Required for simplified usage of methods.")]
        public static MethodInfo GetMethod(Expression<Action<TTarget>> methodSelector)
        {
            return GetMethodInfo(methodSelector);
        }

        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Required for simplified usage of methods.")]
        public static MethodInfo GetMethod<T1>(Expression<Action<TTarget, T1>> methodSelector)
        {
            return GetMethodInfo(methodSelector);
        }

        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Required for simplified usage of methods.")]
        public static MethodInfo GetMethod<T1, T2>(Expression<Action<TTarget, T1, T2>> methodSelector)
        {
            return GetMethodInfo(methodSelector);
        }

        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Required for simplified usage of methods.")]
        public static MethodInfo GetMethod<T1, T2, T3>(Expression<Action<TTarget, T1, T2, T3>> methodSelector)
        {
            return GetMethodInfo(methodSelector);
        }

        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Required for simplified usage of methods.")]
        public static string GetPropertyName<TResult>(Expression<Func<TTarget, TResult>> propertySelector)
        {
            return GetProperty(propertySelector).Name;
        }

        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Required for simplified usage of methods.")]
        public static PropertyInfo GetProperty<TResult>(Expression<Func<TTarget, TResult>> propertySelector)
        {
            var info = GetMemberInfo(propertySelector) as PropertyInfo;
            Guard.That(info)
                .IsTrue(x => x != null, MemberIsNotProperty);

            return info;
        }

        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Required for simplified usage of methods.")]
        public static FieldInfo GetField<TResult>(Expression<Func<TTarget, TResult>> fieldSelector)
        {
            var info = GetMemberInfo(fieldSelector) as FieldInfo;
            Guard.That(info)
                .IsTrue(x => x != null, MemberIsNotField);

            return info;
        }

        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Required for simplified usage of methods.")]
        public static FieldInfo GetEnumerationValue<TResult>(Expression<Func<TResult>> enumerationValueSelector)
        {
            return GetEnumInfo(enumerationValueSelector);
        }

        private static MethodInfo GetMethodInfo(LambdaExpression methodSelector)
        {
            Guard.That(methodSelector.Body.NodeType)
                .IsTrue(x => x == ExpressionType.Call, MethodIsNotCall);

            var callExpression = (MethodCallExpression)methodSelector.Body;
            return callExpression.Method;
        }

        private static MemberInfo GetMemberInfo(LambdaExpression memberSelector)
        {
            Guard.That(memberSelector.Body.NodeType)
                .IsTrue(x => x == ExpressionType.MemberAccess, MemberIsNotAccess);

            var memberExpression = (MemberExpression)memberSelector.Body;
            return memberExpression.Member;
        }

        private static FieldInfo GetEnumInfo(LambdaExpression enumerationValueSelector)
        {
            Guard.That(enumerationValueSelector.Body.NodeType)
                    .IsTrue(x => x == ExpressionType.Constant, EnumerationValueNotConstant);

            var constantExpression = (ConstantExpression)enumerationValueSelector.Body;

            string enumName = constantExpression.Type.GetEnumName(constantExpression.Value);
            return constantExpression.Type.GetField(enumName);
        }
    }
}