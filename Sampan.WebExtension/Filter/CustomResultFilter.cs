using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using Sampan.WebExtension.Model;

namespace Sampan.WebExtension.Filter
{
    /// <summary>
    /// 参数校验Filter
    /// </summary>
    public class CustomResultFilter : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            JsonResultModel<string> messageModel = new JsonResultModel<string>();
            if (!context.ModelState.IsValid)
            {
                //new ValidationError(key, x.ErrorMessage)
                var result = context.ModelState.Keys
                    .SelectMany(key => context.ModelState[key].Errors.Select(x => x.ErrorMessage))
                    .ToList();
                messageModel.errorMsg = ResultMessage.ValidationError;
                messageModel.code = HttpStatusCode.ArgumentError;
                messageModel.status = false;
                messageModel.data = result.FirstOrDefault(); //string.Join("|", result);//目前统一转化成字符串显示
                context.Result = new ObjectResult(messageModel);
            }

            base.OnResultExecuting(context);
        }
    }
}