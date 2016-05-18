using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain
{
    public class MultiLangString
    {
        [Display(ResourceType = typeof (Resources.Domain), Name = "EntityPrimaryKey")]
        public int MultiLangStringId { get; set; }

        /// <summary>
        /// Default value, when translation is not found
        /// </summary>
        [MaxLength(40960)]
        public string Value { get; set; }

        [MaxLength(255)]
        public string Owner { get; set; }

        // list of translations
        public virtual ICollection<Translation> Translations { get; set; } = new List<Translation>();

        #region Constructors

        public MultiLangString()
        {
        }

        public MultiLangString(string defaultStringValue)
        {
            Value = defaultStringValue;
        }

        public MultiLangString(string stringValue, string culture)
        {
            SetTranslation(stringValue, culture);
        }

        public MultiLangString(string stringValue, string culture, string defaultStringValue)
        {
            Value = defaultStringValue;
            SetTranslation(stringValue, culture);
        }

        public MultiLangString(string stringValue, string culture, string defaultStringValue, string owner)
        {
            Value = defaultStringValue;
            SetTranslation(stringValue, culture);
            Owner = owner;
        }

        #endregion

        public void SetTranslation(string stringValue, string culture)
        {
            if (Translations == null) Translations = new List<Translation>();
            // this could be better? how to mix and match?
            // en, en-us, en-gb
            var found = Translations.FirstOrDefault(a => culture.ToUpper().StartsWith(a.Culture.ToUpper()));
            if (found == null)
            {
                Translations.Add(new Translation
                {
                    Culture = culture,
                    Value = stringValue
                });
            }
            else
            {
                found.Value = stringValue;
            }
        }

        public void SetTranslation(string stringValue, string culture, string owner)
        {
            Owner = owner;
            SetTranslation(stringValue, culture);
        }

        public string Translate(string cultureName = "")
        {
            return TranslateToCulture(String.IsNullOrWhiteSpace(cultureName) ? null : new CultureInfo(cultureName));
        }

        public string TranslateToCulture(CultureInfo culture = null)
        {
            if (culture == null) culture = Thread.CurrentThread.CurrentCulture;
            var translation = Translations.FirstOrDefault(a => culture.Name.ToUpper().StartsWith(a.Culture.ToUpper()));
            return translation == null ? Value : translation.Value;
        }

        public override string ToString()
        {
            return TranslateToCulture();
        }
    }
}