using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Wings.Examples.UseCase.Server
{  
    public class AsyncQueryAttribute : ActionFilterAttribute
    {
        //是否返回单个值
        private readonly bool _single;

        public AsyncQueryAttribute(bool single = false)
        {
            _single = single;
        }

        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var objectResult = context.Result as ObjectResult;
            if (objectResult?.Value is IQueryable queryable)
            {
                var result = queryable.Cast<object>();
                var singleData = _single ? await result.FirstOrDefaultAsync() : null;
                var listData = _single ? null : await result.ToListAsync();

                //分页:
                //如果配置了$count=true则OData会自动计算(TotalCount != null)
                //这个时候要返回count
                var count = context.HttpContext.Request.HttpContext.ODataFeature()?.TotalCount;
                if (count != null)
                {
                    if (_single)
                    {
                        context.Result = new ObjectResult(new
                        {
                            count,
                            listData = singleData
                        });
                    }
                    else
                    {
                        context.Result = new ObjectResult(new
                        {
                            count,
                            data = listData
                        });
                    }
                }
                else
                {
                    context.Result = _single ? new ObjectResult(singleData) : new ObjectResult(listData);
                    //单个返回值的情况下，如果不存在则返回404
                    if (_single && singleData == null)
                    {
                        context.Result = new NotFoundResult();
                    }
                }
            }

            await base.OnResultExecutionAsync(context, next);
        }
    }

}