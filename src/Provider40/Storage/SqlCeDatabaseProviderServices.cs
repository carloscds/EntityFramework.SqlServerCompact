﻿using System;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Update;
using Microsoft.Data.Entity.ValueGeneration;
using Microsoft.Data.Entity.Metadata.Conventions.Internal;
using Microsoft.Data.Entity.MetaData.Conventions.Internal;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Query.ExpressionTranslators;
using Microsoft.Data.Entity.Query.Sql;

namespace Microsoft.Data.Entity.Storage
{
    public class SqlCeDatabaseProviderServices : RelationalDatabaseProviderServices
    {
        public SqlCeDatabaseProviderServices([NotNull] IServiceProvider services)
            : base(services)
        {
        }

        public override string InvariantName => GetType().GetTypeInfo().Assembly.GetName().Name;
        public override IDatabaseCreator Creator => GetService<SqlCeDatabaseCreator>();
        public override IHistoryRepository HistoryRepository => GetService<SqlCeHistoryRepository>();
        public override IMigrationsSqlGenerator MigrationsSqlGenerator => GetService<SqlCeMigrationsSqlGenerator>();
        public override IMigrationsAnnotationProvider MigrationsAnnotationProvider => GetService<SqlCeMigrationsAnnotationProvider>();
        public override IModelSource ModelSource => GetService<SqlCeModelSource>();
        public override IRelationalConnection RelationalConnection => GetService<ISqlCeDatabaseConnection>();
        public override IUpdateSqlGenerator UpdateSqlGenerator => GetService<SqlCeUpdateSqlGenerator>();
        public override IValueGeneratorCache ValueGeneratorCache => GetService<SqlCeValueGeneratorCache>();
        public override IRelationalTypeMapper TypeMapper => GetService<SqlCeTypeMapper>();
        public override IModificationCommandBatchFactory ModificationCommandBatchFactory => GetService<SqlCeModificationCommandBatchFactory>();
        public override IRelationalDatabaseCreator RelationalDatabaseCreator => GetService<SqlCeDatabaseCreator>();
        public override IConventionSetBuilder ConventionSetBuilder => GetService<SqlCeConventionSetBuilder>();
        public override IRelationalMetadataExtensionProvider MetadataExtensionProvider => GetService<SqlCeMetadataExtensionProvider>();
        public override IMethodCallTranslator CompositeMethodCallTranslator => GetService<SqlCeCompositeMethodCallTranslator>();
        public override IMemberTranslator CompositeMemberTranslator => GetService<SqlCeCompositeMemberTranslator>();
        public override IExpressionFragmentTranslator CompositeExpressionFragmentTranslator => GetService<SqlCeCompositeExpressionFragmentTranslator>();
        public override ISqlQueryGeneratorFactory SqlQueryGeneratorFactory => GetService<SqlCeQuerySqlGeneratorFactory>();
    }
}
