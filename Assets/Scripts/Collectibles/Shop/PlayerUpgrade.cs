namespace NR {
    /// <summary>
    /// Base cost for purchaseable upgrades that can be bought with gems.
    /// Currently unimplemented, but would be applied by PlayerSaveLoader.
    /// </summary>
    public class PlayerUpgrade : ShopItem {
        public int gemCost;

        public virtual void ApplyUpgrade() { }
    }
}
