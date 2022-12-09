//using Microsoft.AspNetCore.OData.Query.Expressions;
//using Microsoft.OData.UriParser;
//using ODataDemoProject.Models;
//using System.Linq.Expressions;

//namespace ODataDemoProject.SearchFilters
//{
//    public class ProductSaleSearchBinder : QueryBinder, ISearchBinder
//    {
//        public Expression BindSearch(SearchClause searchClause, QueryBinderContext context)
//        {
//            SearchTermNode node = searchClause.Expression as SearchTermNode;

//            Expression<Func<Customer, bool>> exp = p => p.Name.ToString() == node.Text;

//            return exp;
//        }
//    }
//}
