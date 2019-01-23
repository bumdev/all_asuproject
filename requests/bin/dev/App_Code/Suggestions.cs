using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using Entities;
using DomainObjects;
using ConvincingMail.AdvancedAutoSuggest;
using System.Linq;

[WebService(Namespace = "http://convincingmail.com/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class Suggestions : WebService {

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        // Create array of movies  
        string[] movies = { "Star Wars", "Star Trek", "Superman", "Memento", "Shrek", "Shrek II" };

        // Return matching movies  
        return (from m in movies where m.StartsWith(prefixText, StringComparison.CurrentCultureIgnoreCase) select m).Take(count).ToArray();
    }  
    
    
    /*[WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public string CitySuggest(string tryValue, string[] additionalParams)
    {
        List<SuggestionItem> items = new List<SuggestionItem>();
        List<string> surname = new List<string>();
        FAbonent fa = new FAbonent();
        FAbonentDO fado = new FAbonentDO();
        UniversalEntity ue = new UniversalEntity();
        ue = fado.RetrieveLikeSurname(tryValue);
        if (ue.Count > 0)
        {
            for (int i = 0; i < ue.Count; i++)
            {
                fa = (FAbonent)ue[i];
                //create SuggestionItem
                SuggestionItem suggestionItem = new SuggestionItem();
                suggestionItem.Title = fa.Surname;
                suggestionItem.Description = SuggestionTools.HighLight(fa.FirstName + " " + fa.LastName, tryValue) + fa.ID.ToString();
                suggestionItem.Id = fa.ID.ToString();
                //add item to the list
                items.Add(suggestionItem);
            }
        }
        //create result Item
        SuggestionResult suggestionResult = new SuggestionResult();
        suggestionResult.Items = items.ToArray();
        suggestionResult.Header = new BasicSuggestionTemplate("Please select your city.");
        suggestionResult.Footer = new BasicSuggestionTemplate("Powered by ConvincingMail");
        return suggestionResult.ToJSON(tryValue);
    }*/
}