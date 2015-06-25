﻿using System;
using ErikEJ.Data.Entity.SqlServerCe.Metadata;
using ErikEJ.Data.Entity.SqlServerCe.Migrations;
using ErikEJ.Data.Entity.SqlServerCe.Update;
using ErikEJ.Data.Entity.SqlServerCe.ValueGeneration;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata.Builders;
using Microsoft.Data.Entity.Relational;
using Microsoft.Data.Entity.Relational.Metadata;
using Microsoft.Data.Entity.Relational.Migrations.History;
using Microsoft.Data.Entity.Relational.Migrations.Infrastructure;
using Microsoft.Data.Entity.Relational.Migrations.Sql;
using Microsoft.Data.Entity.Relational.Query.Methods;
using Microsoft.Data.Entity.Relational.Update;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.ValueGeneration;

namespace ErikEJ.Data.Entity.SqlServerCe
{
    public class SqlCeDatabaseProviderServices : RelationalDatabaseProviderServices
    {
        public SqlCeDatabaseProviderServices([NotNull] IServiceProvider services)
            : base(services)
        {
        }

        public override IDatabaseConnection Connection => GetService<SqlCeDatabaseConnection>();
        public override IDatabaseCreator Creator => GetService<SqlCeDatabaseCreator>();
        public override IHistoryRepository HistoryRepository => GetService<SqlCeHistoryRepository>();
        public override IMigrationSqlGenerator MigrationSqlGenerator => GetService<SqlCeMigrationSqlGenerator>();
        public override IMigrationAnnotationProvider MigrationAnnotationProvider => GetService<SqlCeMigrationAnnotationProvider>();
        public override IModelSource ModelSource => GetService<SqlCeModelSource>();
        public override IRelationalConnection RelationalConnection => GetService<SqlCeDatabaseConnection>();
        public override ISqlGenerator SqlGenerator => GetService<SqlCeSqlGenerator>();
        public override IDatabase Database => GetService<SqlCeDatabase>();
        public override IValueGeneratorCache ValueGeneratorCache => GetService<SqlCeValueGeneratorCache>();
        public override IRelationalTypeMapper TypeMapper => GetService<SqlCeTypeMapper>();
        public override IModificationCommandBatchFactory ModificationCommandBatchFactory => GetService<SqlCeModificationCommandBatchFactory>();
        public override IRelationalDatabaseCreator RelationalDatabaseCreator => GetService<SqlCeDatabaseCreator>();                
        public override IRelationalMetadataExtensionProvider MetadataExtensionProvider => GetService<SqlCeMetadataExtensionProvider>();
        public override IMethodCallTranslator CompositeMethodCallTranslator => GetService<SqlCeCompositeMethodCallTranslator>();
        public override IMemberTranslator CompositeMemberTranslator => GetService<SqlCeCompositeMemberTranslator>();
    }
}