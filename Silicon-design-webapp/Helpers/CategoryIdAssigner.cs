using Business.Models;
using Newtonsoft.Json;
using Silicon_design_webapp.ViewModels.Shared;
using System.Diagnostics;

namespace Silicon_design_webapp.Helpers;

public class CategoryIdAssigner
{
    public async Task<CategoryModel> assignIdAsync(HttpResponseMessage category)
    {
        {
            try
            {
                var jsonString = await category.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<CategoryModel>(jsonString);
                if (model != null)
                {
                    return model;
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return null!;
        }
    }
}
