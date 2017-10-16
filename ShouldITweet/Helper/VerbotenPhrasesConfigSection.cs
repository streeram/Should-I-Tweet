using System.Configuration;

namespace ShouldITweet.Helper
{
    // Class for holding the every element child to the <Values> element contents
    public class ValueElement : ConfigurationElement
    {
        // Property to hold the name attribue value
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string)this["name"]; }
        }
    }

    // Class for holding the <Values> element and its children
    public class ValueElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ValueElement();
        }


        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ValueElement)element).Name;
        }
    }

    // Class to reading the VerbotenPhrasesConfigSection section from web.config
    public class VerbotenPhrasesConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("Values")]
        public ValueElementCollection Values
        {
            get { return (ValueElementCollection)this["Values"]; }
        }
    }
}