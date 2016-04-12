using System.Collections.Generic;
using System.Linq.Expressions;

namespace Neptune.DataAccess.Specification.Utils
{
	/// <summary>
	/// Utility class for combining expressions.
	/// </summary>
	/// <remarks>
	/// See http://blogs.msdn.com/b/meek/archive/2008/05/02/linq-to-entities-combining-predicates.aspx
	/// </remarks>
	public class ParameterRebinder : ExpressionVisitor
	{
		private readonly Dictionary<ParameterExpression, ParameterExpression> map;

		/// <summary>
		/// Initialises a new instance of the <see cref="ParameterRebinder"/> class.
		/// </summary>
		/// <param name="map">The map.</param>
		public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
		{
			this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
		}

		/// <summary>
		/// Replaces the parameters.
		/// </summary>
		/// <param name="map">The map.</param>
		/// <param name="expression">The expression.</param>
		/// <returns>
		/// Rebound expression.
		/// </returns>
		public static Expression ReplaceParameters(
			Dictionary<ParameterExpression, ParameterExpression> map,
			Expression expression)
		{
			return new ParameterRebinder(map).Visit(expression);
		}

		/// <summary>
		/// Visits the <see cref="ParameterExpression" />.
		/// </summary>
		/// <param name="node">The expression to visit.</param>
		/// <returns>The modified expression, if it or any sub-expression was modified;
		/// otherwise, returns the original expression.</returns>
		protected override Expression VisitParameter(ParameterExpression node)
		{
			ParameterExpression replacement;
			if (map.TryGetValue(node, out replacement))
			{
				node = replacement;
			}

			return base.VisitParameter(node);
		}
	}
}
