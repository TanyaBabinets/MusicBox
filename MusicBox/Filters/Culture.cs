using Microsoft.AspNetCore.Mvc.Filters;
using System.Globalization;

namespace MusicBox.Filters
{
	public class Culture : Attribute, IActionFilter
	{ 
		public void OnActionExecuted(ActionExecutedContext filterContext)
		{
			
		}

		public void OnActionExecuting(ActionExecutingContext filterContext)
		{
			string? cultureName = null;
			// Получаем куки из контекста, которые могут содержать установленную культуру
			var cultureCookie = filterContext.HttpContext.Request.Cookies["lang"];
			if (cultureCookie != null)
				cultureName = cultureCookie;
			else
				cultureName = "ru";

			// Список культур
			List<string> cultures = new List<string>() { "ru", "en", "uk", "es" };
			if (!cultures.Contains(cultureName))
			{
				cultureName = "ru";
			}
			
			Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
		
			Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(cultureName);
		}
	}
}
