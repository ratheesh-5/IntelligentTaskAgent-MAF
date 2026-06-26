using IntelligentTaskAgent.Core.Domain;
using IntelligentTaskAgent.Core.Domain.Entities;
using IntelligentTaskAgent.Notifications.Domain.Entities;
using IntelligentTaskAgent.Repositories.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligentTaskAgent.Repositories.DbContexts
{
    public class IntelligentTaskAgentDbContext : DbContext
    {
        private readonly DatabaseOptions dataBaseOptions;

        public IntelligentTaskAgentDbContext(
            DbContextOptions<IntelligentTaskAgentDbContext> options
            , IOptions<DatabaseOptions> dataBaseOptions)
            : base(options)
        {
            this.dataBaseOptions = dataBaseOptions.Value;
        }

        public DbSet<TaskEntity> Tasks => Set<TaskEntity>();
        public DbSet<ReminderEntity> Reminders => Set<ReminderEntity>();
        public DbSet<AgentExecutionLogEntity> AgentExecutionLogs => Set<AgentExecutionLogEntity>();

        public DbSet<UserNotificationChannel> UserNotificationChannels => Set<UserNotificationChannel>();

        public DbSet<User> Users => Set<User>();
        public DbSet<NotificationLog> NotificationLog => Set<NotificationLog>();
        public DbSet<TelegramOnboardingSession> TelegramOnboardingSession => Set<TelegramOnboardingSession>();

        public DbSet<TelegramConversationStateEntity> TelegramConversationStates
                                => Set<TelegramConversationStateEntity>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskEntity>(entity =>
            {
                entity.ToTable("Tasks");
                entity.HasKey(x => x.TaskId);
            });

            modelBuilder.Entity<ReminderEntity>(entity =>
            {
                entity.ToTable("Reminders");
                entity.HasKey(x => x.ReminderId);
            });

            modelBuilder.Entity<AgentExecutionLogEntity>(entity =>
            {
                entity.ToTable("AgentExecutionLogs");
                entity.HasKey(x => x.LogId);
            });

            modelBuilder.Entity<UserNotificationChannel>(entity =>
            {
                entity.ToTable("UserNotificationChannels");
                entity.HasKey(x => x.ChannelId);

                entity.Property(x => x.Channel)
                      .HasConversion<string>(); // stores "Email", "Telegram", etc.

                entity.Property(x => x.ChannelValue).HasMaxLength(255);
            });

            modelBuilder.Entity<User>()
                    .HasKey(x => x.UserId);

            modelBuilder.Entity<NotificationLog>(entity =>
            {
                entity.ToTable("NotificationLogs");
                entity.HasKey(x => x.NotificationLogId);
            });

            modelBuilder.Entity<TelegramOnboardingSession>(entity =>
            {
                entity.ToTable("TelegramOnboardingSessions");
                entity.HasKey(x => x.ChatId);
            });

            modelBuilder.Entity<TelegramConversationStateEntity>(entity =>
            {
                entity.ToTable("TelegramConversationStates");

                entity.HasKey(x => x.ChatId);

                entity.Property(x => x.StateJson)
                    .IsRequired();

                entity.Property(x => x.UpdatedAtUtc)
                    .IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = this.dataBaseOptions.SQLConnectionString;

                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }

}
