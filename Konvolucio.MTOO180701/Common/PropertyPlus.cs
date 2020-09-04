namespace Konvolucio.MTOO180701.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ComponentModel;
    using System.Linq.Expressions;

    public static class PropertyPlus
    {
        /// <summary>
        /// Tulajdonság nevének megszerzése, még a tulajdonságban
        /// Típikus haszánlata:
        /// Minőségi szoftver estén használd!!!!
        /// <code> 
        /// set 
        /// { 
        ///     if (_year != value) 
        ///     {
        ///        _year = value; 
        ///        OnPropertyChanged(GetPropertyName(() => Year)); 
        ///     } 
        /// }</code>
        /// 
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="propertyId"></param>
        /// <returns></returns>
        public static String GetPropertyName<TValue>(Expression<Func<TValue>> propertyId)
        {
            return ((MemberExpression)propertyId.Body).Member.Name;
        }

        /// <summary>
        /// Tulajodságok értékének megszerzése objektumból
        /// </summary>
        /// <param name="src"></param>
        /// <param name="propName"></param>
        /// <returns></returns>
        public static object GetValue(object src, string propName)
        {

            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        /// <summary>
        /// Tulajdonságok neveinek megszerzése objektumból
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static List<string> GetNames(Type t)
        {
            List<string> names = t.GetProperties().Select(n => n.Name).ToList<string>();
            return names;
        }

        /// <summary>
        /// Tulajdonság típusának megszerése objektumból
        /// </summary>
        /// <param name="t"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static Type GetType(Type t, string propertyName)
        {
            Type retval = t.GetProperties().First(n => n.Name == propertyName).PropertyType;
            return retval;
        }

        public static string GetPropertyDescription(object obj, string propertyName)
        {
            return obj.GetType()
                    .GetProperties()
                        .FirstOrDefault(n => n.Name == propertyName)
                            .GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), true)
                                .Cast<System.ComponentModel.DescriptionAttribute>()
                                    .SingleOrDefault()
                                        .Description;

        }
    }
}
