﻿using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace Microsoft.EntityFrameworkCore.Infrastructure
{
    /// <summary>
    ///     <para>
    ///         Allows SQL Server Compact specific configuration to be performed on <see cref="DbContextOptions"/>.
    ///     </para>
    ///     <para>
    ///         Instances of this class are returned from a call to 
    ///         <see cref="SqlCeDbContextOptionsExtensions.UseSqlCe(DbContextOptionsBuilder, string, System.Action{SqlCeDbContextOptionsBuilder})"/>
    ///         and it is not designed to be directly constructed in your application code.
    ///     </para>
    /// </summary>
    public class SqlCeDbContextOptionsBuilder : RelationalDbContextOptionsBuilder<SqlCeDbContextOptionsBuilder, SqlCeOptionsExtension>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SqlCeDbContextOptionsBuilder"/> class.
        /// </summary>
        /// <param name="optionsBuilder"> The options builder. </param>
        public SqlCeDbContextOptionsBuilder([NotNull] DbContextOptionsBuilder optionsBuilder)
            : base(optionsBuilder)
        {
        }

        /// <summary>
        ///     Use forced client evalution for queries generated by the built-in Relational SQL generator,
        ///that are not supported by SQL Server Compact - nested SELECTs in ORDER BY for example
        /// </summary>
        public virtual void UseClientEvalForUnsupportedSqlConstructs(bool clientEvalForUnsupportedSqlConstructs = false) 
            => WithOption(e => e.WithClientEvalForUnsupportedSqlConstructs(clientEvalForUnsupportedSqlConstructs));
    }
}