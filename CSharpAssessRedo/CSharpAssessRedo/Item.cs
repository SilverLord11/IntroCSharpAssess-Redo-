namespace CSharpAssessRedo
{
    internal class Item
    {
        //itemID, Type, Name
        private int itemId;
        private string itemType;
        private string itemName;

        public int ItemId { get => itemId; set => itemId = value; }
        public string ItemType { get => itemType; set => itemType = value; }
        public string ItemName { get => itemName; set => itemName = value; }

        public Item()
        {

        }

        public Item(int itemId, string itemType, string itemName)
        {
            ItemId = itemId;
            ItemType = itemType;
            ItemName = itemName;
        }

        public virtual void PrintDetails()
        {
            Util.Prompt($"{itemId}, {itemType}, {itemName}");
        }
    }
}