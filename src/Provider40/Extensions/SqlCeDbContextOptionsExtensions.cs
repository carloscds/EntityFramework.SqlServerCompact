﻿using System;
using System.Data.Common;
// ReSharper disable once RedundantUsingDirective
using System.Data.SqlServerCe;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Utilities;
using Microsoft.EntityFrameworkCore.Diagnostics;
using EFCore.SqlCe.Infrastructure.Internal;

// ReSharper disable CheckNamespace

namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    ///     SQL Server Compact specific extension methods for <see cref="DbContextOptionsBuilder"/>.
    /// </summary>
    public static class SqlCeDbContextOptionsExtensions
    {
        /// <summary>
        ///     Configures the context to connect to a Microsoft SQL Server Compact database.
        /// </summary>
        /// <param name="optionsBuilder"> The builder being used to configure the context. </param>
        /// <param name="connectionString"> The connection string of the database to connect to. </param>
        /// <param name="sqlCeOptionsAction">An optional action to allow additional SQL Server Compact specific configuration.</param>
        /// <returns> The options builder so that further configuration can be chained. </returns>
        public static DbContextOptionsBuilder UseSqlCe(
            [NotNull] this DbContextOptionsBuilder optionsBuilder,
            [NotNull] string connectionString,
            [CanBeNull] Action<SqlCeDbContextOptionsBuilder> sqlCeOptionsAction = null)
        {
            Check.NotNull(optionsBuilder, nameof(optionsBuilder));
            Check.NotEmpty(connectionString, nameof(connectionString));

            var extension = GetOrCreateExtension(optionsBuilder)
            .WithConnectionString(connectionString);
            //.WithMaxBatchSize(1);
            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);

            ConfigureWarnings(optionsBuilder);

            sqlCeOptionsAction?.Invoke(new SqlCeDbContextOptionsBuilder(optionsBuilder));

            return optionsBuilder;
        }

#if SQLCE35
#else
        /// <summary>
        ///     Configures the context to connect to a Microsoft SQL Server Compact database.
        /// </summary>
        /// <param name="optionsBuilder"> The builder being used to configure the context. </param>
        /// <param name="connectionStringBuilder"> A connection string builder with the connection string of the database to connect to. </param>
        /// <param name="sqlCeOptionsAction">An optional action to allow additional SQL Server Compact specific configuration.</param>
        /// <returns> The options builder so that further configuration can be chained. </returns>
        public static DbContextOptionsBuilder UseSqlCe(
            [NotNull] this DbContextOptionsBuilder optionsBuilder,
            [NotNull] SqlCeConnectionStringBuilder connectionStringBuilder,
            [CanBeNull] Action<SqlCeDbContextOptionsBuilder> sqlCeOptionsAction = null)
        {
            Check.NotNull(optionsBuilder, nameof(optionsBuilder));
            Check.NotNull(connectionStringBuilder, nameof(connectionStringBuilder));

            var extension = GetOrCreateExtension(optionsBuilder)
            .WithConnectionString(connectionStringBuilder.ConnectionString);
            //.WithMaxBatchSize(1);
            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);

            ConfigureWarnings(optionsBuilder);

            sqlCeOptionsAction?.Invoke(new SqlCeDbContextOptionsBuilder(optionsBuilder));

            return optionsBuilder;
        }

        /// <summary>
        ///     Configures the context to connect to a Microsoft SQL Server Compact database.
        /// </summary>
        /// <param name="optionsBuilder"> The builder being used to configure the context. </param>
        /// <param name="connectionStringBuilder"> A connection string builder with the connection string of the database to connect to. </param>
        /// <param name="sqlCeOptionsAction">An optional action to allow additional SQL Server Compact specific configuration.</param>
        /// <returns> The options builder so that further configuration can be chained. </returns>
        public static DbContextOptionsBuilder<TContext> UseSqlCe<TContext>(
            [NotNull] this DbContextOptionsBuilder<TContext> optionsBuilder,
            [NotNull] SqlCeConnectionStringBuilder connectionStringBuilder,
            [CanBeNull] Action<SqlCeDbContextOptionsBuilder> sqlCeOptionsAction = null)
            where TContext : DbContext
            => (DbContextOptionsBuilder<TContext>)UseSqlCe(
                (DbContextOptionsBuilder)optionsBuilder, connectionStringBuilder, sqlCeOptionsAction);
#endif
        /// <summary>
        ///     Configures the context to connect to a Microsoft SQL Server Compact database.
        /// </summary>
        /// <param name="optionsBuilder"> The builder being used to configure the context. </param>
        /// <param name="connection">
        ///     An existing <see cref="DbConnection" /> to be used to connect to the database. If the connection is
        ///     in the open state then EF will not open or close the connection. If the connection is in the closed
        ///     state then EF will open and close the connection as needed.
        /// </param>
        /// <param name="sqlCeOptionsAction">An optional action to allow additional SQL Server Compact specific configuration.</param>
        /// <returns> The options builder so that further configuration can be chained. </returns>
        public static DbContextOptionsBuilder UseSqlCe(
            [NotNull] this DbContextOptionsBuilder optionsBuilder,
            [NotNull] DbConnection connection,
            [CanBeNull] Action<SqlCeDbContextOptionsBuilder> sqlCeOptionsAction = null)
        {
            Check.NotNull(optionsBuilder, nameof(optionsBuilder));
            Check.NotNull(connection, nameof(connection));

            var extension = GetOrCreateExtension(optionsBuilder)
            .WithConnection(connection);
            //.WithMaxBatchSize(1);
            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);

            ConfigureWarnings(optionsBuilder);

            sqlCeOptionsAction?.Invoke(new SqlCeDbContextOptionsBuilder(optionsBuilder));

            return optionsBuilder;
        }

        /// <summary>
        ///     Configures the context to connect to a Microsoft SQL Server database.
        /// </summary>
        /// <param name="optionsBuilder"> The builder being used to configure the context. </param>
        /// <param name="connectionString"> The connection string of the database to connect to. </param>
        /// <param name="sqlCeOptionsAction">An optional action to allow additional SQL Server specific configuration.</param>
        /// <returns> The options builder so that further configuration can be chained. </returns>
        public static DbContextOptionsBuilder<TContext> UseSqlCe<TContext>(
            [NotNull] this DbContextOptionsBuilder<TContext> optionsBuilder,
            [NotNull] string connectionString,
            [CanBeNull] Action<SqlCeDbContextOptionsBuilder> sqlCeOptionsAction = null)
            where TContext : DbContext
            => (DbContextOptionsBuilder<TContext>)UseSqlCe(
                (DbContextOptionsBuilder)optionsBuilder, connectionString, sqlCeOptionsAction);

        /// <summary>
        ///     Configures the context to connect to a Microsoft SQL Server Compact database.
        /// </summary>
        /// <param name="optionsBuilder"> The builder being used to configure the context. </param>
        /// <param name="connection">
        ///     An existing <see cref="DbConnection" /> to be used to connect to the database. If the connection is
        ///     in the open state then EF will not open or close the connection. If the connection is in the closed
        ///     state then EF will open and close the connection as needed.
        /// </param>
        /// <param name="sqlCeOptionsAction">An optional action to allow additional SQL Server Compact specific configuration.</param>
        /// <returns> The options builder so that further configuration can be chained. </returns>
        public static DbContextOptionsBuilder<TContext> UseSqlCe<TContext>(
            [NotNull] this DbContextOptionsBuilder<TContext> optionsBuilder,
            [NotNull] DbConnection connection,
            [CanBeNull] Action<SqlCeDbContextOptionsBuilder> sqlCeOptionsAction = null)
            where TContext : DbContext
            => (DbContextOptionsBuilder<TContext>)UseSqlCe(
                (DbContextOptionsBuilder)optionsBuilder, connection, sqlCeOptionsAction);

        private static SqlCeOptionsExtension GetOrCreateExtension(DbContextOptionsBuilder optionsBuilder)
        {
            var existingExtension = optionsBuilder.Options.FindExtension<SqlCeOptionsExtension>();

            return existingExtension != null
                ? new SqlCeOptionsExtension(existingExtension)
                : new SqlCeOptionsExtension();
        }

        private static void ConfigureWarnings(DbContextOptionsBuilder optionsBuilder)
        {
            var coreOptionsExtension
                = optionsBuilder.Options.FindExtension<CoreOptionsExtension>()
                  ?? new CoreOptionsExtension();

            coreOptionsExtension = coreOptionsExtension.WithWarningsConfiguration(
                coreOptionsExtension.WarningsConfiguration.TryWithExplicit(
                    RelationalEventId.AmbientTransactionWarning, WarningBehavior.Throw));

            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(coreOptionsExtension);
        }
    }
}
