using System;
using System.Collections.Generic;
using System.Reflection;
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


    public virtual DbSet<SettingsDbModel> Settings { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite(@"DataSource=" + ProjectSettings.GlobalizePath("res://") + "Db/Db.sqlite;");
        }
    }

    public object Get(string s, int rowNumb = 0)
    {
        try
        {
            List<SettingsDbModel> list = this.Settings.ToListAsync<SettingsDbModel>().Result;
            PropertyInfo property = list[rowNumb].GetType().GetProperty(s);
            return property.GetValue(list[rowNumb]);
        }
        catch (Exception e)
        {
            return null;
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

    public async Task<SettingsDbContext> Delete(SettingsDbModel settings)
    {
        Remove(settings);
        await this.SaveChangesAsync();
        return this;
    }

    public async Task<SettingsDbContext> UpdateSound(int rowNumb = 1, float? musicV = null, float? uIV = null, float? tranV = null)
    {
        SettingsDbModel settings = this.Settings.Find(rowNumb);

        if (musicV is float musicVFloat)
        {
            settings.MusicVolume = musicVFloat;
        }

        if (uIV is float uIVFloat)
        {
            settings.UIVolume = uIVFloat;
        }

        if (tranV is float tranVFloat)
        {
            settings.TransitionVolume = tranVFloat;
        }

        await this.SaveChangesAsync();
        return this;
    }

    public async Task<SettingsDbContext> UpdateActionEvent(string action, string @event, int rowNumb = 1)
    {
        SettingsDbModel settings = this.Settings.Find(rowNumb);

        var propertyInfo = typeof(SettingsDbModel).GetProperty(action);

        propertyInfo.SetValue(settings, @event);

        await this.SaveChangesAsync();
        return this;
    }

}