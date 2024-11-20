[System.Serializable]
public class ItemEntry
{
  public ItemSO item;
  public int amount;

  public ItemEntry(ItemSO item)
  {
    this.item = item;
    this.amount = 1; // 初始数量为 1
  }
}