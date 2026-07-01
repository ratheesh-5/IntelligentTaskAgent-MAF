using System;
using System.Collections.Generic;
using System.Text;
using IntelligentTaskAgent.Core.Domain.Entities;
using IntelligentTaskAgent.Core.Interfaces;
using IntelligentTaskAgent.Application.Interfaces;
using System.Xml;

namespace IntelligentTaskAgent.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IUserNotificationChannelRepository channelRepository;
        public UserService(IUserRepository userRepository
            , IUserNotificationChannelRepository channelRepository)
        {
            this.userRepository = userRepository;
            this.channelRepository = channelRepository;
        }
        public async Task<Guid> CreateAsync(string? name)
        {
            var user = new User
            {

                UserId = Guid.NewGuid(),
                Name = name,
                CreatedAt = DateTime.UtcNow
            };

            await userRepository.AddAsync(user);
            return user.UserId;
        }
        public async Task<User> CreateIfNotExistsAsync(Guid userId, string? name)
        {
            var existing = await userRepository.GetByIdAsync(userId);
            if (existing != null)
                return existing;

            var user = new User
            {
                UserId = userId,
                Name = name,
                CreatedAt = DateTime.UtcNow
            };

            await userRepository.AddAsync(user);
            return user;
        }

        public async Task<Guid> FindOrCreateByEmailAsync(string email)
        {
            var existingUser = await this.userRepository.GetByEmailAsync(email);

            if (existingUser != null)
            {
                return existingUser.UserId;
            }

            var newUser = new User
            {
                UserId = Guid.NewGuid(),
                Email = email,
                Name = email,
                CreatedAt = DateTime.UtcNow
            };

            await this.userRepository.AddAsync(newUser);

            return newUser.UserId;
        }

        public async Task<Guid> FindOrCreateByTelegramAsync(long chatId, CancellationToken ct = default)
        {
            var channelValue = chatId.ToString();

            // 🔍 1️⃣ Check existing Telegram channel
            var existingChannel =
                await this.channelRepository.GetByChannelAsync(
                    channel: "Telegram",
                    channelValue: channelValue);

            if (existingChannel != null)
            {
                return existingChannel.UserId;
            }

            // 🆕 2️⃣ Create new user
            var user = new User
            {
                UserId = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow
            };

            await userRepository.AddAsync(user);

            // 🔗 3️⃣ Create Telegram notification channel
            var telegramChannel = new UserNotificationChannel
            {
                ChannelId = Guid.NewGuid(),
                UserId = user.UserId,
                Channel = "Telegram",
                ChannelValue = channelValue,
                IsPrimary = true,
                IsEnabled = true,
                CreatedAt = DateTime.UtcNow
            };

            await this.channelRepository.AddAsync(telegramChannel);

            return user.UserId;
        }
    }
}
