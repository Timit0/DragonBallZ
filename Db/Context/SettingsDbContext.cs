using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;
using Microsoft.EntityFrameworkCore;

class SettingsDbContext : DbContext
{
    public static SettingsDbContext instance = null;
    protected static readonly object threadSafeLocker = new object();
    private SettingsDbContext() { }
    public static SettingsDbContext Instance
    {
        get
        {
            lock (threadSafeLocker)
            {
                if (instance == null)
                {
                    instance = new SettingsDbContext();
                }
            }
            return instance;
        }
    }


    public virtual DbSet<SettingsDbModel> Settings { get ; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // optionsBuilder.UseSqlite(@"DataSource="+ProjectSettings.GlobalizePath("res://")+"DB/RpgSqlite;");
            optionsBuilder.UseSqlite(@"DataSource="+ProjectSettings.GlobalizePath("res://")+"Db/Db.sqlite;");
        }
    }

    public SettingsDbModel Get(int rowNumb = 0)
    {
        List<SettingsDbModel> list = this.Settings.ToListAsync<SettingsDbModel>().Result;
        return list[rowNumb];
    }

    public async Task<SettingsDbContext> Add(SettingsDbModel settings)
    {
        this.Settings.Add(settings);
        await this.SaveChangesAsync();
        return this;
    }

    public async Task<SettingsDbContext> Delete(SettingsDbModel settings){
        Remove(settings);
        await this.SaveChangesAsync();
        return this;
    }

    public async Task<SettingsDbContext> Update(int rowNumb = 1, float soundV = 888)
    {
        SettingsDbModel settings = this.Settings.Find(rowNumb);

        if(soundV != 888)
        {
            settings.SoundVolume = soundV;
        }

        await this.SaveChangesAsync();
        return this;
    }
}