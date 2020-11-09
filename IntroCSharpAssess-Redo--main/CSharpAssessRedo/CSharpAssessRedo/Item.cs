namespace CSharpAssessRedo
{
    internal class Item
    {
        //itemID, Type, Name
        private int itemId;
        private string itemType;
        private string itemName;
        private int itemCost;

        public int ItemId { get => itemId; set => itemId = value; }
        public string ItemType { get => itemType; set => itemType = value; }
        public string ItemName { get => itemName; set => itemName = value; }
        public int ItemCost { get => itemCost; set => itemCost = value; }

        public Item(int itemId, string itemType, string itemName, int itemCost)
        {
            ItemId = itemId;
            ItemType = itemType;
            ItemName = itemName;
            ItemCost = itemCost;
        }

        public virtual void PrintDetails()
        {
            Util.Prompt($"{itemId}, {itemType}, {itemName}, {itemCost}");
        }
    }
}