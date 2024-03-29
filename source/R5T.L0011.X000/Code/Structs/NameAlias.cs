﻿using System;


namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
    /// <summary>
    /// Represents the alias from a source name expression to a destination name.
    /// The destination name is assumed to be unique among aliases (i.e. there will not be multiple source name expressions aliased to the same destination name in any context).
    /// </summary>
    /// <remarks>
    /// * Struct may not have been the best choice vs. class, but this is a simple type.
    /// * Ummutable.
    /// Also see:
    /// * R5T.T0129.NameAlias (more general name alias).
    /// </remarks>
    public struct NameAlias : IEquatable<NameAlias>, IComparable<NameAlias>
    {
        #region Static

        public static NameAlias From(
            string destinationName,
            string sourceNameExpression)
        {
            var output = new NameAlias(
                destinationName,
                sourceNameExpression);

            return output;
        }

        public static bool operator ==(NameAlias left, NameAlias right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(NameAlias left, NameAlias right)
        {
            return !(left == right);
        }

        #endregion


        public string DestinationName { get; }
        public string SourceNameExpression { get; }


        public NameAlias(
            string destinationName,
            string sourceNameExpression)
        {
            this.DestinationName = destinationName;
            this.SourceNameExpression = sourceNameExpression;
        }

        public bool Equals(NameAlias other)
        {
            var output = true
                && this.DestinationName == other.DestinationName
                && this.SourceNameExpression == other.SourceNameExpression;

            return output;
        }

        public override bool Equals(object obj)
        {
            var output = obj is NameAlias nameAlias && Equals(nameAlias);
            return output;
        }

        public override int GetHashCode()
        {
            // Only use the destination name, since compiler error CS1537 ensures the alias will be unique per namespace within a compilation unit.
            var output = this.DestinationName.GetHashCode();
            return output;
        }

        public int CompareTo(NameAlias other)
        {
            var destinationNameComparison = this.DestinationName.CompareTo(other.DestinationName);
            if(ComparisonHelper.IsNotEqualResult(destinationNameComparison))
            {
                return destinationNameComparison;
            }

            var sourceNameExpressionComparison = this.SourceNameExpression.CompareTo(other.SourceNameExpression);
            return sourceNameExpressionComparison;
        }
    }
}
