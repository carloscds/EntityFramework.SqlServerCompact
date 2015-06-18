﻿using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.FunctionalTests;
using Microsoft.Framework.DependencyInjection;

namespace ErikEJ.Data.Entity.SqlServerCe.FunctionalTests
{
    public abstract class GraphUpdatesSqlServerTestBase<TFixture> : GraphUpdatesTestBase<SqlCeTestStore, TFixture>
        where TFixture : GraphUpdatesSqlServerTestBase<TFixture>.GraphUpdatesSqlServerFixtureBase, new()
    {
        protected GraphUpdatesSqlServerTestBase(TFixture fixture)
            : base(fixture)
        {
        }

        public abstract class GraphUpdatesSqlServerFixtureBase : GraphUpdatesFixtureBase
        {
            private readonly IServiceProvider _serviceProvider;

            protected GraphUpdatesSqlServerFixtureBase()
            {
                _serviceProvider = new ServiceCollection()
                    .AddEntityFramework()
                    .AddSqlCe()
                    .ServiceCollection()
                    .AddSingleton(TestSqlCeModelSource.GetFactory(OnModelCreating))
                    .BuildServiceProvider();
            }

            protected abstract string DatabaseName { get; }

            public override SqlCeTestStore CreateTestStore()
            {
                return SqlCeTestStore.GetOrCreateShared(DatabaseName, () =>
                {
                    var optionsBuilder = new DbContextOptionsBuilder();
                    optionsBuilder.UseSqlCe(SqlCeTestStore.CreateConnectionString(DatabaseName));

                    using (var context = new GraphUpdatesContext(_serviceProvider, optionsBuilder.Options))
                    {
                        context.Database.EnsureDeleted();
                        if (context.Database.EnsureCreated())
                        {
                            Seed(context);
                        }
                    }
                });
            }

            public override DbContext CreateContext(SqlCeTestStore testStore)
            {
                var optionsBuilder = new DbContextOptionsBuilder();
                optionsBuilder.UseSqlCe(testStore.Connection);

                var context = new GraphUpdatesContext(_serviceProvider, optionsBuilder.Options);
                context.Database.AsRelational().Connection.UseTransaction(testStore.Transaction);
                return context;
            }
        }
    }
}
