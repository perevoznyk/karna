using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Karna.Windows.UI.Attributes
{
    /// <summary>
    /// Karna specific purpose of the class
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    [Description("Karna specific purpose of the class")]
    public class KarnaPurposeAttribute : System.Attribute
    {
        private string purpose;

        /// <summary>
        /// Initializes a new instance of the <see cref="KarnaPurposeAttribute"/> class.
        /// </summary>
        /// <param name="purpose">The purpose.</param>
        public KarnaPurposeAttribute(string purpose)
        {
            this.purpose = purpose;
        }

        /// <summary>
        /// Gets the purpose.
        /// </summary>
        /// <value>The purpose.</value>
        public string Purpose
        {
            get { return purpose; }
        }
    }
}
