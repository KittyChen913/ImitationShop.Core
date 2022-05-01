namespace ImitationShop.EFCore.Context;

public partial class ImitationShopDBContext : DbContext
{
    private readonly IConfiguration configuration;

    public ImitationShopDBContext(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public ImitationShopDBContext(IConfiguration configuration, DbContextOptions<ImitationShopDBContext> options)
        : base(options)
    {
        this.configuration = configuration;
    }

    public virtual DbSet<Item> Items { get; set; } = null!;

    // 連接數據庫時要做的一些配置
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    // 配置實體跟數據庫彼此之間的映射關係
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>(entity =>
        {
            entity.Property(e => e.ItemName).HasMaxLength(50);

            entity.Property(e => e.ItemPrice).HasColumnType("decimal(18, 0)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    // 需要額外添加自定義的處理
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
