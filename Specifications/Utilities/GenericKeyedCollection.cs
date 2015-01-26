namespace Specifications
{
    using System;
    using System.Collections.ObjectModel;

    public class GenericKeyedCollection<TKey, TItem> : KeyedCollection<TKey, TItem>
    {
        private readonly Func<TItem, TKey> keySelector;

        public GenericKeyedCollection(Func<TItem, TKey> keySelector)
        {
            this.keySelector = keySelector;
        }

        protected override TKey GetKeyForItem(TItem item)
        {
            return this.keySelector(item);
        }
    }
}