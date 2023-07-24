using System.Collections.Generic;

namespace JustMobyTestTask
{
    public class ItemsCountUpdatedSignal
    {
        public IReadOnlyList<ItemData> ItemDatas { get; }

        public ItemsCountUpdatedSignal(IReadOnlyList<ItemData> itemDatas) => ItemDatas = itemDatas;
    }
}
